if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
function warning() {
    (function () {
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        })
        var jsonPa = {
            id:1
        }
     
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetContainerWarningCount",
            contentType: "application/json;charset=utf-8",
            data:JSON.stringify(jsonPa),
            success: function (data) {
                var s = JSON.parse(data.d);
                $("#bigcontainer").val(s.BigContainer);                
                $("#smallcontainer").val(s.SmallContainer);
                $("#id").val(s.ContainerId)
            }, error: function (xhr) {
                alert("请联系管理员");
                return false;
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
                alert("请联系管理员");
                return false;
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
                    alert("请联系管理员");
                    return false;
                }
            })
        })
    })()
}
$(function () {
    warning();
})