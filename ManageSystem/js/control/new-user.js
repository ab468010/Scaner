var userJs, userVar;
var action = $.getUrlParam("action");
var id = $.getUrlParam("id");
var invokeMethod = action == "Create" ? "CreateUser" : "0"


function initConfig() {
    //初始化模块JS
    userJs = new Globals.user();

    //页面变量
    userVar = {

    };

    (function () {
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl+"GetRoleList",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                var option = $("#Role").empty();
                for (var i in s) {
                option.append($("<option>").val(s[i].RoleId).text(s[i].RoleName))
                }
                $('#Role').selectpicker('refresh');
            }, error: function (xhr) {
                alert(xhr);
            }
        })

        $("#btnSave").click(function () {
            if (Globals.trim($("#Username").val()) != "") {
                if (Globals.trim($("#UserCode").val()) != "") {
                    if ($("#pwdPassword").val() == $("#pwdAgain").val() && Globals.trim($("#pwdPassword").val()) != "") {
                        jsonPara = {
                            user: {
                                Name: $("#Name").val(),
                                Username: Globals.trim($("#Username").val()),
                                Password: hex_md5($("#pwdPassword").val()),
                                RoleId: $("#Role").val(),
                                Email: $("#Email").val(),
                                UserCode:Globals.trim($("#UserCode").val()),
                                Description: $("#Description").val()
                            }
                        }

                        $.ajax({
                            type: "post",
                            url: Globals.ServiceUrl + "CreateUser",
                            data: JSON.stringify(jsonPara),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                var s = JSON.parse(data.d);
                                if (s) {
                                    alert("创建成功");
                                    location.href = "user-list.html";
                                }
                                else {
                                    alert("用户名已存在");
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(XMLHttpRequest.status);
                                alert(XMLHttpRequest.readyState);
                                alert(textStatus);
                            }
                        });
                    } else {
                        alert("两次密码不一致");
                    }
                } else {
                    alert("条码不能空");
                }
               
            } else {
                alert("用户名不能为空")
            }
           
       
        });


    })();
}

$(function () {
    initConfig();
});


