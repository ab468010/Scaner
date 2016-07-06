/// <reference path="../../Page/child/edit-container.html" />
var containerJs, containerVar;
var id = $.getUrlParam("containerId");


function initConfig() {
    //初始化模块JS
    containerJs = new Globals.container();

    //页面变量
    containerVar = {

    };

    if (id == "" || id == null)
    {
        alert("找不到周转箱...");
        return;
    }

    (function () {
        $("#myModal .modal-body").load("child/edit-container.html");

        $(document).ready(function () {


            jsonPara = {
                containerId: id
            };

            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetContainer",
                data: JSON.stringify(jsonPara),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var container = JSON.parse(data.d);

                    $("#tleName").text(container.Name);
                    $("#pDescription").text(container.Description);
                    $("#spanContainerCode").text(container.ContainerCode);
                    $("#spanSize").text(container.Size);
                   // $("#spanProject").text(container.ProjectIdName);
                    $("#spanStatus").text(containerJs.bulidstatus(container.StatusCode));

                    $("#txtName").val(container.Name);
                    $("#txtContainerCode").val(container.ContainerCode);
                    $("#txtSize").val(container.Size);
                    $("#textDescription").val(container.Description);

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });
        });

        $(document).ready(function () {

            jsonPara = {
                containerId: id
            };

            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetSampleListByContainerId",
                async: false,
                data: JSON.stringify(jsonPara),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var sampleList = JSON.parse(data.d);
                    var tbody = $(".table tbody").empty();

                    for (i in sampleList) {
                        var content = "<td>" + sampleList[i].Name + "</td><td>" + sampleList[i].ProjectIdName +  "</td><td>" + sampleList[i].SampleCode + "<ul class='actions'><li class='last'><a class='delete1 sample2'>删除</a></li></ul></td>" + "<td style='display:none' name='Id'>" + sampleList[i].SampleId + "</td>";
                        var row = document.createElement("tr");
                        row.innerHTML = content;

                        tbody.append(row);
                    }
                
                    $(".delete1.sample2").click(function () {
                        if (confirm("删除吗？")) {
                            var jsonPar = {
                                sampleId: $(this).parent().parent().parent().parent().find("[name='Id']").text()
                            }
                            $.ajax({
                                type: "post",
                                url: Globals.ServiceUrl + "UpdateContainerId",
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
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });
        });

        $(".delete1.container2").click(function () {
            jsonPara = {
                containerId: id
            };

            if (confirm("确认要删除该项目?")) {
                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "DeleteContainer",
                    data: JSON.stringify(jsonPara),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d) {
                            alert("删除成功");
                            location.href = "container-list.html";
                        }
                        else
                            alert("请先删除相关样品");
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
            if (Globals.trim($("#txtName").val()) != "" && Globals.trim($("#txtContainerCode").val()) != "") {
                var jsonPara = {
                    container: {
                        ContainerId: id,
                        Name: $("#txtName").val(),
                        ContainerCode: Globals.trim($("#txtContainerCode").val()),
                        Size: $("#txtSize").val(),
                        Description: $("#textDescription").val()
                    }
                }

                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "UpdateContainer",
                    data: JSON.stringify(jsonPara),
                    contentType: "application/json; charset=utf-8",
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
                alert("周转箱和条码不能为空");
            }
           
        });
    })();
}

$(function () {
    initConfig();
});


