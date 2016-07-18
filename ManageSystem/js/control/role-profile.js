var SystemUserId = Globals.getCookie("SystemUserId");

$(function () {
    var id = Globals.trim($.getUrlParam("id"));
    $("#newprivilege").click(function () {
        location.href = "new-privilege.html?id=" + id;
    });
    $("#login").click(function () {
        if (confirm("确定注销？")) {
            location.href = "login.html";
        }
    });
    if (id!= ""&&id!=null) {
        $("#myModal .modal-body").load("child/edit-role.html");
        var jsonPara = {
            roleid: id
        }
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
           
            url: Globals.ServiceUrl + "GetRole",
            data: JSON.stringify(jsonPara),
            success: function (data) {
                var s = JSON.parse(data.d);
                if (s == null) {
                    alert("角色不存在");
                    location.href = "role-list.html";
                } else {
                    $("#tleName").text(s.RoleName);
                    $("#Description").text(s.Description);
                    $("#RotxtName").val(s.RoleName);
                    $("#Rodescription").val(s.Description);
                }
            }, error: function (xhr) {
                alert(xhr);
            }
        });
    } else {
        alert("角色不存在")
        location.href = "role-list.html";
    }
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: Globals.ServiceUrl + "GetRolePrivilegeList",
        async: false,
        data: JSON.stringify(jsonPara),
        success: function (data) {
            var s = JSON.parse(data.d);

            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var cont = "<td>" + s[i].Name + "</td><td>" + s[i].ModuleName + "</td><td>" + istrue(s[i].CanRead) + "</td><td>"
                           + istrue(s[i].CanDelete) + "</td><td>" + istrue(s[i].CanWrite) + "</td><td>" + istrue(s[i].CanCreate) + "</td><td>" + istrue(s[i].CanManage) +
                           "<ul class='actions'><li class='last'><a class='privilegeedit privilege2 edit1' href='#myModal1'data-toggle='modal'>编辑</a> <a class='deleteprivilege privilege2 delete1'>删除</a></li></ul>" + "</td><td name='privilegeid' style='display:none'>" + s[i].PrivilegeId + "</td>"
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }
            $("#myModal1 .modal-body").load("child/edit-privilege.html");
            $(".deleteprivilege").click(function () {
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
            $(".privilegeedit").click(function () {            
                var jsonPar={
                    id:$(this).parent().parent().parent().parent().find("[name='privilegeid']").text()
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json;charset=utf-8",
                    url: Globals.ServiceUrl + "GetPrivilege",
                    data: JSON.stringify(jsonPar),
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        var option1 = $("#Module1").empty();
                        $("#Name1").val(s.Name);
                        option1.append($("<option>").val(s.ModuleId).text(s.ModuleName));
                        $('#Module1').selectpicker('refresh');
                        $("#CanRead1").prop("checked", s.CanRead);
                        $("#CanWrite1").prop("checked", s.CanWrite);
                        $("#CanDelete1").prop("checked", s.CanDelete);
                        $("#CanCreate1").prop("checked", s.CanCreate);
                        $("#CanManage1").prop("checked", s.CanManage);
                        $("#privilegeid1").val(s.PrivilegeId)
                    }, error: function (xhr) {
                        alert(xhr)
                    }
                })
            })
        }
    })
    $("#savechange1").click(function () {
        var jsonPar = {
            privilege: {
                privilegeid: $("#privilegeid1").val(),
                name: $("#Name1").val(),
                moduleid: $("#Module1").val(),
                canread: $("#CanRead1").prop("checked"),
                canwrite: $("#CanWrite1").prop("checked"),
                candelete: $("#CanDelete1").prop("checked"),
                cancreate: $("#CanCreate1").prop("checked"),
                canmanage: $("#CanManage1").prop("checked")
            }
        }
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            url: Globals.ServiceUrl + "UpdatePrivilege",
            data: JSON.stringify(jsonPar),
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
                alert(xhr)
            }
        })
    })
    $("#savechange").click(function () {
        var jsonPara = {
            role: {
                roleid: id,
                rolename: $("#RotxtName").val(),
                description: $("#Rodescription").val(),
                ModifiedBy:SystemUserId
            }
        }
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(jsonPara),
            url: Globals.ServiceUrl + "UpdateRole",
            dataType: "json",
            success: function (data) {
                var s = JSON.parse(data.d);
                if (s) {
                    alert("修改成功");
                    window.location.reload();
                }
                else {
                    alert("修改失败")
                }
            }, error: function (xhr) {
                alert(xhr);
            }
        })
    });

    $("#deleterole").click(function () {
        var jsonPara = {
            roleid: id
        }
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            url: Globals.ServiceUrl + "GetRoleType",
            data: JSON.stringify(jsonPara),
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
})
            function istrue(bool) {
                if (bool) {
                    return '✔';
                } else {
                    return '✘';
                }
            };