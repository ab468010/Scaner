if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
if ($.getUrlParam("page") == null || $.getUrlParam("page") == undefined) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}
var privilege = JSON.parse(Globals.getCookie("privilege"));
function config() {

    (function () {
        $("#btnNew").click(function () {
            location.href = "new-customer.html"
        });
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            url: Globals.ServiceUrl + "Scount",
            success: function (data) {
                var s = JSON.parse(data.d);
                var pa = Math.ceil((s.Count) / 10);
                if (pa == 0) {
                    pa=1
                }
                $("#PageNo").text(page);
                $("#totalPageNo").text(pa);
            }, error: function (xhr) {
                alert("请联系管理员");
                return false;
            }
        });
       
        Page((page - 1) * 10);
        $(".first").click(function () {
            if ($("#PageNo").text() != 1) {
                $("#PageNo").text(1);
                location.href = "customer-list.html?page=" + "1"
            }
        })
        $(".before").click(function () {
            if ($("#PageNo").text() > 1) {
                var number = parseInt($("#PageNo").text() - 1);
                $("#PageNo").text(number);
                location.href = "customer-list.html?page=" + number;
            }
        })
        $(".last").click(function () {
            if ($("#PageNo").text() < $("#totalPageNo").text()) {
                var number = parseInt($("#totalPageNo").text());
                $("#PageNo").text(number);
                location.href = "customer-list.html?page=" + number;
            }
        })
        $(".next").click(function () {
            if (($("#PageNo").text() < $("#totalPageNo").text())) {
                var number = parseInt($("#PageNo").text()) + 1;
                $("#PageNo").text(number);
                location.href = "customer-list.html?page=" + number;
            }
        })
        $(".Go").click(function () {
            if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo").val())) {
                var number = parseInt($("#pageNum").val());
                $("#PageNo").text(number);
                location.href = "customer-list.html?page=" + number;
            }
        })
      

    })();
}

$(function () {
    config();
})

function Page(num) {
    var jsonPa = {
        number:num
    }
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: Globals.ServiceUrl + "GoPage",
        data:JSON.stringify(jsonPa),
        success: function (data) {
            var s = JSON.parse(data.d);

            var tbody = $(".table tbody").empty();
            for (var i in s) {
                if (s[i].StateCode == 1) {
                    var statecode = "启用";
                } else {
                    var statecode = "禁用";
                }
                var cont = "<td name='id' style='display:none'>" + s[i].Customerid + "</td><td>" + s[i].Name + "</td><td>" + s[i].Description + "</td><td>" + "<span class='label label-success'>" + statecode + "</span>" +
                    " <ul class='actions'><li class='last'><a class='customer2 read1'style='display:none'>详情</a> <a  class='customer2 delete1'style='display:none'>删除</a></li></ul>" + "</td>"
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            };
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
            $(".delete1").click(function () {
                if (confirm("确定删除?")) {
                    var jsonPara = {
                        customerid: parseInt($(this).parent().parent().parent().parent().find("[name='id']").text())
                    }
                    $.ajax({
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(jsonPara),
                        url: Globals.ServiceUrl + "DeleteCustomer",
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s) {
                                alert("删除成功");
                                window.location.reload();
                            } else {
                                alert("请先删除联系人")
                                return false;
                            }
                        }, error: function (xhr) {
                             alert("请联系管理员");
                             return false;
                        }
                    })
                }
            });
            $(".customer2.read1").click(function () {
                var id = parseInt($(this).parent().parent().parent().parent().find("[name='id']").text())
                location.href = "customer-profile.html?id=" + id;
            })
        }
    })
}
