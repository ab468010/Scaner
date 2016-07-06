var sampleJs, sampleVar;

function initConfig() {
    //初始化模块JS
    sampleJs = new Globals.sample();

    //页面变量
    sampleVar = {
    };

    (function () {

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
                var content = "<td>" + sampleList[i].Name + "</td><td>" + sampleList[i].ProjectIdName + "</td><td>" + sampleList[i].ContainerIdName + "</td><td>" + sampleList[i].SampleCode + "</td><td>" + sampleJs.getSampleClassText(sampleList[i].SampleClass) + "<ul class='actions'><li class='last'><a  class='sample2 edit1'>详情</a></li></ul></td>" + "<td style='display:none' name='Id'>" + sampleList[i].SampleId + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = content;

                tbody.append(row);
            }
            $(".sample2.edit1").click(function () {
                location.href = "sample-profile.html?id=" + $(this).parent().parent().parent().parent().find("[name='Id']").text();
            });
        
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        }
    });
}
