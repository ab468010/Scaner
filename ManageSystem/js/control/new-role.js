if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var createdby = Globals.getCookie("SystemUserId");
function nrole() {

    (function () {
        $("#btnSave").click(function () {
            if (Globals.trim($("#Name").val()) != "") {
                var jsnPar = {
                    role: {
                        rolename: $("#Name").val(),
                        description: $("#Description").val(),
                        CreatedBy:createdby
                    }
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "CreateRole",
                    data: JSON.stringify(jsnPar),
                    dataType: "json",
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        if (s) {
                            alert("创建成功");
                            location.href = "role-list.html";
                        } else {
                            alert("创建失败");
                            return false;
                        }
                    }, error: function (xhr) {
                        alert("请联系管理员");
                        return false;
                    }
                })
            } else {
                alert("角色名不能空");
                return false;
            }
     
        })
    })()
}
$(function () {
    nrole();
})