var createdby = Globals.getCookie("SystemUserId");
$(function () {
    $("#login").click(function () {
        if (confirm("确定注销？")) {
            location.href = "login.html";
        }
    });
    $("#btnSave").click(function () {
        if (Globals.trim($("#txtName").val()) != "" && Globals.trim($("#txtContainerCode").val()) != "") {
            var jsonPara = {
                container: {
                    Name: $("#txtName").val(),
                    ContainerCode: Globals.trim($("#txtContainerCode").val()),
                    Size: $("#txtSize").val(),
                    Description: $("#textDescription").val(),
                    CreatedBy: createdby
                }
            }
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: Globals.ServiceUrl + "CreateContainer",
                data: JSON.stringify(jsonPara),
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("创建成功");
                        location.href = "container-list.html";
                    } else {
                        alert("条码已存在");
                    }
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        } else {
            alert("周转箱和条码不能空");
        }
 
    })
})