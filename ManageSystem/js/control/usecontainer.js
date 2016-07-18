var page;
if ($.getUrlParam("page") == null || undefined) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}
function useContainer() {
    (function () {
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetUseContainerCount",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                var pa = Math.ceil(s / 10);
                if (pa== 0) {
                    pa = 1;
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
                location.href = "usecontainer.html?page=" + "1";
            }
        })
        $(".before").click(function () {
            if ($("#PageNo").text() > 1) {
                var number = parseInt($("#PageNo").text() - 1);
                $("#PageNo").text(number);
                location.href = "usecontainer.html?page=" + number;
            }
        })
        $(".last").click(function () {
            if ($("#PageNo").text() < $("#totalPageNo").text()) {
                var number = parseInt($("#totalPageNo").text());
                $("#PageNo").text(number);
                location.href = "usecontainer.html?page=" + number;
            }
        })
        $(".next").click(function () {
            if (($("#PageNo").text() < $("#totalPageNo").text())) {
                var number = parseInt($("#PageNo").text())+1;
                $("#PageNo").text(number);
                location.href = "usecontainer.html?page=" + number;
            }
        })
        $(".Go").click(function () {
            if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo").val())) {
                var number = parseInt($("#pageNum").val());
                $("#PageNo").text(number);
                location.href = "usecontainer.html?page=" + number;
            }
        })
    })()
}
$(function () {
    useContainer();
})
function Page(pa) {
    var jsonP = {
        Page:pa
    }
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetUseContainerList",
        contentType: "application/json; charset=utf-8",
        data:JSON.stringify(jsonP),
        success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var cont = "<ta>" + s[i].Name + "</td><td>" + s[i].ContainerCode + "</td><td>" + s[i].Size + "</td><td style='display:none'>" + s[i].ContainerId + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }

        }
    })
}