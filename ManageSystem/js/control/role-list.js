function rolelist() {
    (function () {
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetRoleCount",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                var page = Math.ceil(s /10);
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
            if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo"))) {
                var number = parseInt($("#pageNum").val());
                $("#PageNo").text(number);
                Page((number - 1) * 10);
            }
        })
           
    })()
}

$(function () {
    rolelist()
})

function Page(page) {
    var jsonPar = {
        number:page
    }
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: Globals.ServiceUrl + "GetPageRoleList",
        //async: false,
        data:JSON.stringify(jsonPar),
        success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var cont = "<td>" + s[i].RoleName + "</td><td>" + s[i].Description
                + "<ul class='actions'><li class='last'><a class='role2 edit1'>编辑</a> <a class='role2 delete1'>删除</a></li></ul>" + "</td><td name='roleid' style='display:none'>" + s[i].RoleId + "</td>"
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }
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
                                            alert("请先删除用户")
                                        }
                                    }, error: function (xhr) {
                                        alert(xhr);
                                    }
                                })
                            }     
                        }
                    }, error: function (xhr) {
                        alert(xhr);
                    }
                })             
            })
          }

         })       
        
    }
