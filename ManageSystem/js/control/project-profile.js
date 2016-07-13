/// <reference path="../../Page/child/edit-project.html" />
var projectJs, projectVar;
var id = $.getUrlParam("projectId");


function initConfig() {
    //初始化模块JS
    projectJs = new Globals.project();

    //页面变量
    projectVar = {

    };

    (function () {

        
        $(document).ready(function () {
            $("#myModal .modal-body").load("child/edit-project.html");
            $("#login").click(function () {
                if (confirm("确定注销？")) {
                    location.href = "login.html";
                }
            });
            var jsonP = {
                rolename: "Engineer"
            }
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetUserByRole",
                data: JSON.stringify(jsonP),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    var option = $("#projectengineer").empty();
                    for (var i in s) {
                        option.append($("<option>").val(s[i].SystemUserId).text(s[i].Name));
                       
                    }
                    option.append($("<option>").val(-1).text("Nothing selected"));
                    $('#projectengineer').selectpicker('refresh');
                }
            })
          var  jsonPara = {
                projectId: id
            };

            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetProject",
               
                data: JSON.stringify(jsonPara),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                 
                    var project = JSON.parse(data.d);
                    $("#projectstatuscode").val(project.StatusCode);
                    if (project.StatusCode < 3) {
                        $(".project2.edit1").attr({ style: "display:inline" });
                        $(".project2.delete1").attr({ style: "display:inline" });
                       
                    }
                    if (project.StatusCode == 4) {
                        $("#givecustomer").attr({style:"display:inline"})
                    } 

                    $("#tleName").text(project.Name);                  
                    $("#pDescription").text(project.Description);
                    $("#spanProjectNo").text(project.ProjectNo);
                    $("#spanEngineer").text(project.EngineerIdName);
                 
                    $("#spanCustomer").text(project.CustomerIdName);
                    $("#spanStatus").text(projectJs.bulidstatus(project.StatusCode));
                    $("#spanStartTime").text(Globals.datetime_is_null(project.StartTime));
                    $("#spanEndTime").text(Globals.datetime_is_null(project.EndTime));
                    
                    $("#projectname").val(project.Name);
                    $("#projectno").val(project.ProjectNo);
                    $("#statuscode").val(projectJs.bulidstatus(project.StatusCode));

                    $("#testengineer option").attr("selected", false);
                    $("#testengineer option[val=" + project.TesterId + "]").attr("selected",true);
                    $('#testengineer').selectpicker('refresh');

                    $("#projectengineer option").attr("selected", false);
                    $("#projectengineer option[val=" + project.EngineerId + "]").attr("selected",true);
                    $('#projectengineer').selectpicker('refresh');

                    $("#description").val(project.Description);


                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });
        });
        var jsonPar = {
            projectId:id
        }
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetTaskListByProjectId",
            //async: false,
            data: JSON.stringify(jsonPar),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                var tbody = $(".table tbody").empty();
                for (var i in s) {
                    var cont = "<td>" + s[i].Name + "</td><td>" + s[i].RoomName + "</td><td>" + Globals.datetime_is_null(s[i].EstimatedStart) + "</td><td>" + Globals.datetime_is_null(s[i].EstimatedEnd) + "</td><td>"
                        + Globals.datetime_is_null(s[i].ActualStart) + "</td><td>" + Globals.datetime_is_null(s[i].ActualEnd) + "<ul class='actions'style='display:none'><li class='last'><a class='task2 edit1'href='#myModal1' data-toggle='modal'>编辑</a>  <a class='task2 delete1' >删除</a></li></ul>"
                        + "</td><td name='taskid' style='display:none'>" + s[i].TaskId + "</td>";
                    var row = document.createElement("tr");
                    row.innerHTML = cont;
                    tbody.append(row);
                    if ($("#projectstatuscode").val ()< 3) {
                        $(".actions").attr({style:"display:inline"})
                    }
                }

                $(".task2.delete1").click(function () {
                    if ($("#projectstatuscode").val()<3) {
                        if (confirm("删除吗？")) {
                            var jsonPar = {
                                taskid: $(this).parent().parent().parent().parent().find("[name='taskid']").text()
                            }
                            $.ajax({
                                type: "post",
                                url: Globals.ServiceUrl + "DeleteTask",
                                contentType: "application/json; charset=utf-8",
                                data: JSON.stringify(jsonPar),
                                success: function (data) {
                                    var s = JSON.parse(data.d);
                                    if (s) {
                                        alert("删除成功");
                                        window.location.reload();
                                    } else {
                                        alert("请先删除样品");
                                    }
                                }, error: function (xhr) {
                                    alert(xhr);
                                }
                            })
                        }

                    } else {
                        alert("项目完成不能删除")
                    }
                        
                
                });
                $(".task2.edit1").click(function () {
                    if ($("#projectstatuscode").val() < 3) {
                        $("#myModal1 .modal-body").load("child/edit-task.html");
                        var json = {
                            taskid: $(this).parent().parent().parent().parent().find("[name='taskid']").text()
                        }
                        $.ajax({
                            type: "post",
                            url: Globals.ServiceUrl + "SelectTaskId",
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify(json),
                            success: function (data) {
                                var sh = JSON.parse(data.d);
                                $("#name").val(sh.Name);
                                $("#projectid").val(sh.ProjectName);
                                $("#description").val(sh.Description);
                                $("#roomid").val(sh.RoomName);
                                $("#taskid").val(sh.TaskId);
                                $("#estimatedstart").val(Globals.datetime_is_null(sh.EstimatedStart) == "空" ? "" : Globals.datetime_is_null(sh.EstimatedStart));
                                $("#estimatedend").val(Globals.datetime_is_null(sh.EstimatedEnd) == "空" ? "" : Globals.datetime_is_null(sh.EstimatedEnd));


                            }, error: function (xhr) {
                                alert(xhr);
                            }
                        })
                    } else {
                        alert("项目完成不能编辑")
                    }
                        
                    
                  
                })

            }, error: function (xhr) {
                alert(xhr)
            }
        })
        $(".project2.delete1").click(function () {
           var jsonPara = {
                projectId: id
            };

            if (confirm("确认要删除该项目?")) {
                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "DeleteProject",
                    data: JSON.stringify(jsonPara),
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        if (s) {
                            alert("删除成功");
                            location.href = "project-list.html";
                        }
                        else
                            alert("请先删除相关任务");
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(XMLHttpRequest.status);
                        alert(XMLHttpRequest.readyState);
                        alert(textStatus);
                    }
                });
            }
        });
        $("#savechange").click(function () {
            var jsonPara = {
                project: {
                    projectId:id,
                    name: $("#projectname").val(),
                    projectNo: $("#projectno").val(),
                    testerId: $("#testengineer").val(),
                    engineerId: $("#projectengineer").val(),
                    description: $("#description").val(),
                }
            }
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: Globals.ServiceUrl + "UpdateProject",
                data: JSON.stringify(jsonPara),
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("更新成功");
                        window.location.reload();
                    } else {
                        alert("更新失败");
                    }
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        });
        $("#savechange1").click(function () {
            var jsonPara = {
                task: {
                    name: $("#name").val(),
                    description: $("#description").val(),
                    taskid: $("#taskid").val(),
                    estimatedstart: $("#estimatedstart").val(),
                    estimatedend: $("#estimatedend").val()
                }
            }
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: Globals.ServiceUrl + "UpdateTask",
                data: JSON.stringify(jsonPara),
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("更新成功");
                        window.location.reload();
                    } else {
                        alert("更新失败");
                    }
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        })

        $("#givecustomer").click(function () {         
            var jsonPara = {
                projectId: id
            }
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "UpdateProjectStatusCode",
                data: JSON.stringify(jsonPara),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        $("#spanStatus").text(projectJs.bulidstatus(5));
                        alert("归还成功");
                    } else {
                        alert("归还失败");
                    }

                }
            })
        })
    })();
}

$(function () {
    initConfig();
});


