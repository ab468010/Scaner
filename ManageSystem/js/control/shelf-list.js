var page;
var roleid = Globals.getCookie("RoleId");
if ($.getUrlParam("page") == null || $.getUrlParam("page") == undefined) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}

$(function () {
    $("#myModal .modal-body").load("child/edit-shelf.html");
    $("#login").click(function () {
        if (confirm("确定注销？")) {
            location.href = "login.html";
        }
    });
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetShelfCount",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var s = JSON.parse(data.d);
            if (s == 0) {
                s=1
            }
            var p = Math.ceil(s / 10);
            $("#PageNo").text(page);
            $("#totalPageNo").text(p);
        }, error: function (xhr) {
            alert(xhr);
        }
    })
    Page((page-1)*10);
    $(".first").click(function () {
        if ($("#PageNo").text() != 1) {
            $("#PageNo").text(1);
            location.href = "shelf-list.html?page=" + "1"
                 
            //Page((page-1)*10);
        }
    })
    $(".before").click(function () {
        if ($("#PageNo").text() > 1) {
            var number = parseInt($("#PageNo").text())-1;
            $("#PageNo").text(number);
            location.href = "shelf-list.html?page=" + number;
                  
            //Page((page - 2) * 10);
        }
    })
    $(".last").click(function () {
        if ($("#PageNo").text() < $("#totalPageNo").text()) {
            var number = parseInt($("#totalPageNo").text());
            $("#PageNo").text(number);
            location.href = "shelf-list.html?page=" + number;
          
           // Page((page - 1) * 10);
        }
    })
    $(".next").click(function () {
        if (($("#PageNo").text() < $("#totalPageNo").text())) {
            var number = parseInt($("#PageNo").text())+1;
            $("#PageNo").text(number);
            location.href = "shelf-list.html?page=" +number ;
          
           // Page(page * 10);
        }
    })
    $(".Go").click(function () {
        if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo").val())) {
            var number = parseInt($("#pageNum").val());
            $("#PageNo").text(number);
            location.href = "shelf-list.html?page=" + number;
           
           // Page((number - 1) * 10);
        }
    })
})

function Page(page) {
    var jsonPar = {
        number:page
    }
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetShelf",
        
        data:JSON.stringify(jsonPar),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var cont = "<td>" + s[i].Name + "</td><td>" + s[i].ShelfCode + "</td><td>" + s[i].Description +
                    " <ul class='actions'><li class='last'><a href='#myModal' data-toggle='modal' class='shelf2 edit1'>编辑</a> <a class='shelf2 delete1'>删除</a></li></ul>" + "</td><td style='display:none' name='shelfid'>" + s[i].ShelfId + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }
          
            $(".edit1").click(function () {
                var jsonPara = {
                    shelfid: $(this).parent().parent().parent().parent().find("[name='shelfid']").text()
                }
                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "SelectShelf",
                    //async: false,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(jsonPara),
                    success: function (data) {
                        var sh = JSON.parse(data.d);
                        $("#shelfname").val(sh.Name);
                        $("#shelfcode").val(sh.ShelfCode);
                        $("#description").val(sh.Description);
                        $("#shelfid").val(sh.ShelfId);
                    }, error: function (xhr) {
                        alert(xhr);
                    }
                })
            })
            $("#savechange").click(function () {
                if (Globals.trim($("#shelfcode").val())!= "" && Globals.trim($("#shelfname").val())!="") {
                    var jsonPara = {
                        shelf: {
                            shelfid: $("#shelfid").val(),
                            name: $("#shelfname").val(),
                            shelfcode: Globals.trim($("#shelfcode").val()),
                            description: $("#description").val()
                        }
                    }
                    $.ajax({
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        url: Globals.ServiceUrl + "UpdateShelf",
                        data: JSON.stringify(jsonPara),
                        dataType: "json",
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s) {
                                alert("更新成功");
                                window.location.reload();
                            } else {
                                alert("更新失败");
                            }
                        }, error: function (xhr) {
                            alert(xhr);
                        }
                    })
                } else {
                    alert("货架和条码不能空");
                }
              
            })

            $(".delete1").click(function () {
                if (confirm("删除吗？")) {
                    var jsonPa = {
                        shelfid: $(this).parent().parent().parent().parent().find("[name='shelfid']").text()
                    }
                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "DeleteShelf",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(jsonPa),
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s) {
                                alert("删除成功");
                                window.location.reload();
                            } else {
                                alert("删除失败");
                            }
                        }, error: function (xhr) {
                            alert(xhr);
                        }
                    })
                }

            })
        }

    })
}