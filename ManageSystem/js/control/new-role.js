var createdby= Globals.getCookie("SystemUserId");
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
                        }
                    }, error: function (xhr) {
                        alert(xhr);
                    }
                })
            } else {
                alert("角色名不能空")
            }
     
        })
    })()
}
$(function () {
    nrole();
})