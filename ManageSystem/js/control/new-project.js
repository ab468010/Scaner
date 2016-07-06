var userJs, userVar;


function initConfig() {
    //初始化模块JS
    userJs = new Globals.project();

    //页面变量
    userVar = {

    };

    (function () {
        $(function () {

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
                    
                    $('#sltEngineer').selectpicker('refresh');
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
                    $('#sltCustomer').selectpicker('refresh');
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });

            //获取测试工程师
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetTesterList",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var result = JSON.parse(data.d);
                    var list = $("#sltTester").empty();
                    for (var i in result) {
                        list.append($("<option>").val(result[i].SystemUserId).text(result[i].Name));

                    }
                    list.append($("<option>").val(-1).text("Nothing selected"));
                    $('#sltTester').selectpicker('refresh');
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
                        TesterId: $("#sltTester").val()
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
            }
           
        });


    })();
}

$(function () {
    initConfig();
});


