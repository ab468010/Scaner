/// <reference path="../../Page/child/edit-sample.html" />
if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var sampleJs, sampleVar;
var id = $.getUrlParam("id");
var projectid = $.getUrlParam("projectId");
var containerid = $.getUrlParam("containerId");
var systemuserid = Globals.getCookie("SystemUserId");
function initConfig() {
    //初始化模块JS
    sampleJs = new Globals.sample();

    //页面变量
    sampleVar = {

    };

    if (id == "" || id == null)
    {
        alert("找不到样品...");
        return false;
    }

    (function () {
        var userInfo = {
            userId: 1
        };

        $(document).ready(function () {
            $("#myModal .modal-body").load("child/edit-sample.html");
            $("#login").click(function () {
                if (confirm("确定注销？")) {
                    location.href = "login.html";
                }
            });
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetProjectListByStatus",
      
                data: JSON.stringify(userInfo),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
              
                    var child_option = $("#child_sltProject").empty();

                    for (var i in s) {
     
                        child_option.append($("<option>").val(s[i].ProjectId).text(s[i].Name));
                    }
                 
                    child_option.append($("<option>").val(-1).text("Nothing Selected"));  
                    $('#child_sltProject').selectpicker('refresh');
                    $("#child_sltProject option").attr("selected", false);
                    $("#child_sltProject option[value=" + projectid + "]").attr("selected", true);
                    $("#child_sltProject ").val(projectid);
                    $('#child_sltProject').selectpicker('refresh');
                }, error: function (xhr) {
                    alert("请联系管理员");
                    return false;
                }
            });

     
                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "GetContainerList",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        var child_option = $("#child_sltContainer").empty();

                        for (var i in s) {
                            child_option.append($("<option>").val(s[i].ContainerId).text(s[i].Name))
                        }
 
                        child_option.append($("<option>").val(-1).text("Nothing Selected"));
                        $('#child_sltContainer').selectpicker('refresh');
                        $("#child_sltContainer option").attr("selected", false);
                        $("#child_sltContainer option[value=" + containerid + "]").attr("selected", true);
                        $("#child_sltContainer").val(containerid);
                        $('#child_sltContainer').selectpicker('refresh');
                    }, error: function (xhr) {
                        alert("请联系管理员");
                        return false;
                    }
                });
        
            if (id != null && id != "") {
                var jsonPara = {
                    sampleId: id
                };
              
                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "GetSample",
                    data: JSON.stringify(jsonPara),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        if (s == null) {
                            alert("样品不存在");
                            location.href = "sample-list.html";
                        } else {
                            $("#statusCode").val(s.ProjectStatusCode);
                         
                            $("#txtName").val(s.Name);
                            $("#txtSampleCode").val(s.SampleCode);

                            $("#sltProject").val(s.ProjectIdName);

                            $("#sltContainer").val(s.ContainerIdName);
   
                            $("#txtShelf").val(s.ShelfIdName);
   
                            $("#sltSampleClass").val(sampleJs.getSampleClassText(s.SampleClass));

                            $("#textDescription").val(s.Description);
                            
                         
                            $('#sltSampleClass').selectpicker('refresh');


                            $("#child_txtName").val(s.Name);
                            $("#child_txtSampleCode").val(s.SampleCode);

                  

                         

                            $("#child_sltSampleClass option").attr("selected", false);
                            $("#child_sltSampleClass option[value=" + s.SampleClass + "]").attr("selected", true);
                            $("#child_sltSampleClass").val(s.SampleClass);

                            $("#child_textDescription").val(s.Description);
                            $('#child_sltProject').selectpicker('refresh');
                            $('#child_sltContainer').selectpicker('refresh');
                            $('#child_sltSampleClass').selectpicker('refresh');
                        }

                    }, error: function (xhr) {
                        alert("请联系管理员");
                        return false;
                    }
                });
            } else {
                alert("样品不存在");
                location.href = "sample-list.html";
            }
        });
        $(".sample2.edit1").click(function () {
            if($("#statusCode").val() < 3){
                return true;
            } else {
                return false;
            }
        })

        $(".delete1").click(function () {
            jsonPara = {
                sampleId: id
            };
            if ($("#statusCode").val() < 3) {
                if (confirm("确认要删除该样品?")) {
                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "DeleteSample",
                        data: JSON.stringify(jsonPara),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d) {
                                alert("删除成功");
                                location.href = "sample-list.html";
                            }
                            else
                                alert("删除失败");
                            return false;
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(XMLHttpRequest.status);
                            alert(XMLHttpRequest.readyState);
                            alert(textStatus);
                        }
                    });
                } 
            } else {
                alert("样品无法删除");
                return false;
            }
          
        });
        $("#savechange").click(function () {
            if (Globals.trim($("#child_txtName").val()) != "" && Globals.trim($("#child_txtSampleCode").val()) != "") {
                var jsonPara = {
                    sample: {
                        SampleId: id,
                        Name: $("#child_txtName").val(),
                        SampleCode: Globals.trim($("#child_txtSampleCode").val()),
                        ProjectId: $("#child_sltProject").val() == -1 ? null : $("#child_sltProject").val(),
                        ContainerId: $("#child_sltContainer").val() == -1 ? null : $("#child_sltContainer").val(),
                        SampleClass: $("#child_sltSampleClass").val() == -1 ? null : $("#child_sltSampleClass").val(),
                        Description: $("#child_textDescription").val(),
                        ModifiedBy:systemuserid
                    }
                }

                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "UpdateSample",
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
                alert("样品名和条码不能为空");
                return false;
            }
      
        });

    })();
}

$(function () {
    initConfig();
});


