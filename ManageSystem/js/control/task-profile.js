if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var statucode = $.getUrlParam("projectstatuscode");
var id = $.getUrlParam("taskid");
var tester1 = $.getUrlParam("tester1");
var tester2 = $.getUrlParam("tester2");
var roomid = $.getUrlParam("roomid");
var systemuserid = Globals.getCookie("SystemUserId");
var privilege = JSON.parse(Globals.getCookie("privilege"));
function taskprofile() {
    $("#myModal .modal-body").load("child/edit-task.html");
    $("#myModal2 .modal-body").load("child/edit-tasksample.html");
 
    (function () {
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $("#samplecreate").click(function () {
            if (statucode > 2) {
                alert("无法添加");
            } else {
                var jsonPar = {
                    taskid: id,
                    projectid: $("#projectid_hidden").val(),

                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "ExceptSampleList",
                    data: JSON.stringify(jsonPar),
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        var option = $("#edit_samplename").empty()
                        for (i in s) {
                            option.append($("<option>").val(s[i].SampleId).text(s[i].Name))

                        }
                        $('#edit_samplename').selectpicker('refresh');
                    }, error: function (xhr) {
                        alert("请联系管理员");
                        return false;
                    }
                })
            }
          
        })
        $("#savechange2").click(function () {
            var jsonPara = {
                taskid: id,
                sampleid: $("#edit_samplename").val(),
             
            }
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: Globals.ServiceUrl + "CreateTaskSample",
                data: JSON.stringify(jsonPara),
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("添加成功")
                        window.location.reload();
                    } else {
                        alert("添加失败");
                        return false;
                    }
                }, error: function (xhr) {
                    alert("请联系管理员");
                    return false;
                }
            })
        })
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
                $("#roomid option").attr("selected", false)
                $("#roomid option[val=" + roomid + "]").attr("selected", true);
                $("#roomid").val(roomid);
                $('#roomid ').selectpicker('refresh');
            }, error: function (xhr) {
                alert("请联系管理员");
                return false;
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
                $("#tester1 option").attr("selected", false)
                $("#tester1 option[val=" + tester1 + "]").attr("selected", true);
                $("#tester1").val(tester1);
                $('#tester1 ').selectpicker('refresh');

                $('#tester2').selectpicker('refresh');
                $("#tester2 option").attr("selected", false)
                $("#tester2 option[val=" + tester2+ "]").attr("selected", true);
                $("#tester2").val(tester2);
                $('#tester2 ').selectpicker('refresh');
            }
        });
        var jsonPa = {
            taskid:id
        }          
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "SelectTaskId",
           // async: false,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(jsonPa),
            success: function (data) {
                var sh = JSON.parse(data.d);
           
            
            


                $('#tester2 .selectpicker').selectpicker('refresh');
                $("#tester2 option").attr("selected", false)
                $("#tester2 option[val=" + sh.Tester2+ "]").attr("selected", true);
                $("#tester2").val(sh.Tester2);
                $('#tester2 ').selectpicker('refresh');

                $("#Tester1").text(sh.Tester1IdName);
                $("#Tester2").text(sh.Tester2IdName);
                $("#projectid_hidden").val(sh.ProjectId);
                $("#name").val(sh.Name);
                $("#projectid").val(sh.ProjectName);
                $("#description").val(sh.Description);
                $("#taskid").val(sh.TaskId);
                $("#estimatedstart").val(Globals.datetime_is_null(sh.EstimatedStart) == "空" ? "" : Globals.datetime_is_null(sh.EstimatedStart));
                $("#estimatedend").val(Globals.datetime_is_null(sh.EstimatedEnd) == "空" ? "" : Globals.datetime_is_null(sh.EstimatedEnd));
                $("#tleName").text(sh.Name);
                $("#ProjectId").text(sh.ProjectName);
                $("#RoomId").text(sh.RoomName);
                $("#pDescription").text(sh.Description);
                $("#Estimatedstart").text(Globals.datetime_is_null(sh.EstimatedStart));
                $("#Estimatedend").text(Globals.datetime_is_null(sh.EstimatedEnd));



                if (Globals.datetime_is_null(sh.ActualStart) !== "空") {
                    $("#roomid").attr("disabled", "disabled");
                }


            }, error: function (xhr) {
                alert("请联系管理员");
                return false;
            }
        });
        $(".task2.edit1").click(function () {
            if (statucode > 2) {
                alert("无法更改");
                return false;
            } else {
                return true;
            }
        })
          
            $("#deletetask").click(function () {
                if (statucode > 2) {
                    alert("无法删除");
                } else {
                    if (confirm("删除吗？")) {
                        var jsonPar = {
                            taskid: id
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
                                    location.href = "task-list.html";
                                } else {
                                    alert("请先删除样品");
                                    return false;
                                }
                            }, error: function (xhr) {
                                alert("请联系管理员");
                                return false;
                            }
                        })
                    }
                }
        

        })
        $("#savechange").click(function () {
            if (Globals.trim($("#name").val())!="") {
                var jsonPara = {
                    task: {
                        name: $("#name").val(),
                        description: $("#description").val(),
                        taskid: $("#taskid").val(),
                        estimatedstart: $("#estimatedstart").val(),
                        estimatedend: $("#estimatedend").val(),
                        roomid: $("#roomid").val(),
                        Tester1: $("#tester1").val(),
                        Tester2:$("#tester2").val(),
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
                            return false;
                        }
                    }, error: function (xhr) {
                        alert("请选择测试工程师");
                        return false;
                    }
                })
            } else {
                alert("任务名不能为空");
                return false;
            }
          
        })
        
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            url: Globals.ServiceUrl + "GetSampleListByTaskId",
            data: JSON.stringify(jsonPa),
            success: function (data) {
                var sampleList = JSON.parse(data.d);
                var tbody = $(".table tbody").empty();
                for (var i in sampleList) {
                    var sampleclass = "";
                    if (sampleList[i].SampleClass == 1) {
                        sampleclass = "样品";
                    } else {
                        sampleclass = "附件";
                    }
                    var cont = "<td>" + sampleList[i].Name + "</td><td>" + sampleList[i].ContainerIdName + "</td><td>" + sampleList[i].SampleCode + "</td><td>" + sampleclass + "<ul class='actions'><li class='last'><a class='sample2 delete1'style='display:none'>移除</a></li></ul></td>" + "<td style='display:none' name='Id'>" + sampleList[i].SampleId + "</td>";
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
                $(".sample2.edit1").click(function () {
                    if (statucode > 2) {
                        alert("无法编辑");
                        return false;
                    } else {
                        return true;
                    }
                })
                $(".sample2.delete1").click(function () {
                    if (statucode > 2) {
                        alert("项目已开始无法删除样品")
                    } else {
                        if (confirm("删除吗？")) {
                            var jsonPar = {
                                sampleId: $(this).parent().parent().parent().parent().find("[name='Id']").text()
                            }
                            $.ajax({
                                type: "post",
                                url: Globals.ServiceUrl + "DeleteTaskSample",
                                contentType: "application/json; charset=utf-8",
                                data: JSON.stringify(jsonPar),
                                success: function (data) {
                                    var s = JSON.parse(data.d);
                                    if (s) {
                                        alert("删除成功");
                                        window.location.reload();
                                    } else {
                                        alert("删除失败");
                                        return false;
                                    }
                                }
                            })
                        }
                    }
                
                })
            }, error: function (xhr) {
                alert("请联系管理员");
                return false;
            }
            
        })

    })()
}
$(function () {
    taskprofile();
})