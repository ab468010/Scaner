


 $(function () {
     $.ajax({
         type: "post",
         contentType: "application/json; charset=utf-8",
         url: Globals.ServiceUrl + "SelectUser",
         async: false,
         success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $("#tbl tbody").empty();
            for (var i in s) {
                if (s[i].StateCode == 1) {
                    var StateCode = "启用";
                } else {
                    var StateCode = "禁用";
                }
                var cont = "<td>" + s[i].Name + "</td><td>" + s[i].UserName + "</td><td>" + StateCode + "</td><td style='display:none' name='systemuserid'>" + s[i].SystemUserId + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }
            $("#tbl tr").dblclick(function () {
                location.href = "user-profile.html?id=" + $(this).find("[name='systemuserid']").text();
            })
         }
     })
     $("#btnNew").click(function () {
         location.href = "new-user.html";
     })
})