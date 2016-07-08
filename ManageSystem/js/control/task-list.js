var systemuserid = Globals.getCookie("SystemUserId");
var roleid = Globals.getCookie("RoleId");
$(function () {
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetTaskCount",
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



   
  
})
function Page(page) {
    var jsonPar = {
        number: page,
        systemuserId: systemuserid,
        roleId:roleid
    }
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: Globals.ServiceUrl + "SelectTask",
        //async: false,
        data: JSON.stringify(jsonPar),
        success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var estart = Globals.datetime_is_null(s[i].EstimatedStart);
                var eend = Globals.datetime_is_null(s[i].EstimatedEnd); 

                var astart = Globals.datetime_is_null(s[i].ActualStart);
                var aend = Globals.datetime_is_null(s[i].ActualEnd);
                var con = "<td>" + s[i].Name + "</td><td>" + s[i].ProjectName + "</td><td>" + s[i].RoomName + "</td><td>" + estart +
                    "</td><td>" + eend + "</td><td>" + astart + "</td><td>" + aend + " <ul class='actions'><li class='last'><a href='#myModal' data-toggle='modal' class='task2 edit1'>详情</a> <a class='task2 delete1'>删除</a></li></ul>" +
                    "</td><td style='display:none' name='taskid'>" + s[i].TaskId + "</td><td name='projectstatuscode'style='display:none'>"+s[i].ProjectStatusCode+"</td>"
                var row = document.createElement("tr");
                row.innerHTML = con;
                tbody.append(row)
            }


         


            $(".task2.edit1").click(function () {
                
                location.href = "task-profile.html?taskid=" + $(this).parent().parent().parent().parent().find("[name='taskid']").text() + "&projectstatuscode=" +
                    $(this).parent().parent().parent().parent().find("[name='projectstatuscode']").text();
                })
      
           
            $(".task2.delete1").click(function () {
                var s = $(this).parent().parent().parent().parent().find("[name='projectstatuscode']").text()
                if (s > 2) {
                    alert("项目已开始无法删除")
                } else {
                    if (confirm("删除吗？")) {
                        var jsonPar = {
                            taskid: $(this).parent().parent().parent().parent().find("[name='taskid']").text()
                        }
                        $.ajax({
                            type: "post",
                            url: Globals.ServiceUrl + "DeleteTask",
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
                }
           

            })

        }, error: function (xhr) {
            alert(xhr);
        }
    })
}