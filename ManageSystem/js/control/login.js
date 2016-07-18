var userJs, userVar;

function initConfig() {
    //初始化模块JS
    userJs = new Globals.user();

    //页面变量
    userVar = {
    };

    (function () {
        $(".forgot").click(function () {
            alert("请联系管理员")
        })
        $(".login").click(function () {
            var _username = $("#txtUsername").val();
            var _password = $("#txtPassword").val();
            var jsonPara = JSON.stringify({
                user: {
                    username: _username,
                    password: hex_md5(_password)  //MD5加密
                }
            });
            $("#txtPassword").val(hex_md5(_password));

            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "Login",
                data: jsonPara,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var user = JSON.parse(data.d);

                    if (user.SystemUserId != null && user.SystemUserId != "") {
                        var Days = 7;
                        var exp = new Date();
                        exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);

                        //document.cookie="SystemId="+user.SystemUserId+";"+"UserName="+user.UserName+";"+"Name="+user.Name+";"+
                        //                 "StateCode=" + user.StateCode + ";" + "RoleId=" + user.RoleId + ";" + "expires=" + exp.toGMTString();

                        document.cookie = "SystemUserId=" + user.SystemUserId;
                        document.cookie = "UserName=" + user.Username;
                        document.cookie = "Name=" + user.Name;
                        document.cookie = "RoleId=" + user.RoleId;
                        document.cookie = "Num=" + "1";
                        document.cookie = "expirse=" + exp.toGMTString();
                       
                    }
                    else {
                        alert("用户名或密码错误");
                        $("#txtPassword").val("");
                    };
                    jsonPar = {
                        roleid: user.RoleId
                    };
                    $.ajax({
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        url: Globals.ServiceUrl + "GetRolePrivilegeList",
                        data: JSON.stringify(jsonPar),
                        success: function (data) {
        
                            document.cookie = "privilege=" + data.d;
                            location.href = "index.html";
                        
                        }
                    })
                }
             
            });
        });
    })();
}

$(function () {
    initConfig();
});


