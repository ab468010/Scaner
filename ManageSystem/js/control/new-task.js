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
       
            $('#tester1').selectpicker('refresh');
            $('#tester2').selectpicker('refresh');
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
            $('#projectid').selectpicker('refresh');
        }, error: function (xhr) {
            alert(xhr);
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
            $('#roomid').selectpicker('refresh');
        }, error: function (xhr) {
            alert(xhr);
        }
    })
    $("#btnSave").click(function () {
        if (Globals.trim($("#name").val()) != "" && $("#estimatedstart").val() != "" && $("#estimatedend").val() != "") {
            var jsonPara = {
                task: {
                    name: $("#name").val(),
                    projectid: $("#projectid").val(),
                    roomid: $("#roomid").val(),
                    estimatedstart: $("#estimatedstart").val(),
                    estimatedend: $("#estimatedend").val(),
                    description: $("#description").val(),
                    tester1:$("#tester1").val(),
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
                        location.href = "task-list.html";
                    } else {
                        alert("创建失败");
                    }
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        } else {
            alert("任务名和预计时间必填");
        }
     
    })
})