if ($.getUrlParam("page") == null || $.getUrlParam("page") == undefined) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}
var roleid = Globals.getCookie("RoleId");
$(function () {
  
    $("#myModal .modal-body").load("child/edit-contact.html");
    $(".success").click(function () {
        location.href = "new-contact.html";
    });
    $("#login").click(function () {
        if (confirm("确定注销？")) {
            location.href = "login.html";
        }
    });
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetContactCount",
        async:false,
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
            location.href="contact-list.html?page="+"1"
        }
    })
    $(".before").click(function () {
        if ($("#PageNo").text() > 1) {
            var number = parseInt($("#PageNo").text() - 1);
            $("#PageNo").text(number);
            location.href = "contact-list.html?page=" + number;
        }
    })
    $(".last").click(function () {
        if ($("#PageNo").text() < $("#totalPageNo").text()) {
            var number = parseInt($("#totalPageNo").text());
            $("#PageNo").text(number);
            location.href = "contact-list.html?page=" + number;
        }
    })
    $(".next").click(function () {
        if (($("#PageNo").text() < $("#totalPageNo").text())) {
            var number = parseInt($("#PageNo").text())+1;
            $("#PageNo").text(number);
            location.href = "contact-list.html?page=" + number;
        }
    })
    $(".Go").click(function () {
        if(($("#totalPageNo").text()>=$("#pageNum").val() >= 1 && $("#pageNum").val()!=$("#PageNo").val())){
            var number = parseInt($("#pageNum").val());
            $("#PageNo").text(number);
            location.href = "contact-list.html?page=" + number;
        }
    })
   
});

function Page(pa) {
    var jsonPar = {
        number: pa
    }
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "Selectcontact",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(jsonPar),
        success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var cont = "<td name='contactid' style='display:none'>" + s[i].Contactid + "</td><td>" + s[i].Name + "</td><td>" + s[i].Description + "</td><td>" + s[i].Customername +
                    " <ul class='actions'><li class='last'><a class='contact2 edit1'style='display:none' href='#myModal' data-toggle='modal'>编辑</a>  <a class='contact2 delete1'style='display:none' >删除</a></li></ul>" + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }
            json = {
                roleid: roleid
            };
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                //async: false,
                url: Globals.ServiceUrl + "GetRolePrivilegeList",
                data: JSON.stringify(jsonPar),
                success: function (data) {
                    var s = JSON.parse(data.d);

                    for (var i in s) {
                        if (s[i].CanDelete == true) {
                            $("." + s[i].Tablename + 2 + ".delete1").attr({ style: "display:inline" });
                        }
                        if (s[i].CanCreate == true) {
                            $("." + s[i].Tablename + 2 + ".create1").attr({ style: "display:inline" });
                        }
                        if (s[i].CanWrite == true) {
                            $("." + s[i].Tablename + 2 + ".edit1").attr({ style: "display:inline" });
                        }
                        if (s[i].CanRead == true) {
                            $("." + s[i].Tablename + 2 + ".read1").attr({ style: "display:inline" });
                        }
                    }
                }
            });
            $(".delete1").click(function () {
                if (confirm("确定删除？")) {
                    var jsonPara = {
                        contactid: parseInt($(this).parent().parent().parent().parent().find("[name='contactid']").text())
                    }
                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "DeleteContact",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(jsonPara),
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s == true) {
                                alert("删除成功")
                                window.location.reload();
                            } else { alert("删除失败") }
                        }
                    })
                }
            })
            $(".contact2.edit1").click(function () {
                var jsonPa = {
                    contactid: parseInt($(this).parent().parent().parent().parent().find("[name='contactid']").text())
                }

                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                   //async: false,
                    url: Globals.ServiceUrl + "Scontactid",
                    data: JSON.stringify(jsonPa),
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        $("#contactid").val(s.Contactid)
                        $("#txtName").val(s.Name);
                        $("#customer").val(s.Customername);
                        $("#description").text(s.Description);

                    }, error: function (xhr) {
                        alert(xhr);
                    }
                })
            })

            $("#savechange").click(function () {
                var jsonPara = {
                    contact: {
                        contactid: $("#contactid").val(),
                        name: $("#txtName").val(),
                        description: $("#description").text()
                    }
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "UpdateContact",
                    data: JSON.stringify(jsonPara),
                    success: function (data) {
                        alert("更新成功");
                        window.location.reload();
                    }, error: function (xhr) {
                        alert(xhr);
                    }
                })
            })

        }, error: function (xhr) {
            alert(xhr);
        }
    })
}