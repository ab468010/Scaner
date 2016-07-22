function privilege() {
    (function () {
        $.ajax({
            type: "post",
            contentType: "application/json;charset=utf-8",
            url: Globals.ServiceUrl + "GetPrivilegeList",
            success: function (data) {
                var s = JSON.parse(data.d);
             
                    var tbody = $(".table tbody").empty();
                    for (var i in s) {
                        var cont = "<td>" + s[i].Name + "</td><td>" + s[i].ModuleName+ "</td><td>" +istrue(s[i].CanRead)+ "</td><td>"
                                   + istrue(s[i].CanDelete) + "</td><td>" + istrue(s[i].CanWrite) + "</td><td>" + istrue(s[i].CanCreate) + "</td><td>" + istrue(s[i].CanManage) +
                                   "<ul class='actions'><li class='last'><a class='edit'>编辑</a> <a class='delete'>删除</a></li></ul>" + "</td><td name='privilegeid' style='display:none'>"+s[i].PrivilegeId+"</td>"
                        var row = document.createElement("tr");
                        row.innerHTML = cont;
                        tbody.append(row);                  
                    }
                    $(".edit").click(function () {
                        location.href = "privilege-profile.html?id=" + $(this).parent().parent().parent().parent().find("[name=privilegeid]").text();
                    })
                    $(".delete").click(function () {
                        if (confirm("确定删除？")) {
                            var jsonPar = {
                                privilegeid: $(this).parent().parent().parent().parent().find("[name=privilegeid]").text()
                            }
                            $.ajax({
                                type: "post",
                                contentType: "application/json; charset=utf-8",
                                url: Globals.ServiceUrl + "DeletePrivilege",
                                data: JSON.stringify(jsonPar),
                                success: function (data) {
                                    var s = JSON.parse(data.d);
                                    if (s) {
                                        alert("删除成功");
                                        window.location.reload();
                                    } else {
                                        alert("删除失败");
                                    }
                                }
                            })
                        }
                      
                    })
             
            }, error: function (xhr) {
                 alert("请联系管理员");
            }
        })
      
    })()
}

function istrue(bool) {
    if (bool) {
        return '✔';
    } else {
        return '✘';
    }
}
$(function () {
    privilege();
})