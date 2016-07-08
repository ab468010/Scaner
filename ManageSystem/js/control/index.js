var systemuserid = Globals.getCookie("SystemUserId");
var roleid = Globals.getCookie("RoleId");
var jsonPar = {
    systemuserId: Number(systemuserid),
    roleId: Number(roleid)
}
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
            location.href = "login.html";}
        
            var jsonPa = {
                id: 1
            }
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetContainerWarning",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(jsonPa),
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s == 1) {
                        alert("大小周转箱剩余不足");
                    } else if (s == 2) {
                        alert("小周转箱剩余不足");
                    } else if (s == 3) {
                        alert("大周转箱剩余不足");
                    } else {
                        return;
                    }

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
                url: Globals.ServiceUrl + "GetDelayProjectList",
                data:JSON.stringify(jsonPar),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var tbody = $("#table tbody").empty();
                    var s = JSON.parse(data.d);
                    for (var i in s) {
                        var cont = "<td>" + s[i].Name + "</td><td>" + s[i].ProjectNo + "</td><td>" + s[i].EngineerIdName + "</td>"
                        var row = document.createElement("tr");
                        row.innerHTML = cont;
                        tbody.append(row);
                    }

                }, error: function (xhr) {
                    alert(xhr);
                }
            });
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetDelayTaskList",
                data: JSON.stringify(jsonPar),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    var tbody = $("#table1 tbody").empty();
                    var s = JSON.parse(data.d);
                    for (var i in s) {
                        var cont = "<td>" + s[i].Name + "</td><td>" + s[i].ProjectName + "</td><td>" + s[i].Tester1IdName + "</td><td>" + s[i].Tester2IdName + "</td>";
                        var row = document.createElement("tr");
                        row.innerHTML = cont;
                        tbody.append(row);
                    }

                }, error: function (xhr) {
                    alert(xhr);
                }
            });
            //$.ajax({
            //    type: "post",
            //    url: Globals.ServiceUrl + "GetDelayTaskList",
            //    contentType: "application/json;charset=utf-8",
            //    data:JSON.stringify(jsonPar),
            //    success: function (data) {
            //        var s = JSON.parse(data.d);
                

            //    }, error: function (xhr) {
            //        alert(xhr);
            //    }
            //});
         


    })()
}

$(function () {
    index();
})
