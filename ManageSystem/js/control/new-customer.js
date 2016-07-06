$(function () {
    
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
                        alert("添加成功")
                    } else {
                        alert("添加失败")
                    }
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        }

    })

})