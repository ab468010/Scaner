function room() {
    (function () {
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetRoomCount",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                var page = Math.ceil(s / 10);
                $("#PageNo").text(1);
                $("#totalPageNo").text(page);
            }, error: function (xhr) {
                 alert("请联系管理员");
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
    })()
}
$(function () {
  room()
})

function Page(page) {
    var jsonPar = {
        number:page
    }
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetRoomList",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify(jsonPar),
        success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var cont = "<td name='roomid' style='display:none'>" + s[i].RoomId + "</td><td>" + s[i].Name + "</td><td>" + s[i].RoomCode + 
                    " <ul class='actions'><li class='last'><a class='room2 edit1'href='#myModal' data-toggle='modal'>编辑</a>  <a class='room2 delete1' >删除</a></li></ul>" + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }
            $("#myModal .modal-body").load("child/edit-room.html");
            $(".delete1").click(function () {
                if (confirm("确定删除？")) {
                    var jsonPara = {
                        roomid: parseInt($(this).parent().parent().parent().parent().find("[name='roomid']").text())
                    }
                    $.ajax({
                        type: "post",
                        url: Globals.ServiceUrl + "DeleteRoom",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(jsonPara),
                        success: function (data) {
                            var s = JSON.parse(data.d);
                            if (s) {
                                alert("删除成功")
                                window.location.reload();
                            } else { alert("请先删除对应任务") }
                        }
                    })
                }
            })
            $(".edit1").click(function () {
                var jsonPar = {
                    roomid: parseInt($(this).parent().parent().parent().parent().find("[name='roomid']").text())
                }

                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "GetRoom",
                    data: JSON.stringify(jsonPar),
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        $("#txtName").val(s.Name);
                        $("#roomid").val(s.RoomId);
                        $("#roomcode").val(s.RoomCode);

                    }, error: function (xhr) {
                         alert("请联系管理员");
                    }
                })
            })

            $("#savechange").click(function () {
                var jsonPara = {
                    room: {
                        roomid: $("#roomid").val(),
                        name: $("#txtName").val(),
                        roomcode: $("#roomcode").val()
                    }
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "UpdateRoom",
                    data: JSON.stringify(jsonPara),
                    success: function (data) {
                        alert("更新成功");
                        window.location.reload();
                    }, error: function (xhr) {
                         alert("请联系管理员");
                    }
                })
            })

        }, error: function (xhr) {
             alert("请联系管理员");
        }
    })

}