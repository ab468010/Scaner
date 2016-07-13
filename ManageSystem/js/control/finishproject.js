var SystemUserId = Globals.getCookie("SystemUserId");
var roleid = Globals.getCookie("RoleId");
var projectJs, projectVar;
var page;
if ($.getUrlParam("page") == null || undefined) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}
function finishProject() {
    projectJs = new Globals.project();
    (function () {
        var jsonPar = {
            systemuserId: SystemUserId,
            roleId: roleid
        }
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetFinishProjectCount",
            data: JSON.stringify(jsonPar),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                var pa = Math.ceil(s / 10);
                if (pa == 0) {
                    pa=1
                }
                $("#PageNo").text(page);
                $("#totalPageNo").text(pa);
            }, error: function (xhr) {
                alert(xhr);
            }
        })
        Page((page-1)*10);
        $(".first").click(function () {
            if ($("#PageNo").text() != 1) {
                $("#PageNo").text(1);
                location.href = "finishproject.html?page" + "1";
            }
        })
        $(".before").click(function () {
            if ($("#PageNo").text() > 1) {
                var number = parseInt($("#PageNo").text() - 1);
                $("#PageNo").text(number);
                location.href = "finishproject.html?page" + number;
            }
        })
        $(".last").click(function () {
            if ($("#PageNo").text() < $("#totalPageNo").text()) {
                var number = parseInt($("#totalPageNo").text());
                $("#PageNo").text(number);
                location.href = "finishproject.html?page" + number;
            }
        })
        $(".next").click(function () {
            if (($("#PageNo").text() < $("#totalPageNo").text())) {
                var number = parseInt($("#PageNo").text())+1;
                $("#PageNo").text(number);
                location.href = "finishproject.html?page" + number;
            }
        })
        $(".Go").click(function () {
            if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo").val())) {
                var number = parseInt($("#pageNum").val());
                $("#PageNo").text(number);
                location.href = "finishproject.html?page" + number;
            }
        })
    })()
}
$(function () {
    finishProject();
})
function Page(pa) {
    var jsonPa = {
        Page: pa,
        systemuserId: SystemUserId,
        roleId:roleid
    }
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetFinishProjectList",
        data: JSON.stringify(jsonPa),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var projectList = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (i in projectList) {
                var content = '<tr><td>' + projectList[i].ProjectNo + '</td><td class="name">' + projectList[i].Name + '</td><td>' + projectJs.bulidstatus(projectList[i].StatusCode) + '</td><td name="projectid" style="display:none">' + projectList[i].ProjectId + '</td>' +
                            '<td>' + projectList[i].EngineerIdName +
                            '</td>' +
                        '</tr>';

                var row = document.createElement("tr");
                row.innerHTML = content;

                tbody.append(row);
            }
        }, error: function (xhr) {
            alert(xhr);
        }
    })
}