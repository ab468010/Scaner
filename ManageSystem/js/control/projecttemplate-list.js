if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
function projecttemplate() {
    (function () {
        $("#myModal .modal-body").load("child/edit-projecttemplate.html");
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        })
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "SelectRoom",
            //async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                var option = $("#editRoomId").empty();
                for (var i in s) {
                    option.append($("<option>").val(s[i].RoomId).text(s[i].Name));
                }
                $('#editRoomId').selectpicker('refresh');
            }, error: function (xhr) {
                alert("请联系管理员");
                return false;
            }
        })
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetProjectTemplateList",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                var tbody = $(".table tbody").empty();
                for (var i in s) {
                    var cont = "<td name='projecttemplateid' style='display:none'>" + s[i].ProjectTemplateId + "</td><td>" + s[i].TaskName + "</td><td>" + s[i].RoomName + "</td><td>" + s[i].Description +
                        "<ul class='actions'><li class='last'><a class='projecttemplate2 edit1'href='#myModal' data-toggle='modal'>编辑</a>  <a class='projecttemplate2 delete1' >删除</a></li></ul>"
                    var row = document.createElement("tr");
                    row.innerHTML = cont;
                    tbody.append(row);
                }
                $(".delete1").click(function () {
                    if (confirm("确定删除？")) {
                        var jsonPara = {
                            projectTemplateId: $(this).parent().parent().parent().parent().find("[name=projecttemplateid]").text()
                        }
                    }
                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "DeleteProjectTemplate",
                        contentType: "application/json;charset=utf-8",
                        data:JSON.stringify(jsonPara),
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s) {
                                alert("删除成功");
                                window.location.reload();
                            } else {
                                alert("删除失败");
                                return false;
                            }
                        }, error: function (xhr) {
                            alert("请联系管理员");
                            return false;
                        }
                    })
                });
                $(".edit1").click(function () {
                   
                    var jsonPara = {
                        projectTemplateId: $(this).parent().parent().parent().parent().find("[name=projecttemplateid]").text()
                    }
                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl+"GetProjectTemplate",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(jsonPara),
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            $("#editTaskName").val(s.TaskName);
                            $("#editDescription").text(s.Description);
                            $("#editProjecttemplateid").val(s.ProjectTemplateId);
                            $("#editRoomId").attr("selected", false);
                            $("#editRoomId option[value=" + s.RoomId + "]").attr("selected", true);
                            $('#editRoomId').selectpicker('refresh');
                           
                        },
                        error: function (xhr)
                        {
                            alert("请联系管理员");
                            return false;
                        }
                    })
                })
            }, error: function (xhr) {
                alert("请联系管理员");
                return false;
            }
            
        })
        $("#savechange").click(function () {
            if (Globals.trim($("#editTaskName").val()) != "") {
                var jsonPara = {
                    projecttemplate: {
                        taskname: $("#editTaskName").val(),
                        description: $("#editDescription").text(),
                        roomid: $("#editRoomId").val(),
                        projecttemplateid: $("#editProjecttemplateid").val()
                    }
                }

                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "UpdateProjectTemplate",
                    data: JSON.stringify(jsonPara),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        if (s) {
                            alert("更新成功");
                            window.location.reload();
                        } else {
                            alert("更新失败");
                            return false;
                        }
                    }, error: function (xhr) {
                        alert("请联系管理员");
                        return false;
                    }
                })
            } else {
                alert("任务名不能空");
                return false;
            }
  
        })
    })()
}
$(function () {
    projecttemplate();
})