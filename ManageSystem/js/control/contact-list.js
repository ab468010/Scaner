

$(function () {
  
    $("#myModal .modal-body").load("child/edit-contact.html");
    $(".success").click(function () {
        location.href = "new-contact.html";
    })
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetContactCount",
        async:false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var s = JSON.parse(data.d);
            var page = Math.ceil(s / 10);
            $("#PageNo").text(1);
            $("#totalPageNo").text(page);
        }, error: function (xhr) {
            alert(xhr);
        }
    })
    Page(0);
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
            Page((number - 1) * 10);
        }
    })
    $(".last").click(function () {
        if ($("#PageNo").text() < $("#totalPageNo").text()) {
            var number = parseInt($("#totalPageNo").text());
            $("#PageNo").text(number);
            Page((number - 1) * 10);
        }
    })
    $(".next").click(function () {
        if (($("#PageNo").text() < $("#totalPageNo").text())) {
            var number = parseInt($("#PageNo").text());
            $("#PageNo").text(number + 1);
            Page(number * 10);
        }
    })
    $(".Go").click(function () {
        if(($("#totalPageNo").text()>=$("#pageNum").val() >= 1 && $("#pageNum").val()!=$("#PageNo"))){
            var number = parseInt($("#pageNum").val());
            $("#PageNo").text(number);
            Page((number-1)*10);
        }
    })
   
});

function Page(page) {
    var jsonPar = {
        number: page
    }
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "Selectcontact",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify(jsonPar),
        success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var cont = "<td name='contactid' style='display:none'>" + s[i].Contactid + "</td><td>" + s[i].Name + "</td><td>" + s[i].Description + "</td><td>" + s[i].Customername +
                    " <ul class='actions'><li class='last'><a class='contact2 edit1'href='#myModal' data-toggle='modal'>编辑</a>  <a class='contact2 delete1' >删除</a></li></ul>" + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }

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
                var jsonPar = {
                    contactid: parseInt($(this).parent().parent().parent().parent().find("[name='contactid']").text())
                }

                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "Scontactid",
                    data: JSON.stringify(jsonPar),
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