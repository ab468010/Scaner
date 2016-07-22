if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
$(function () {
    var id = $.getUrlParam("id");
    $("#myModal .modal-body").load("child/edit-contact.html");
    if (id != null && id != "") {
        var jsonPar = {
            contactid: id
        }
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            url: Globals.ServiceUrl + "Scontactid",
            data: JSON.stringify(jsonPar),
            success: function (data) {
                var s = JSON.parse(data.d);
                $("#tleName").text(s.Name);
                $("#txtName").val(s.Name);
                $("#sp1").text(s.Customername);
                $("#sp2").text(s.Description);
                if (s.StateCode ==1) {
                    $("#sp3").text("启用");
                } else {
                    $("#sp3").text("禁用");
                }
               
            }, error: function (xhr) {
                alert("请联系管理员");
                return false;
            }
        })
    }
    $(".delete1").click(function () {
        if (confirm("确定删除?")) {
            var jsonPar = {
                contactid:id
            }
              $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "Delete",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(jsonPara),
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s == true) {
                                alert("删除成功")
                                window.location.reload();
                            } else { alert("删除失败"); return false;}
                        }
                    })
        }
    })
    $("#savechange").click(function () {
        if (Globals.trim($("#txtName").val()) != "") {
            var jsonPara = {
                contact: {
                    contactid: id,
                    name: $("#txtName").val()
                }
            }
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: Globals.ServiceUrl + "UpdateContact",
                data: JSON.stringify(jsonPara),
                success: function (data) {
                    alert("更新成功");
                    window.location.reload();
                }, error: function (xhr) {
                    alert("请联系管理员");
                    return false;
                }
            })
        } else {
            alert("姓名不能为空");
            return false;
        }
     
    })
    $(".success").click(function () {
        location.href = "new-contact.html";
    })
})