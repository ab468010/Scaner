
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
                var page = Math.ceil((s.Count) / 10);
                $("#PageNo").text(1);
                $("#totalPageNo").text(page);
            }, error: function (xhr) {
                alert(xhr);
            }
        })
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            url: Globals.ServiceUrl + "Selectcustomer",
            async: false,
            success: function (data) {
                var s = JSON.parse(data.d);
                
                var tbody = $(".table tbody").empty();
                for (var i in s) {
                    if (s[i].StateCode == 1) {
                        var statecode = "启用";
                    } else {
                        var statecode = "禁用";
                    }
                    var cont = "<td name='id' style='display:none'>" + s[i].Customerid + "</td><td>" + s[i].Name + "</td><td>" + s[i].Description + "</td><td>" +"<span class='label label-success'>"+ statecode+"</span>"+
                        " <ul class='actions'><li class='last'><a class='customer2 edit1'>编辑</a> <a  class='customer2 delete1'>删除</a></li></ul>"+"</td>"
                    var row = document.createElement("tr");
                    row.innerHTML = cont;
                    tbody.append(row);
                }
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
                                }
                            }, error: function (xhr) {
                                alert(xhr);
                            }
                        })
                    }
                })
                $(".edit1").click(function () {
                    var id = parseInt($(this).parent().parent().parent().parent(). find("[name='id']").text())
                    location.href = "customer-profile.html?id=" + id;
                })
            }
        })

        $(".next").click(function () {
            if ($("#PageNo").text() < $("#totalPageNo").text()) {
                var a = parseInt($("#PageNo").text());
                var b=a*10;
                var jsonPara = {
                    number:b
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "GoPage",
                    async: false,
                    data: JSON.stringify(jsonPara),
                    success: function (data) {
                        $("#PageNo").text(a+1);
                        var s = JSON.parse(data.d);
                        var tbody = $(".table tbody").empty();
                        for (var i in s) {
                            if (s[i].StateCode == 1) {
                                var statecode = "启用";
                            } else {
                                var statecode = "禁用";
                            }
                            var cont = "<td name='id' style='display:none'>" + s[i].Customerid + "</td><td>" + s[i].Name + "</td><td>" + s[i].Description + "</td><td>" + "<span class='label label-success'>" + statecode + "</span>" +
                                 " <ul class='actions'><li class='last'><a class='customer2 edit1'>编辑</a> <a  class='customer2 delete1'>删除</a></li></ul>" + "</td>"
                            var row = document.createElement("tr");
                            row.innerHTML = cont;
                            tbody.append(row);
                        }
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
                                        }
                                    }, error: function (xhr) {
                                        alert(xhr);
                                    }
                                })
                            }
                        })
                        $(".edit1").click(function () {
                            var id = parseInt($(this).parent() .parent().parent().parent(). find("[name='id']").text())
                            location.href = "customer-profile.html?id=" + id;
                        })
                    }
                })
            }
        })
        $(".before").click(function () {
            if ($("#PageNo").text() >1 ){
                var a = parseInt($("#PageNo").text());
                var b = (a - 2) * 10;
                var jsonPara = {
                    number:b
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "GoPage",
                    async: false,
                    data: JSON.stringify(jsonPara),
                    success: function (data) {
                        $("#PageNo").text(a-1);
                        var s = JSON.parse(data.d);
                        var tbody = $(".table tbody").empty();
                        for (var i in s) {
                            if (s[i].StateCode == 1) {
                                var statecode = "启用";
                            } else {
                                var statecode = "禁用";
                            }
                            var cont = "<td name='id' style='display:none'>" + s[i].Customerid + "</td><td>" + s[i].Name + "</td><td>" + s[i].Description + "</td><td>" + "<span class='label label-success'>" + statecode + "</span>" +
                                 " <ul class='actions'><li class='last'><a class='customer2 edit1'>编辑</a> <a  class='customer2 delete1'>删除</a></li></ul>" + "</td>"
                            var row = document.createElement("tr");
                            row.innerHTML = cont;
                            tbody.append(row);
                        }
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
                                        }
                                    }, error: function (xhr) {
                                        alert(xhr);
                                    }
                                })
                            }
                        })
                        $(".edit1").click(function () {
                            var id = parseInt($(this).parent().parent().parent().parent().find("[name='id']").text())
                            location.href = "customer-profile.html?id=" + id;
                        })
                    }
                })
            }
        })
        $(".Go").click(function () {
            if ($("#pageNum").val() >= 1 && $("#pageNum").val() <= $("#totalPageNo").text()) {
                var a = parseInt($("#pageNum").val())
                var b=(a-1)*10
                var jsonPara = {
                    number:b
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "GoPage",
                    async: false,
                    data: JSON.stringify(jsonPara),
                    success: function (data) {
                        $("#PageNo").text($("#pageNum").val());
                        var s = JSON.parse(data.d);
                        var tbody = $(".table tbody").empty();
                        for (var i in s) {
                            if (s[i].StateCode == 1) {
                                var statecode = "启用";
                            } else {
                                var statecode = "禁用";
                            }
                            var cont = "<td name='id' style='display:none'>" + s[i].Customerid + "</td><td>" + s[i].Name + "</td><td>" + s[i].Description + "</td><td>" + "<span class='label label-success'>" + statecode + "</span>" +
                                " <ul class='actions'><li class='last'><a class='customer2 edit1'>编辑</a> <a  class='customer2 delete1'>删除</a></li></ul>" + "</td>"
                            var row = document.createElement("tr");
                            row.innerHTML = cont;
                            tbody.append(row);
                        }
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
                                        }
                                    }, error: function (xhr) {
                                        alert(xhr);
                                    }
                                })
                            }
                        })
                        $(".edit1").click(function () {
                            var id = parseInt($(this).parent().parent().parent().parent().find("[name='id']").text())
                            location.href = "customer-profile.html?id=" + id;
                        })
                    }
                })
            }
        })
        $(".first").click(function () {
            if ($("#PageNo").text() != 1) {
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "Selectcustomer",
                    async: false,
                    success: function (data) {
                        $("#PageNo").text(1);
                        var s = JSON.parse(data.d);
                        var tbody = $(".table tbody").empty();
                        for (var i in s) {
                            if (s[i].StateCode == 1) {
                                var statecode = "启用";
                            } else {
                                var statecode = "禁用";
                            }
                            var cont = "<td name='id' style='display:none'>" + s[i].Customerid + "</td><td>" + s[i].Name + "</td><td>" + s[i].Description + "</td><td>" + "<span class='label label-success'>" + statecode + "</span>" +
                      " <ul class='actions'><li class='last'><a class='customer2 edit1'>编辑</a> <a  class='customer2 delete1'>删除</a></li></ul>" + "</td>"
                            var row = document.createElement("tr");
                            row.innerHTML = cont;
                            tbody.append(row);
                        }
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
                                        }
                                    }, error: function (xhr) {
                                        alert(xhr);
                                    }
                                })
                            }
                        })
                        $(".edit1").click(function () {
                            var id = parseInt($(this).parent().parent().parent().parent(). find("[name='id']").text())
                            location.href = "customer-profile.html?id=" + id;
                        })
                    }
                })
            }
          
        })
        $(".last").click(function () {
            if ($("#PageNo").text() != $("#totalPageNo").text()) {
                var a=parseInt($("#totalPageNo").text())
                var b=(a-1)*10
                var jsonPara = {
                    number:b
                }
                $.ajax({
                    type: "post",
                    data:JSON.stringify(jsonPara),
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "GoPage",
                    async: false,
                    success: function (data) {
                        $("#PageNo").text($("#totalPageNo").text());
                        var s = JSON.parse(data.d);
                        var tbody = $(".table tbody").empty();
                        for (var i in s) {
                            if (s[i].StateCode == 1) {
                                var statecode = "启用";
                            } else {
                                var statecode = "禁用";
                            }
                            var cont = "<td name='id' style='display:none'>" + s[i].Customerid + "</td><td>" + s[i].Name + "</td><td>" + s[i].Description + "</td><td>" + "<span class='label label-success'>" + statecode + "</span>" +
                                " <ul class='actions'><li class='last'><a class='customer2 edit1'>编辑</a> <a  class='customer2 delete1'>删除</a></li></ul>" + "</td>"
                            var row = document.createElement("tr");
                            row.innerHTML = cont;
                            tbody.append(row);
                        }
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
                                        }
                                    }, error: function (xhr) {
                                        alert(xhr);
                                    }
                                })
                            }
                        })
                        $(".edit1").click(function () {
                            var id = parseInt($(this).parent().parent().parent().parent().find("[name='id']").text())
                            location.href = "customer-profile.html?id=" + id;
                        })
                    }
                })
            }
        })

    })();
}

$(function () {
    config();
})


