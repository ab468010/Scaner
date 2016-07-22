if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
$(function () {
    $("#login").click(function () {
        if (confirm("确定注销？")) {
            location.href = "login.html";
        }
    });
    $("#btnSave").click(function () {
        if (Globals.trim($("#txtName").val()) == "") {
            alert("姓名不能为空")
        } else {
            var jsonPara = {
                customer: {
                    name: $("#txtName").val(),
                    description: $("#Description").val(),
                    statecode: $("#Select1").val()
                }
            }
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "InCustomer",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(jsonPara),
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("添加成功");
                        location.href="customer-list.html"
                    } else {
                        alert("添加失败");
                        return false;
                    }
                }, error: function (xhr) {
                    alert("请联系管理员");
                    return false;
                }
            })
        }

    })

})