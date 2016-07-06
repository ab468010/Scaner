
var Id=$.getUrlParam("id");
function privilegepr() {

    (function () {
        if (Id == "" || Id == null) {
            alert("权限不存在")
            location.href = "privilege.html";
        } else {
            $("#myModal .modal-body").load("child/edit-privilege.html");
            var jsonPar = {
                id: Id
            }
            $.ajax({
                type: "post",
                contentType: "application/json;charset=utf-8",
                url: Globals.ServiceUrl + "GetPrivilege",
                data: JSON.stringify(jsonPar),
                success: function (data) {
                    var s = JSON.parse(data.d);
                    var option = $("#Module").empty();
                    var option1 = $("#Module1").empty();
                    if (s.PrivilegeId != null) {
                        $("#Name").val(s.Name);
                        option.append($("<option>").val(s.ModuleId).text(s.ModuleName));
                        $('#Module').selectpicker('refresh');
                        $("#CanRead").prop("checked", s.CanRead);
                        $("#CanWrite").prop("checked", s.CanWrite);
                        $("#CanDelete").prop("checked", s.CanDelete);
                        $("#CanCreate").prop("checked", s.CanCreate);
                        $("#CanManage").prop("checked", s.CanManage);
                        $("#privilegeid").val(s.PrivilegeId)
                        $("#Name1").val(s.Name);
                        option1.append($("<option>").val(s.ModuleId).text(s.ModuleName));
                        $('#Module1').selectpicker('refresh');
                        $("#CanRead1").prop("checked", s.CanRead);
                        $("#CanWrite1").prop("checked", s.CanWrite);
                        $("#CanDelete1").prop("checked", s.CanDelete);
                        $("#CanCreate1").prop("checked", s.CanCreate);
                        $("#CanManage1").prop("checked", s.CanManage);
                        $("#privilegeid1").val(s.PrivilegeId)

                    } else {
                        alert("权限不存在");
                        location.href = "privilege.html";
                    }
                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        }
        $("#deleteprivilege").click(function () {
            if (confirm("确定删除？")) {
                var jsonPar = {
                    privilegeid:Id
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
                            location.href = "privilege.html";
                        } else {
                            alert("删除失败");
                        }
                    }
                })
            }

        })
        $("#savechange").click(function () {
            var jsonPar = {
                privilege: {
                    privilegeid: $("#privilegeid").val(),
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
                        location.href = "privilege.html";
                    } else {
                        alert("更新失败");
                    }
                }, error: function (xhr) {
                    alert(xhr)
                }
            })
        })
    })()
}

$(function () {
    privilegepr();
})