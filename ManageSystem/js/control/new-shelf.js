if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var createdby = Globals.getCookie("SystemUserId");
$(function () {
    $("#login").click(function () {
        if (confirm("确定注销？")) {
            location.href = "login.html";
        }
    });
    $("#btnSave").click(function () {
        if (Globals.trim($("#shelfName").val()) != "" && Globals.trim($("#shelfcode").val()) != "") {
            var jsonPar = {
                shelf: {
                    name: $("#shelfName").val(),
                    shelfcode: Globals.trim($("#shelfcode").val()),
                    description: $("#description").val(),
                    CreatedBy: createdby
                }
            }
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: Globals.ServiceUrl + "CreateShelf",
                data: JSON.stringify(jsonPar),
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("创建成功")
                        location.href = "shelf-list.html";
                    } else {
                        alert("条码已存在");
                        return false;
                    }
                }, error: function (xhr) {
                    alert("请联系管理员");
                    return false;
                }
            })
        } else {
            alert("货架和条码不能为空");
            return false;
        }
      
    })


})