﻿if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var id = $.getUrlParam("projectId");
var createdby = Globals.getCookie("SystemUserId");
$(function () {
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetTesterList",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var result = JSON.parse(data.d);
            var list = $("#tester1").empty();
            var list1 = $("#tester2").empty();
            for (var i in result) {
                list.append($("<option>").val(result[i].SystemUserId).text(result[i].Name));
                list1.append($("<option>").val(result[i].SystemUserId).text(result[i].Name));

            }
       
            $("#tester1").select2();
            $("#tester2").select2();
        }
    });

    $("#login").click(function () {
        if (confirm("确定注销？")) {
            location.href = "login.html";
        }
    });
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetProjectListByStatus",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var s = JSON.parse(data.d);
            var option = $("#projectid").empty();
            for (var i in s) {
                option.append($("<option>").val(s[i].ProjectId).text(s[i].Name));
            }
            $('#projectid').select2();
            if (id != null) {
                $("#projectid option").attr("selected", false);
                $("#projectid option[val=" + id + "]").attr("selected", true);
                $("#projectid").val(id);
                $('#projectid').select2();
                $("#projectid").attr("disabled","disabled");
            }
        }, error: function (xhr) {
            alert("请联系管理员");
            return false;
        }
    })
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "SelectRoom",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var s = JSON.parse(data.d);
            var option = $("#roomid").empty();
            for (var i in s) {
                option.append($("<option>").val(s[i].RoomId).text(s[i].Name));
            }
            $('#roomid').select2();
        }, error: function (xhr) {
            alert("请联系管理员");
            return false;
        }
    })
    $("#btnSave").click(function () {
        if (Globals.trim($("#name").val()) != "" && $("#estimatedstart").val() != "" && $("#estimatedend").val() != "") {

            if ($("#estimatedstart").val() < $("#estimatedend").val()) {
                var jsonPara = {
                    task: {
                        name: $("#name").val(),
                        projectid: $("#projectid").val(),
                        roomid: $("#roomid").val(),
                        estimatedstart: $("#estimatedstart").val(),
                        estimatedend: $("#estimatedend").val(),
                        description: $("#description").val(),
                        tester1: $("#tester1").val(),
                        tester2: $("#tester2").val(),
                        CreatedBy: createdby
                    }
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "CreateTask",
                    data: JSON.stringify(jsonPara),
                    dataType: "json",
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        if (s) {
                            alert("创建成功");
                            if (id != null) {
                                location.href = "project-profile.html?projectId=" + id;
                            } else {
                                location.href = "task-list.html";
                            }
                          
                        } else {
                            alert("创建失败");
                            return false;
                        }
                    }, error: function (xhr) {
                        alert("请联系管理员");
                        return false;
                    }
                })
            } else {
                alert("开始时间必须小于结束时间");
                return false;
            }
        
        } else {
            alert("任务名和预计时间必填");
            return false;
        }
     
    })
})