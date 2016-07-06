function warning() {
    (function () {
        var jsonPar = {
            id:1
        }
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetContainerWarning",
            contentType: "application/json;charset=utf-8",
            data:JSON.stringify(jsonPar),
            success: function (data) {
                var s = JSON.parse(data.d);
                $("#bignumber").val(s.BigContainer);
                $("#bigmessage").val(s.BigMessage);
                $("#smallnumber").val(s.SmallContainer);
                $("#smallmessage").val(s.SmallMessage);
                $("#id").val(s.ContainerId)
            }, error: function (xhr) {
                alert(xhr);
            }
        })
        $(".containerwarning2").click(function () {
            var jsonPara = {
                containerwarning: {
                    BigContainer: $("#bignumber").val(),
                    BigMessage:  $("#bigmessage").val(),
                    SmallContainer :$("#smallnumber").val(),
                    SmallMessage: $("#smallmessage").val(),
                    ContainerId: $("#id").val()
                }
            }
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "UpdateContainerWarning",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(jsonPara),
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("创建成功")
                        window.location.reload();
                    } else {
                        alert("创建失败")
                    }
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        })
    })()
}
$(function () {
    warning();
})