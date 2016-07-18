/// <reference path="../../Page/child/edit-project.html" />
var projectJs, projectVar;
var id = $.getUrlParam("projectId");
var systemuserid = Globals.getCookie("SystemUserId");
var privilege = JSON.parse(Globals.getCookie("privilege"));
function initConfig() {
    //初始化模块JS
    projectJs = new Globals.project();

    //页面变量
    projectVar = {

    };

    (function () {

        
        $(document).ready(function () {
            $("#myModal .modal-body").load("child/edit-project.html");
            $("#myModal1 .modal-body").load("child/edit-task.html");
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
                // async: false,
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
            });
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "SelectRoom",
                // async: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    option = $("#roomid").empty();
                    for (var i in s) {
                        option.append($("<option>").val(s[i].RoomId).text(s[i].Name));
                    }
                    $('#roomid').selectpicker('refresh');
                }, error: function (xhr) {
                    alert(xhr);
                }
            });
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetTesterList",
                //async: false,
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

                    $("#projectengineer option").attr("selected", false);                  
                    $("#projectengineer option[val=" + project.EngineerId + "]").attr("selected", true);
                     $("#projectengineer").val(project.EngineerId);
                    $('#projectengineer').selectpicker('refresh');


                    $("#projectstatuscode").val(project.StatusCode);

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
                 

                    $("#pdescription").text(project.Description);


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
                    var cont = "<td>" + s[i].Name + "</td><td>" + s[i].RoomName +"</td><td>"+s[i].Tester1IdName+"</td><td>"+s[i].Tester2IdName+"</td><td>" + Globals.datetime_is_null(s[i].EstimatedStart) + "</td><td>" + Globals.datetime_is_null(s[i].EstimatedEnd) + "</td><td>"
                        + Globals.datetime_is_null(s[i].ActualStart) + "</td><td>" + Globals.datetime_is_null(s[i].ActualEnd) + "<ul class='actions'><li class='last'><a class='task2 edit1'href='#myModal1' data-toggle='modal'>编辑</a>  <a class='task2 delete1' >删除</a></li></ul>"
                        + "</td><td name='taskid' style='display:none'>" + s[i].TaskId + "</td>";
                    var row = document.createElement("tr");
                    row.innerHTML = cont;
                    tbody.append(row);
                 
                }
                for (var i in privilege) {
                    if (privilege[i].CanDelete == true) {
                        $("." + privilege[i].Tablename + 2 + ".delete1").attr({ style: "display:inline" });
                    }
                    if (privilege[i].CanCreate == true) {
                        $("." + privilege[i].Tablename + 2 + ".create1").attr({ style: "display:inline" });
                    }
                    if (privilege[i].CanWrite == true) {
                        $("." + privilege[i].Tablename + 2 + ".edit1").attr({ style: "display:inline" });
                    }
                    if (privilege[i].CanRead == true) {
                        $("." + privilege[i].Tablename + 2 + ".read1").attr({ style: "display:inline" });

                    }
                };
                $(".task2.delete1").click(function () {
                    if ($("#projectstatuscode").val() < 3) {
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
                        alert("项目无法删除");
                        return false;
                    }


                });
                $(".task2.edit1").click(function () {
                    if ($("#projectstatuscode").val() < 3) {

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
                                $('#tester1 .selectpicker').selectpicker('refresh');
                                $("#tester1 option").attr("selected", false)
                                $("#tester1 option[val=" + sh.Tester1 + "]").attr("selected", true);
                                $("#tester1").val(sh.Tester1);
                                $('#tester1').selectpicker('refresh');

                                $('#tester2 .selectpicker').selectpicker('refresh');
                                $("#tester2 option").attr("selected", false)
                                $("#tester2 option[val=" + sh.Tester2 + "]").attr("selected", true);
                                $("#tester2").val(sh.Tester1);
                                $('#tester2').selectpicker('refresh');
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
                        alert("项目无法编辑");
                        return false;
                    }
                })

            }, error: function (xhr) {
                alert(xhr)
            }
        });
        $(".project2.edit1").click(function () {
            if ($("#projectstatuscode").val() < 3) {
                return true;
            } else {
                alert("无法编辑");
                return false;
            }
        })
        $(".project2.delete1").click(function () {
            if ($("#projectstatuscode").val() < 3) {
                var jsonPa = {
                    projectId: id
                };

                if (confirm("确认要删除该项目?")) {
                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "DeleteProject",
                        data: JSON.stringify(jsonPa),
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s) {
                                alert("删除成功");
                                location.href = "project-list.html";
                            }
                            else
                                alert("请先删除相关任务");
                        }

                    });
                }
            } else {
                alert("无法删除")
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
                    description: $("#pdescription").text(),
                    ModifiedBy:systemuserid
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
                    description: $("#description").text(),
                    taskid: $("#taskid").val(),
                    estimatedstart: $("#estimatedstart").val(),
                    estimatedend: $("#estimatedend").val(),
                    Tester1: $("#tester1").val(),
                    Tester2: $("#tester2").val(),
                    roomid:$("#roomid").val(),
                    ModifiedBy:systemuserid
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
                        window.location.reload()
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


