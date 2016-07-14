var projectJs, projectVar;
var SystemUserId = Globals.getCookie("SystemUserId");
var roleid = Globals.getCookie("RoleId");
var page;
var GetCount;
var GetList ;
var val;
if ($.getUrlParam("statusCode") == null ||$.getUrlParam("statusCode")== undefined) {
    val = 0;
} else {
    val = $.getUrlParam("statusCode");
}
if ($.getUrlParam("page") == null || $.getUrlParam("page") == undefined) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}
function initConfig() {
    //初始化模块JS
    projectJs = new Globals.project();

    //页面变量
    projectVar = {
    };

    (function () {

        $("#btnNew").click(function () {
            location.href = "new-project.html";
        });
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $(document).ready(function () {

            $("#selectStatusCode option").attr("selected", false);
            $("#selectStatusCode").val(val);

            $("#selectStatusCode option[val='" + val + "']").attr("selected", true);
            if ($("#selectStatusCode").val() == 0) {
                GetCount = "GetProjectCount";
                GetList = "GetProjectListByUserId";
            } else {
                GetCount = "GetProjectStatusCodeCount";
                GetList = "GetProjectStatusCodeList";
            }
            var jsonPa = {
                systemuserId: SystemUserId,
                roleId: roleid,
                statusCode:$("#selectStatusCode").val()
            };
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + GetCount ,
                data: JSON.stringify(jsonPa),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var s = JSON.parse(data.d);
                 
                    var pa = Math.ceil(s / 10);
                    if (pa == 0) {
                        pa=1
                    }
                    $("#PageNo").text(page);
                    $("#totalPageNo").text(pa);
                }
            });
            Page((page-1)*10);
            $("#serch").click(function () {
                location.href = "project-list.html?page=" + "1" + "&statusCode=" + $("#selectStatusCode").val();
            })
            $(".first").click(function () {
                if ($("#PageNo").text() != 1) {
                    $("#PageNo").text(1);
                    location.href = "project-list.html?page=" + "1";
                }
            })
            $(".before").click(function () {
                if ($("#PageNo").text() > 1) {
                    var number = parseInt($("#PageNo").text())-1;
                    $("#PageNo").text(number);
                    location.href = "project-list.html?page=" + number;
                    //Page((number - 1) * 10);
                }
            })
            $(".last").click(function () {
                if ($("#PageNo").text() < $("#totalPageNo").text()) {
                    var number = parseInt($("#totalPageNo").text());
                    $("#PageNo").text(number);
                    location.href = "project-list.html?page=" + number;
                   // Page((number - 1) * 10);
                }
            })
            $(".next").click(function () {
                if (($("#PageNo").text() < $("#totalPageNo").text())) {
                    var number = parseInt($("#PageNo").text())+1;
                    $("#PageNo").text(number);
                    location.href = "project-list.html?page=" + number;
                   // Page(number * 10);
                }
            })
            
            $(".Go").click(function () {
                if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo").val())) {
                    var number = parseInt($("#pageNum").val());
                    $("#PageNo").text(number);
                    location.href = "project-list.html?page=" + number;
                    //Page((number - 1) * 10);
                }
            })

        });


    })();
}

$(function () {
    initConfig();
});

function Page(pa) {
    var jsonPara = {
        systemuserId: SystemUserId,
        roleId: roleid,
        Page: pa,
        statusCode: $("#selectStatusCode").val()
    };
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + GetList,
        data: JSON.stringify(jsonPara),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var projectList = JSON.parse(data.d);

            var tbody = $("#divProject table tbody").empty();

            for (i in projectList) {
                var content = '<tr><td><a href="#" class="no">' + projectList[i].ProjectNo + '</a></td><td class="name">' + projectList[i].Name + '</td><td>' + projectJs.bulidstatus(projectList[i].StatusCode) + '</td><td name="projectid" style="display:none">' + projectList[i].ProjectId + '</td>' +
                            '<td>' + projectList[i].EngineerIdName +
                                '<ul class="actions">' +
                                    '<li><a class="project2 read1" href="project-profile.html?projectId=' + projectList[i].ProjectId + '">详情</a></li>' +
                                    '<li class="last"><a  class="project2 delete1 ">删除</a></li>' +
                                '</ul>' +
                            '</td>' +
                        '</tr>';
                // var content = "<td>" + projectList[i].ProjectNo + "</td><td>" + projectList[i].Name + "</td><td></td><td class='align-right'></td><td style='display:none' name='Id'>" + projectList[i].SystemUserId + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = content;

                tbody.append(row);
            }
            $(".project2.delete1").click(function () {
                if (confirm("确定删除?")) {
                    var jsonPar = {
                        projectId: $(this).parent().parent().parent().parent().find("[name='projectid']").text()
                    }
                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "DeleteProject",
                        data: JSON.stringify(jsonPar),
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s) {
                                alert("删除成功");
                                window.location.reload();
                            } else {
                                alert("请先删除任务");
                            }
                        }, error: function (xhr) {
                            alert(xhr);
                        }
                    })
                }

            })
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        }
    });

}

