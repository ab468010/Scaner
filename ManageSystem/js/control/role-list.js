if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var page;
var privilege = JSON.parse(Globals.getCookie("privilege"));
var roleid = Globals.getCookie("RoleId");
if ($.getUrlParam("page") == null || $.getUrlParam("page") == undefinedd) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}
function rolelist() {
    (function () {
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetRoleCount",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                if (s == 0) {
                    s=1
                }
                var pa = Math.ceil(s /10);
                $("#PageNo").text(page);
                $("#totalPageNo").text(pa);
            }, error: function (xhr) {
                alert("请联系管理员");
                return false;
            }
        })
        Page((page-1)*10);
        $(".first").click(function () {
            if ($("#PageNo").text() != 1) {
                $("#PageNo").text(1);
                location.href = "role-list.html?page=" + "1";
            }
        })
        $(".before").click(function () {
            if ($("#PageNo").text() > 1) {
                var number = parseInt($("#PageNo").text() - 1);
                $("#PageNo").text(number);
                location.href = "role-list.html?page=" + number;
            }
        })
        $(".last").click(function () {
            if ($("#PageNo").text() < $("#totalPageNo").text()) {
                var number = parseInt($("#totalPageNo").text());
                $("#PageNo").text(number);
                location.href = "role-list.html?page=" + number;
            }
        })
        $(".next").click(function () {
            if (($("#PageNo").text() < $("#totalPageNo").text())) {
                var number = parseInt($("#PageNo").text())+1;
                $("#PageNo").text(number);
                location.href = "role-list.html?page=" + number;
            }
        })
        $(".Go").click(function () {
            if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo").val())) {
                var number = parseInt($("#pageNum").val());
                $("#PageNo").text(number);
                location.href = "role-list.html?page=" + number;
            }
        })
           
    })()
}

$(function () {
    rolelist()
})

function Page(p) {
    var jsonPar = {
        number:p
    }
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: Globals.ServiceUrl + "GetPageRoleList",
        
        data:JSON.stringify(jsonPar),
        success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var cont = "<td>" + s[i].RoleName + "</td><td>" + s[i].Description
                + "<ul class='actions'><li class='last'><a class='role2 edit1'style='display:none'>详情</a> <a class='role2 delete1'style='display:none'>删除</a></li></ul>" + "</td><td name='roleid'style='display:none'>" + s[i].RoleId + "</td>"
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }
            for (var i in privilege) {
                if (privilege[i].CanDelete == true) {
                    $("." + privilege[i].Tablename + 2 + ".delete1").attr({ style: "display:inline" });
                }
                if (privilege[i].CanCreate == true) {
                    $("." + privilege[i].Tablename + 2 + ".create1").attr({ style: "display:inline" });
                }
                if (privilege[i].CanWrite == true) {
                    $("." + privilege[i].Tablename + 2 + ".edit1").attr({ style: "display:inline" });
                }
                if (privilege[i].CanRead == true) {
                    $("." + privilege[i].Tablename + 2 + ".read1").attr({ style: "display:inline" });

                }
            };
            $(".edit1").click(function () {
                location.href = "role-profile.html?id=" + $(this).parent().parent().parent().parent().find("[name='roleid']").text()
            })
            $(".delete1").click(function () {
                var jsonPara = {
                    roleid: $(this).parent().parent().parent().parent().find("[name='roleid']").text()
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "GetRoleType",
                    data:JSON.stringify(jsonPara),
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        if (s == 1) {
                            alert("系统角色不能删除");
                            return false;
                        } else {
                            if (confirm("确定删除?")) {
                            
                                $.ajax({
                                    type: "post",
                                    contentType: "application/json; charset=utf-8",
                                    data: JSON.stringify(jsonPara),
                                    url: Globals.ServiceUrl + "DeleteRole",
                                    success: function (data) {
                                        var s = JSON.parse(data.d);
                                        if (s) {
                                            alert("删除成功");
                                            location.href = "role-list.html";
                                        } else {
                                            alert("请先删除用户");
                                            return false;
                                        }
                                    }, error: function (xhr) {
                                        alert("请联系管理员");
                                        return false;
                                    }
                                })
                            }     
                        }
                    }, error: function (xhr) {
                        alert("请联系管理员");
                        return false;
                    }
                })             
            })
          }

         })       
        
    }
