var containerJs, containerVar;

function initConfig() {
    //初始化模块JS
   containerJs = new Globals.container();

    //页面变量
    containerVar = {
    };

    (function () {

        $("#btnNew").click(function () {
            location.href = "new-container.html";
        })
        $(document).ready(function () {
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetContainerCount",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    var page = Math.ceil(s / 10);
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
        });


    })();
}

$(function () {
    initConfig();
});

function Page(number) {
    var jsonPara = {
        pageNo:number
    }

    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetContainerPageList",
        contentType: "application/json; charset=utf-8",
        async: false,
        data:JSON.stringify(jsonPara),
        success: function (data) {
            var containerList = JSON.parse(data.d);
            var tbody = $("#divContainer table tbody").empty();
            for (i in containerList) {
                var content = '<tr><td><a href="#">' + containerList[i].Name + '</a></td><td>' + containerList[i].ContainerCode + '</td><td>' + containerList[i].Size +
                                '<ul class="actions">' +
                                    '<li><a class="container2 edit1 edit" href="container-profile.html?containerId=' + containerList[i].ContainerId + '">编辑</a></li>' +
                                    '<li class="last"><a href="#" class="container2 delete1 delete">删除</a></li>' +
                                '</ul>' +
                            '</td>' + "<td style='display:none' name='Id'>" + containerList[i].ContainerId + "</td>" +
                        '</tr>';

                var row = document.createElement("tr");
                row.innerHTML = content;

                tbody.append(row);
            }
            $("#divContainer table tbody tr").dblclick(function () {
                location.href = "container-profile.html?id=" + $(this).find("[name='Id']").text();
            });

            $(".delete").click(function () {
                if (confirm("删除吗？")) {
                    var jsonPar = {
                        containerId: $(this).parent().parent().parent().parent().find("[name='Id']").text()
                    }
                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "DeleteContainer",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(jsonPar),
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s) {
                                alert("删除成功");
                                window.location.reload();
                            } else {
                                alert("删除失败");
                            }
                        }, error: function (xhr) {
                            alert(xhr);
                        }
                    })
                }
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        }
    });
}
