function index() {
    (function () {

        //var strCookie = document.cookie;
        //将多cookie切割为多个名/值对 
        //var arrCookie = strCookie.split("; ");
        //var userId;
        //遍历cookie数组，处理每个cookie对 
        //for (var i = 0; i < arrCookie.length; i++) {
        //    var arr = arrCookie[i].split("=");
        //    //找到名称为userId的cookie，并返回它的值 
        //    if ("SystemId" == arr[0]) {
        //        userId = arr[1];
        //        break;
        //    }
        //}
        if (Globals.getCookie("UserName") == null || Globals.getCookie("RoleId") == null) {
            alert("请登录");
            location.href = "login.html";
        } else {
            var jsonPar = {
                id: 1
            }
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetContainerWarning",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(jsonPar),
                success: function (data) {
                    var s = JSON.parse(data.d);
                    $("#bignumber").val(s.BigContainer);
                    $("#bigmessage").val(s.BigMessage);
                    $("#smallnumber").val(s.SmallContainer);
                    $("#smallmessage").val(s.SmallMessage);
                    //if (s != null) {
                    //    $.ajax({
                    //        type: "post",
                    //        url: Globals.ServiceUrl + "GetUseBigContainer",
                    //        contentType: "application/json;charset=utf-8",
                    //        success: function (data) {
                    //            var s = JSON.parse(data.d);
                    //            s = s * 10
                    //            if (s > $("#bignumber").val()) {
                    //                alert($("#bigmessage").val())
                    //            }
                    //        }, error: function (xhr) {
                    //            alert(xhr);
                    //        }
                    //    });
                   // }
                   

                }, error: function (xhr) {
                    alert(xhr);
                }
            })

            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetDelayTaskCount",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    var count = s 
                    $("#utask").val(count).trigger('change');

                }
            })
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetFinishProjectCount",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                  
                        $("#finishproject").text(s);
                   
                   

                }, error: function (xhr) {
                    alert(xhr);
                }
            });
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetGoingProjectCount",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    $("#goingproject").text(s);
                   

                }, error: function (xhr) {
                    alert(xhr);
                }
            });
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetDelayProjectCount",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    $("#delayproject").text(s);
                    $("#uproject").val(s).trigger('change');

                }, error: function (xhr) {
                    alert(xhr);
                }
            });
      
         
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetUseBigContainer",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    
                   
                    //if (s / ($("#containercount").val()) * 100 > 70) {
                    //    alert("小周转箱数量不足")
                    //}
                }, error: function (xhr) {
                    alert(xhr);
                }
            });
         
            
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetUseContainerCount",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    $("#usecontainer").text(s);
                }, error: function (xhr) {
                    alert(xhr);
                }
            });
        }


    })()
}

$(function () {
    index();
})
