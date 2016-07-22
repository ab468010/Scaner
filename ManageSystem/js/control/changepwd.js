if ( Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}

function changepwd() {
  
    (function () {
            $("#Username").val((Globals.getCookie("UserName")));
       
        
       
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $("#btnSave").click(function () {
            if (Globals.trim($("#pwdPassword").val()) != "") {
                if ($("#pwdPassword").val() == $("#pwdAgain").val()) {
                    jsonPara = {
                        user: {
                            Username: $("#Username").val(),
                            Password: hex_md5($("#pwdPassword").val()),

                        }
                    }

                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "ChangeUserPwd",
                        data: JSON.stringify(jsonPara),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d) {
                                alert("更改成功请重新登录");
                                location.href = "login.html";
                            }
                            else {
                                alert("错误");
                                return false;
                            }
                        }
                    
                    });
                } else {
                    alert("两次密码不一致");
                    $("#pwdPassword").val("");
                    $("#pwdAgain").val("");
                    return false;
                }
            } else {
                alert("密码不能为空");
                return false;
            }
     

        });

    })()
}

$(function () {
    changepwd();
})