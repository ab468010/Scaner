
/// <reference path="../../Page/child/edit-container.html" />
if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var containerJs, containerVar;
var id = $.getUrlParam("containerId");
var SystemUserId = Globals.getCookie("SystemUserId");
var privilege = JSON.parse(Globals.getCookie("privilege"))
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
            $("#login").click(function () {
                if (confirm("确定注销？")) {
                    location.href = "login.html";
                }
            });
            $.ajax({
                type: "post",
            
                url: Globals.ServiceUrl + "GetContainer",
                data: JSON.stringify(jsonPara),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var container = JSON.parse(data.d);
                 
                    $("#statuscode").val(container.ProjectStatusCode)

                    
                    $("#tleName").text(container.Name);
                    $("#pDescription").text(container.Description);
                    $("#spanContainerCode").text(container.ContainerCode);
                    $("#spanSize").text(container.Size);
                    $("#spanProject").text(container.ProjectIdName);
                    $("#spanStatus").text(containerJs.bulidstatus(container.StatusCode));

                    $("#txtName").val(container.Name);
                    $("#txtContainerCode").val(container.ContainerCode);
                    $("#txtSize").val(container.Size);
                    $("#textDescription").val(container.Description);

                }, error: function (xhr) {
                    alert("请联系管理员");
                    return false;
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
                //async: false,
                data: JSON.stringify(jsonPara),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var sampleList = JSON.parse(data.d);
                    var tbody = $(".table tbody").empty();

                    for (i in sampleList) {
                        var content = "<td>" + sampleList[i].Name + "</td><td>" + sampleList[i].ProjectIdName + "</td><td>" + sampleList[i].SampleCode + "<ul class='actions samplelist' ><li class='last'><a class='delete1 sample2'style='display:none'>移除</a></li></ul></td>" +
                                     "<td style='display:none' name='Id'>" + sampleList[i].SampleId + "</td><td name='projectstatuscode'style='display:none'>"+sampleList[i].ProjectStatusCode+"</td>";
                      
                        var row = document.createElement("tr");
                        row.innerHTML = content;

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
                    $(".delete1.sample2").click(function () {
                        var s = $(this).parent().parent().parent().parent().find("[name='projectstatuscode']").text();
                        if(s>2){
                            alert("无法移除样品");
                            return false;
                        }else{
                            if (confirm("移除吗？")) {
                                var jsonPar = {
                                    sampleId: $(this).parent().parent().parent().parent().find("[name='Id']").text(),
                                    containerId: id,
                                    modifiedBy:SystemUserId
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
                                            return;
                                        }
                                    }, error: function (xhr) {
                                        alert("请联系管理员");
                                        return false;
                                    }
                                })
                            }
                        }
                       
                    });
                }           
            });
        });
        $(".container2.edit1").click(function () {
            if ($("#statuscode").val() < 3) {
                return true;
            } else {
                alert("无法编辑");
                return false;
            }
        })
        $(".delete1.container2").click(function () {
            if ($("#statuscode").val() < 3) {
                jsonPara = {
                    containerId: id
                };

                if (confirm("确认要删除?")) {
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
                            return false;
                        }
                    });
                }
            } else {
                alert("无法删除");
                return false;
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
                        Description: $("#textDescription").val(),
                        ModifiedBy: SystemUserId
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
                            return false;
                        }
                    }, error: function (xhr) {
                        alert("请联系管理员");
                        return false;
                    }
                })
            } else {
                alert("周转箱和条码不能为空");
                return false;
            }
           
        });
    })();
}

$(function () {
    initConfig();
});


