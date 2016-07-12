var statucode = $.getUrlParam("projectstatuscode");
var id = $.getUrlParam("taskid");
function taskprofile() {
    $("#myModal .modal-body").load("child/edit-task.html");
    $("#myModal2 .modal-body").load("child/edit-tasksample.html");
    if (statucode < 3) {
        $(".task2.delete1").attr({ style: "display:inline" });
        $(".task2.edit1").attr({ style: "display:inline" });
    }
    (function () {
        $(".sample2.create1").click(function () {
            var jsonPar = {
                taskid: id,
                projectid:$("#projectid_hidden").val()
            }
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: Globals.ServiceUrl + "ExceptSampleList",
                data:JSON.stringify(jsonPar),
                success: function (data) {
                    var s = JSON.parse(data.d);
                   var option=$("#edit_samplename").empty()
                    for (i in s) {
                      option.append($("<option>").val(s[i].SampleId).text(s[i].Name))

                    }
                    $('#edit_samplename').selectpicker('refresh');
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        })
        $("#savechange2").click(function () {
            var jsonPara = {
                taskid: id,
                sampleid:$("#edit_samplename").val()
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
                    }
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        })
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "SelectRoom",
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
        var jsonPa = {
            taskid:id
        }          
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "SelectTaskId",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(jsonPa),
                success: function (data) {
                    var sh = JSON.parse(data.d);
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

                    $("#roomid option").attr("selected",false)
                    $("#roomid option[val=" + sh.RoomId + "]").attr("selected", true);
                    $('#roomid').selectpicker('refresh');
                    if (Globals.datetime_is_null(sh.ActualStart)!== "空") {
                        $("#roomid").attr("disabled", "disabled");
                    } 


                }, error: function (xhr) {
                    alert(xhr);
                }
            })
          
        $("#deletetask").click(function () {
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
                        }
                    }, error: function (xhr) {
                        alert(xhr);
                    }
                })
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
            } else {
                alert("任务名不能为空");
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
                         sampleclass="附件";
                    }
                    var cont = "<td>" + sampleList[i].Name + "</td><td>" + sampleList[i].ContainerIdName + "</td><td>" + sampleList[i].SampleCode + "</td><td>" + sampleclass + "<ul class='actions'><li class='last'><a class='sample2 delete1'>删除</a></li></ul></td>" + "<td style='display:none' name='Id'>" + sampleList[i].SampleId + "</td>";
                    var row = document.createElement("tr");
                    row.innerHTML = cont;
                    tbody.append(row);
                }
                $(".sample2.edit1").click(function () {

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
                                    }
                                }, error: function (xhr) {
                                    alert(xhr);
                                }
                            })
                        }
                    }
                
                })
            }, error: function (xhr) {
                alert(xhr);
            }
            
        })

    })()
}
$(function () {
    taskprofile();
})