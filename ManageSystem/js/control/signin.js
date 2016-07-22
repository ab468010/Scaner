
function log() {

    (function () {
        $("#aaa").click(function () {
            var jsonPar = {
                user: {
                    username: $("#txtUsername").val(),
                    password: $("#txtPassword").val()
                }
            }
            $.ajax({
                contentType: "application/json; charset=utf-8",
                type: "post",
                url: Globals.ServiceUrl + "log",
                data: JSON.stringify(jsonPar),
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s.Username != null && s.Password != null) {
                        location.href = "user-list.html"
                    } else alert("用户名或密码错误")
                }, error: function (xhr) {
                     alert("请联系管理员");
                }
            })
        })
    })();
}
$(function () {
    log();
})