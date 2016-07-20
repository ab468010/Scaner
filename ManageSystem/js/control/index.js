var systemuserid = Globals.getCookie("SystemUserId");
var roleid = Globals.getCookie("RoleId");
var num = Globals.getCookie("Num");
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
                    if (s == 1 && num == 1) {
                        alert("大小周转箱剩余不足");
                        document.cookie = "Num=" + "2";
                    } else if (s == 2 && num == 1) {
                        alert("小周转箱剩余不足");
                        document.cookie = "Num=" + "2";
                    } else if (s == 3 && num == 1) {
                        alert("大周转箱剩余不足");
                        document.cookie = "Num=" + "2";
                    } else {
                        return;
                    }

                }, error: function (xhr) {
                    alert(xhr);
                }
            });
            $("#login").click(function () {
                if (confirm("确定注销？")) {
                    location.href = "login.html";
                }
            });
   
        //完成的项目
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetFinishProjectCount",
                contentType: "application/json;charset=utf-8",
                data:JSON.stringify(jsonPar),
                success: function (data) {
                    var s = JSON.parse(data.d);
                  
                        $("#finishproject").text(s);                                 

                }, error: function (xhr) {
                    alert(xhr);
                }
            });
        //进行中的项目
            var jsonP = {
                systemuserId: Number(systemuserid),
                roleId: Number(roleid),
                statusCode: 2
            }
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetProjectStatusCodeCount",
                contentType: "application/json;charset=utf-8",
                data:JSON.stringify(jsonP),
                success: function (data) {
                    var s = JSON.parse(data.d);
                    $("#goingproject").text(s);
                   
                }, error: function (xhr) {
                    alert(xhr);
                }
            });   
       
    
        //未完成任务总数
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetUTaskCount",
                async: false,
                data:JSON.stringify(jsonPar),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    var maxNum = s;
                    var minNum = 0;
                    $("#Hidden1").val(s);
                    $(".knob").knob({
                        width: "120",
                        max: maxNum,
                        min: minNum,
                        thickness: .2,
                        'readonly': "readonly",
                        fgColor: '#30a1ec',
                        bgColor: "#d4ecfd",
                        "readOnly": true
                    });
                  
                }
         
            })
        //未完成项目总数
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetUProjectCount",
                async: false,
                data: JSON.stringify(jsonPar),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    $("#Hidden2").val(s);
                    var maxNumP = s;
                    var minNumP = 0;
                    $(".knobP").knob({
                        width: "120",
                        max: maxNumP,
                        min: minNumP,
                        thickness: .2,
                        'readonly': "readonly",
                        fgColor: '#30a1ec',
                        bgColor: "#d4ecfd",
                        "readOnly": true
                    });
                }
            });
        //已使用的周转箱
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
        //延误的项目
        
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetDelayProjectCount",
                contentType: "application/json;charset=utf-8",
                data:JSON.stringify(jsonPar),
                success: function (data) {
                    var s = JSON.parse(data.d);
                    var c;
                    var page = Math.ceil(s / 5);
                    if (page == 0) {
                        page=1
                    }
                    if ($("#Hidden2").val() == 0) {
                        c = 0 + "%";
                    } else {
                        c = Math.round((s / $("#Hidden2").val()) * 100) + "%";
                    }
                    
                    $("#PageNop").text(1);
                    $("#totalPageNop").text(page);
                    $("#delayproject").text(s);
                    $("#uproject").val(s).trigger('change');
                    $("#uproject").val(c);

                }, error: function (xhr) {
                    alert(xhr);
                }
            });
            Pagep(0);
            $(".firstp").click(function () {
                if ($("#PageNop").text() != 1) {
                    $("#PageNop").text(1);
                    Pagep(0);
                }
            })
            $(".beforep").click(function () {
                if ($("#PageNop").text() > 1) {
                    var number = parseInt($("#PageNop").text() - 1);
                    $("#PageNop").text(number);
                    Pagep((number - 1) * 5);
                }
            })
            $(".lastp").click(function () {
                if ($("#PageNop").text() < $("#totalPageNop").text()) {
                    var number = parseInt($("#totalPageNop").text());
                    $("#PageNop").text(number);
                    Pagep((number - 1) * 5);
                }
            })
            $(".nextp").click(function () {
                if (($("#PageNop").text() < $("#totalPageNop").text())) {
                    var number = parseInt($("#PageNop").text());
                    $("#PageNop").text(number + 1);
                    Pagep(number * 5);
                }
            })

        //延误的任务
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetDelayTaskCount",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(jsonPar),
                success: function (data) {
                    var s = JSON.parse(data.d);
                    var c;
                    var page = Math.ceil(s / 5);
                    if (page == 0) {
                        page=1
                    }
                    $("#PageNo").text(1);
                    $("#totalPageNo").text(page);
                    if ($("#Hidden1").val() == 0) {
                        c = 0 + "%";
                    } else {
                        c = Math.round(s * 100 / $("#Hidden1").val()) + "%";
                    }
                   
                    
                    $("#utask").val(s).trigger('change');
                    $("#utask").val(c)
                }
            })
        Page(0)
        $(".first").click(function () {
            if ($("#PageNo").text() != 1) {
                $("#PageNo").text(1);
                Page(0);
            }
        })
        $(".before").click(function () {
            if ($("#PageNo").text() > 1) {
                var number = parseInt($("#PageNo").text() - 1);
                $("#PageNo").text(number);
                Page((number - 1) * 5);
            }
        })
        $(".last").click(function () {
            if ($("#PageNo").text() < $("#totalPageNo").text()) {
                var number = parseInt($("#totalPageNo").text());
                $("#PageNo").text(number);
                Page((number - 1) * 5);
            }
        })
        $(".next").click(function () {
            if (($("#PageNo").text() < $("#totalPageNo").text())) {
                var number = parseInt($("#PageNo").text());
                $("#PageNo").text(number + 1);
                Page(number * 5);
            }
        })

    })()
}

$(function () {
    index();
})
function Page(page) {
    var jsonPara = {
        Page: page,
        systemuserId: Number(systemuserid),
        roleId: Number(roleid)
    }
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetDelayTaskList",
        data: JSON.stringify(jsonPara),
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
}
function Pagep(page) {
    var jsonP = {
        Page: page,
        systemuserId: Number(systemuserid),
        roleId: Number(roleid),
        Limit:5
    }
    //延误的项目列表
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetDelayProjectList",
        data: JSON.stringify(jsonP),
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
}