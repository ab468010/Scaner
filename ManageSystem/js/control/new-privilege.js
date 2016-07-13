var roleid = $.getUrlParam("id");
var mothed;
function newprivilege() {
    (function () {
        if (roleid != "" && roleid != null) {
            mothed = "CreateRolePrivilege";
        } else {
            mothed = "CreatePrivilege";
        }
        var jsonP = {
            roleId:roleid
        }
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            url: Globals.ServiceUrl + "GetModuleList",
            data: JSON.stringify(jsonP),
            success: function (data) {
                var s = JSON.parse(data.d);
                var option = $("#Module").empty();
                for (var i in s) {
                    option.append($("<option>").val(s[i].ModuleId).text(s[i].Name))
                }
                $('#Module').selectpicker('refresh');
            }
        });
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $("#btnSave").click(function () {
            var jsonPar = {
                privilege: {
                    name: $("#Name").val(),
                    moduleid:$("#Module").val(),
                    canread: $("#CanRead").prop("checked"),
                    canwrite: $("#CanWrite").prop("checked"),
                    candelete: $("#CanDelete").prop("checked"),
                    cancreate: $("#CanCreate").prop("checked"),
                    canmanage:$("#CanManage").prop("checked")
                },
                roleId:roleid
            }
         
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                url: Globals.ServiceUrl + mothed,
                data: JSON.stringify(jsonPar),
                dataType:"json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("创建成功");
                        location.href = "role-profile.html?id="+roleid;
           
                    } else {
                        alert("创建失败");
                    }
                }, error: function (xhr) {
                    alert(xhr)
                }
            })
           })    
    })()
}

$(function () {
    newprivilege();
})