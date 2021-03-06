﻿if (Globals.getCookie("SystemUserId") == null) {
    alert("请登录");
    location.href = "login.html";
}
var sampleJs, sampleVar;
var page;
var roleid = Globals.getCookie("RoleId");
if ($.getUrlParam("page") == null || $.getUrlParam("page") == undefined) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}
var privilege = JSON.parse(Globals.getCookie("privilege"));
function initConfig() {
    //初始化模块JS
    sampleJs = new Globals.sample();

    //页面变量
    sampleVar = {
    };

    (function () {
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $("#btnNew").click(function () {
            location.href = "new-sample.html";
        })

        $(document).ready(function () {
            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetSampleCount",
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
                    alert("请联系管理员");
                    return false;
                }
            })
            Page((page-1)*10);
            $(".first").click(function () {
                if ($("#PageNo").text() != 1) {
                    $("#PageNo").text(1);
                    location.href = "sample-list.html?page=" + "1";
                }
            })
            $(".before").click(function () {
                if ($("#PageNo").text() > 1) {
                    var number = parseInt($("#PageNo").text() - 1);
                    $("#PageNo").text(number);
                    location.href = "sample-list.html?page=" + number;
                }
            })
            $(".last").click(function () {
                if ($("#PageNo").text() < $("#totalPageNo").text()) {
                    var number = parseInt($("#totalPageNo").text());
                    $("#PageNo").text(number);
                    location.href = "sample-list.html?page=" + number;
                }
            })
            $(".next").click(function () {
                if (($("#PageNo").text() < $("#totalPageNo").text())) {
                    var number = parseInt($("#PageNo").text())+1;
                    $("#PageNo").text(number);
                    location.href = "sample-list.html?page=" + number;
                }
            })
            $(".Go").click(function () {
                if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo"))) {
                    var number = parseInt($("#pageNum").val());
                    $("#PageNo").text(number);
                    location.href = "sample-list.html?page=" + number;
                }
            })
       
        });

    })();
}

$(function () {
    initConfig();
});

function Page(number) {
    var jsonPara={
        pageNo:number
    }
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetSamplePageList",
      
        contentType: "application/json; charset=utf-8",
        data:JSON.stringify(jsonPara),
        success: function (data) {
            var sampleList = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();

            for (i in sampleList) {
                var content = "<td>" + sampleList[i].Name + "</td><td>" + sampleList[i].ProjectIdName + "</td><td>" + sampleList[i].ContainerIdName + "</td><td>" + sampleList[i].SampleCode + "</td><td style='display:none'name='projectid'>"
                                 +sampleList[i].ProjectId+"</td><td style='display:none'name=containerid>"+sampleList[i].ContainerId+"</td><td>"
                                   +sampleList[i].ShelfIdName+"</td><ta>"+ sampleJs.getSampleClassText(sampleList[i].SampleClass) +
                    "<ul class='actions'><li class='last'><a  class='sample2 read1'style='display:none'>详情</a></li></ul></td>" + "<td style='display:none' name='Id'>" + sampleList[i].SampleId + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = content;

                tbody.append(row);
            }
            for (var i in privilege) {
                if (privilege[i].CanDelete == true) {
                    $("." + privilege[i].Tablename + 2 + ".delete1").attr({ style: "display:inline" });
                }
                if (privilege[i].CanCreate == true) {
                    $("." + privilege[i].Tablename + 2 + ".create1").attr({ style: "display:inline" });
                }
                if (privilege[i].CanWrite == true) {
                    $("." + privilege[i].Tablename + 2 + ".edit1").attr({ style: "display:inline" });
                }
                if (privilege[i].CanRead == true) {
                    $("." + privilege[i].Tablename + 2 + ".read1").attr({ style: "display:inline" });

                }
            };
            $(".sample2.read1").click(function () {
                location.href = "sample-profile.html?id=" + $(this).parent().parent().parent().parent().find("[name='Id']").text()
                + "&projectId=" + $(this).parent().parent().parent().parent().find("[name='projectid']").text()
                + "&containerId=" + $(this).parent().parent().parent().parent().find("[name='containerid']").text()
            });
            json = {
                roleid: roleid
            };
           
        }
      
    });
}
