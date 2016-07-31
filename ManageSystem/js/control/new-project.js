if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var userJs, userVar;

var createdby = Globals.getCookie("SystemUserId");
function initConfig() {
    //初始化模块JS
    userJs = new Globals.project();

    //页面变量
    userVar = {

    };

    (function () {
        $(function () {
            $("#login").click(function () {
                if (confirm("确定注销？")) {
                    location.href = "login.html";
                }
            });
            //获取项目工程师
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetEngineerList",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var result = JSON.parse(data.d);
                    var list = $("#sltEngineer").empty();
                    for (var i in result) {
                        list.append($("<option>").val(result[i].SystemUserId).text(result[i].Name));

                    }
                    
                    $('#sltEngineer').select2();
                }
            });

            //获取客户
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "Selectcustomer",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var result = JSON.parse(data.d);
                    var list = $("#sltCustomer").empty();
                    for (var i in result) {
                        list.append($("<option>").val(result[i].Customerid).text(result[i].Name));

                    }
                    $('#sltCustomer').select2();
                },error:function(xhr){

                    alert("请联系管理员");
                    return false;
                }
            });

      
        })

        $("#btnCreate").click(function () {
            if (Globals.trim($("#txtProjectNo").val())!= "" && Globals.trim($("#txtName").val())!="") {
                jsonPara = {
                    project: {
                        ProjectNo: $("#txtProjectNo").val(),
                        Name: $("#txtName").val(),
                        Description: $("#textDescription").text(),
                        CustomerId: $("#sltCustomer").val(),
                        EngineerId: $("#sltEngineer").val(),
                        TesterId: $("#sltTester").val(),
                        CreatedBy: createdby
                    }
                };

                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "CreateProject",
                    data: JSON.stringify(jsonPara),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var s = JSON.parse(data.d)
                        if (s > 0) {
                            alert("创建成功");
                            location.href = "project-profile.html?projectId=" + s;
                        }
                        else {
                            alert("错误");
                            return false;
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(XMLHttpRequest.status);
                        alert(XMLHttpRequest.readyState);
                        alert(textStatus);
                    }
                });
            } else {
                alert("项目编号项目名必填");
                return false;
            }
           
        });


    })();
}

$(function () {
    initConfig();
});


