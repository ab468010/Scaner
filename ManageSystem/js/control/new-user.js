if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var userJs, userVar;
var action = $.getUrlParam("action");
var id = $.getUrlParam("id");
var invokeMethod = action == "Create" ? "CreateUser" : "0"
var createdby = Globals.getCookie("SystemUserId");

function initConfig() {
    //初始化模块JS
    userJs = new Globals.user();

    //页面变量
    userVar = {

    };

    (function () {
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
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
                alert("请联系管理员");
                return false;
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
                                Description: $("#Description").val(),
                                CreatedBy:createdby
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
                                if (s==3) {
                                    alert("创建成功");
                                    location.href = "user-list.html";
                                }else if(s==1){
                                    alert("用户名已存在");
                                    return false;
                                } else if (s == 2) {
                                    alert("条码已存在");
                                    return false;
                                } else {
                                    alert("创建失败");
                                    return false;
                                }
            
                            }, error: function (xhr) {
                                alert("请联系管理员");
                                return false;
                            }
                        });
                    } else {
                        alert("两次密码不一致");
                        $("#pwdPassword").val("");
                        $("#pwdAgain").val("");
                        return false;
                    }
                } else {
                    alert("条码不能空");
                    return false;
                }
               
            } else {
                alert("用户名不能为空");
                return false;
            }
                 
        });


    })();
}

$(function () {
    initConfig();
});


