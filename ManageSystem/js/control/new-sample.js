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
        if (Globals.trim($("#txtName").val()) != "" && Globals.trim($("#txtSampleCode").val()) != "") {
            var jsonPara = {
                sample: {
                    name: $("#txtName").val(),
                    samplecode: Globals.trim($("#txtSampleCode").val()),
                    sampleclass: $("#sltSampleClass").val(),
                    description: $("#textDescription").val(),
                    CreatedBy: createdby
                }
            }
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: Globals.ServiceUrl + "CreateSample",
                data: JSON.stringify(jsonPara),
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("创建成功");
                        location.href = "sample-list.html";
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
            alert("样品名和条码不能为空");
            return false;
        }
      
    })
})