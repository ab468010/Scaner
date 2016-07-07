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
                $("#bigcontainer").val(s.BigContainer);                
                $("#smallcontainer").val(s.SmallContainer);
                $("#id").val(s.ContainerId)
            }, error: function (xhr) {
                alert(xhr);
            }
        })
     
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetBigContainer",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var s=JSON.parse(data.d);
                $("#bigcount").val(s);
            }
            
        })
   
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetSmallContainer",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                $("#smallcount").val(s);
            }, error: function (xhr) {
                alert(xhr);
            }

        })
        $(".containerwarning2").click(function () {

            var jsonPara = {
                containerwarning: {
                    BigContainer: $("#bigcontainer").val(),
                    SmallContainer :$("#smallcontainer").val(),             
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