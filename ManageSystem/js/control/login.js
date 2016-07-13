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
                        location.href = "index.html";
                    }
                    else
                    {
                        alert("没有该用户");
                        $("#txtPassword").val("");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });
        });
    })();
}

$(function () {
    initConfig();
});


