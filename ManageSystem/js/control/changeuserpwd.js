var id = $.getUrlParam("id");
function changeuserpwd() {
    (function () {
        var jsonPa = {
            userId:id
        }
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetUserById",
            data: JSON.stringify(jsonPa),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                $("#Username").val(s.Username);
            }, error: function (xhr) {
                alert(xhr);
            }
        })
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
                                alert("更改成功");
                                location.href = "user-list.html";
                            }
                            else {
                                alert("错误");
                            }
                        }
                
                    });
                } else {
                    alert("两次密码不一致");
                    $("#pwdPassword").val("");
                    $("#pwdAgain").val("");
                }
            } else {
                alert("密码不能为空");

            }


        });
    })()
}
$(function () {
    changeuserpwd();
})