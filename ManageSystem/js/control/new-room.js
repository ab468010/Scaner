var createdby = Globals.getCookie("SystemUserId");
function newroom() {
    (function () {
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $("#btnSave").click(function () {
            if (Globals.trim($("#name").val()) != "" && Globals.trim($("#roomcode").val()) != "") {
                var jsonPar = {
                    room: {
                        name: $("#name").val(),
                        roomcode: Globals.trim($("#roomcode").val()),
                        CreatedBy: createdby

                    }
                }
                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "CreateRoom",
                    //async: false,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(jsonPar),
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        if (s) {
                            alert("创建成功");
                            location.href = "room-list.html";
                        } else {
                            alert("条码已存在");
                        }
                    }, error: function (xhr) {
                        alert(xhr);
                    }
                })
            } else {
                alert("场地和条码不能为空");
            }
      
        })
 
    })()
}
$(function () {
    newroom()
})