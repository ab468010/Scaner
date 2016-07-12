$(function () {
    var id = $.getUrlParam("id");

    $("#newcontact").click(function () {
        location.href = "new-contact.html?id=" + id;
    })
    if (Globals.trim(id)!="") {
        $("#myModal .modal-body").load("child/edit-customer.html");
        var jsonPara = {
            customerid: id
        }
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            url: Globals.ServiceUrl + "Scustomerd",
            data: JSON.stringify(jsonPara),
            success: function (data) {
                var s = JSON.parse(data.d);
                if (s == null) {
                    alert("客户不存在");
                    location.href = "customer-list.html";
                } else {
                    $("#tleName").text(s.Name);
                    $("#Description").text(s.Description);
                    $("#CutxtName").val(s.Name);
                    $("#Cudescription").val(s.Description);
                }          
            }
        });
    } else {
        alert("客户不存在")
        location.href = "customer-list.html";
    }
   
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: Globals.ServiceUrl + "Scontact",
        async: false,
        data: JSON.stringify(jsonPara),
        success: function (data) {
            var s = JSON.parse(data.d);
                var tbody = $(".table tbody").empty();
                for (var i in s) {
                    if (s[i].Contactid != null) {
                        var cont = "<td >" + s[i].Name + "</td><td name='contactid' style='display:none'>" + s[i].Contactid + "</td><td>" +
                            "<ul class='actions'><li class='last'><a class='editcontact contact2 edit1'href='#myModal1' data-toggle='modal'>编辑</a> <a class='contact2 delete1'>删除</a></li></ul>" + "</td>"
                        var row = document.createElement("tr");
                        row.innerHTML = cont;
                        tbody.append(row);
                    } else {
                        $(".table tbody").empty();
                    }
                }
                $(".editcontact").click(function () {
                    $("#myModal1 .modal-body").load("child/edit-contact.html");
                    var jsoPara = {
                        contactid: $(this).parent().parent().parent().parent().find("[name='contactid']").text()
                    }
                    $.ajax({
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: Globals.ServiceUrl + "Scontactid",
                        data: JSON.stringify(jsoPara),
                        success: function (data) {
                                var s = JSON.parse(data.d);
                        $("#contactid").val(s.Contactid)
                        $("#txtName").val(s.Name);
                        $("#customer").val(s.Customername);
                        $("#description").text(s.Description);
                        }
                    })
                })
            $(".contact2.delete1").click(function () {
                if (confirm("确定删除?")) {
                    var jsoPara = {
                        contactid: $(this).parent().parent().parent().parent(). find("[name='contactid']").text()
                    }
                    $.ajax({
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        url: Globals.ServiceUrl + "DeleteContact",
                        data: JSON.stringify(jsoPara),
                        success: function (data) {
                            alert("删除成功");
                            window.location.reload();
                        }
                    })
                }          
            })
        }
    })
    $("#savechange").click(function () {
        if (Globals.trim($("#CutxtName").val()) != "") {
            var jsonPara = {
                customer: {
                    Customerid: id,
                    Name: $("#CutxtName").val(),
                    Description: $("#Cudescription").val()
                }
            }
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(jsonPara),
                url: Globals.ServiceUrl + "Update",
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("成功");
                        window.location.reload();
                    }
                    else {
                        alert("失败")
                    }
                }
            })
        } else {
            alert("客户名不能为空");
        }

   
    })
    $("#savechange1").click(function () {
        if (Globals.trim($("#txtName").val()) != "") {
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
        } else {
            alert("联系人不能为空")
        }
      
    })
    $("#deletecustomer").click(function () {
        if (confirm("确定删除?")) {
            var jsonPara = {
                customerid: id
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
                        location.href = "customer-list.html";
                    } else {
                        alert("请先删除联系人")
                    }
                }
            })
        }
   
    })
})