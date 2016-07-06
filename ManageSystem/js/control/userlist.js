var userJs, userVar;

function initConfig() {
    //初始化模块JS
    userJs = new Globals.user();

    //页面变量
    userVar = {
    };

    (function () {

        $("#btnNew").click(function () {
            location.href = "new-user.html?action=Create";
        })

        $(document).ready(function () {

            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetUserList",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var userList = JSON.parse(data.d);
                    var tbody = $("#divUserList table tbody").empty();

                    for (i in userList)
                    {
                        var content = "<td>" + userList[i].Name + "</td><td>" + userList[i].Username + "</td><td></td><td class='align-right'></td><td style='display:none' name='Id'>"+userList[i].SystemUserId+"</td>";
                        var row = document.createElement("tr");
                        row.innerHTML = content;

                        tbody.append(row);
                    }
                    $("#divUserList table tbody tr").dblclick(function () {
                        location.href = "user-profile.html?id=" + $(this).find("[name='Id']").text();
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });
        });


    })();
}

$(function () {
    initConfig();
});


