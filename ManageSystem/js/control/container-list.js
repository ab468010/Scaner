var containerJs, containerVar;
var page;
var roleid = Globals.getCookie("RoleId");
if ($.getUrlParam("page") == null || $.getUrlParam("page") == undefined) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}
function initConfig() {
    //初始化模块JS
   containerJs = new Globals.container();

    //页面变量
    containerVar = {
    };

    (function () {
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
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
                    if (s == 0) {
                        s=1
                    }
                    var pa = Math.ceil(s / 10);
                   
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
                    location.href = "container-list.html?page=" + "1";
                }
            })
            $(".before").click(function () {
                if ($("#PageNo").text() > 1) {
                    var number = parseInt($("#PageNo").text() - 1);
                    $("#PageNo").text(number);
                    location.href = "container-list.html?page=" + number;
                }
            })
            $(".last").click(function () {
                if ($("#PageNo").text() < $("#totalPageNo").text()) {
                    var number = parseInt($("#totalPageNo").text());
                    $("#PageNo").text(number);
                    location.href = "container-list.html?page=" + number;
                }
            })
            $(".next").click(function () {
                if (($("#PageNo").text() < $("#totalPageNo").text())) {
                    var number = parseInt($("#PageNo").text())+1;
                    $("#PageNo").text(number);
                    location.href = "container-list.html?page=" + number;
                }
            })
            $(".Go").click(function () {
                if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo").val())) {
                    var number = parseInt($("#pageNum").val());
                    $("#PageNo").text(number);
                    location.href = "container-list.html?page=" + number;
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
        data:JSON.stringify(jsonPara),
        success: function (data) {
            var containerList = JSON.parse(data.d);
            var tbody = $("#divContainer table tbody").empty();
            for (i in containerList) {
                var content = '<tr><td><a href="#">' + containerList[i].Name + '</a></td><td>' + containerList[i].ContainerCode + '</td><td>' + containerList[i].Size +
                                '<ul class="actions">' +
                                    '<li><a class="container2 read1" href="container-profile.html?containerId=' + containerList[i].ContainerId + '">详情</a></li>' +
                                    '<li class="last"><a href="#" class="container2 delete1">删除</a></li>' +
                                '</ul>' +
                            '</td>' + "<td style='display:none' name='Id'>" + containerList[i].ContainerId + "</td><td name='projectstatuscode'style='display:none'>" +containerList[i].ProjectStatusCode+"</td>"
                        '</tr>';

                var row = document.createElement("tr");
                row.innerHTML = content;

                tbody.append(row);
            }
            
            $("#divContainer table tbody tr").dblclick(function () {
                location.href = "container-profile.html?id=" + $(this).find("[name='Id']").text();
            });

            $(".container2.delete1").click(function () {
                var s=$(this).parent().parent().parent().parent().find("[name=projectstatuscode]").text();
                if (s > 2) {
                    alert("项目已开始无法删除")
                } else {
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
                                    alert("移除成功");
                                    window.location.reload();
                                } else {
                                    alert("请先删除相关样品");
                                }
                            }, error: function (xhr) {
                                alert(xhr);
                            }
                        })
                    }
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
