
$(function () {


    $('#roomid').selectpicker('refresh');

    $("#btnSave").click(function () {
        if (Globals.trim($("#txtName").val()) != "" && Globals.trim($("#txtSampleCode").val()) != "") {
            var jsonPara = {
                sample: {
                    name: $("#txtName").val(),
                    samplecode: Globals.trim($("#txtSampleCode").val()),
                    sampleclass: $("#sltSampleClass").val(),
                    description: $("#textDescription").val()
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
                    }
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        } else {
            alert("样品名和条码不能为空");
        }
      
    })
})