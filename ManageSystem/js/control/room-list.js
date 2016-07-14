var page;
var roleid = Globals.getCookie("RoleId");
if ($.getUrlParam("page") == null || $.getUrlParam("page") == undefined) {
    page = 1;
} else {
    page = $.getUrlParam("page");
}
function room() {
    (function () {
        $("#myModal .modal-body").load("child/edit-room.html");
        $("#login").click(function () {
            if (confirm("确定注销？")) {
                location.href = "login.html";
            }
        });
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetRoomCount",
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
                location.href = "room-list.html?page=" + "1";
            }
        })
        $(".before").click(function () {
            if ($("#PageNo").text() > 1) {
                var number = parseInt($("#PageNo").text() - 1);
                $("#PageNo").text(number);
                location.href = "room-list.html?page=" + number;
            }
        })
        $(".last").click(function () {
            if ($("#PageNo").text() < $("#totalPageNo").text()) {
                var number = parseInt($("#totalPageNo").text());
                $("#PageNo").text(number);
                location.href = "room-list.html?page=" + number;
            }
        })
        $(".next").click(function () {
            if (($("#PageNo").text() < $("#totalPageNo").text())) {
                var number = parseInt($("#PageNo").text())+1;
                $("#PageNo").text(number );
                location.href = "room-list.html?page=" + number;
            }
        })
        $(".Go").click(function () {
            if (($("#totalPageNo").text() >= $("#pageNum").val() >= 1 && $("#pageNum").val() != $("#PageNo").val())) {
                var number = parseInt($("#pageNum").val());
                $("#PageNo").text(number);
                location.href = "room-list.html?page=" + number;
            }
        })
   

        $("#savechange").click(function () {
            if (Globals.trim($("#txtName").val()) != "" && Globals.trim($("#roomcode").val()) != "") {
                var jsonPara = {
                    room: {
                        roomid: $("#roomid").val(),
                        name: $("#txtName").val(),
                        roomcode: Globals.trim($("#roomcode").val())
                    }
                }
                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "UpdateRoom",
                    data: JSON.stringify(jsonPara),
                    success: function (data) {
                        var s = JSON.parse(data.d)
                        if (s) {
                            alert("更新成功");
                            window.location.reload();
                        } else {
                            alert("更新失败");
                        }

                    }, error: function (xhr) {
                        alert(xhr);
                    }
                })
            } else {
                alert("场地和条码不能为空");
            }

        })
    })()
}
$(function () {
  room()
})

function Page(pa) {
    var jsonPar = {
        number:pa
    }
    $.ajax({
        type: "post",
        url: Globals.ServiceUrl + "GetRoomList",
      
        contentType: "application/json; charset=utf-8",
       
        data: JSON.stringify(jsonPar),
        success: function (data) {
            var s = JSON.parse(data.d);
            var tbody = $(".table tbody").empty();
            for (var i in s) {
                var cont = "<td name='roomid' style='display:none'>" + s[i].RoomId + "</td><td>" + s[i].Name + "</td><td>" + s[i].RoomCode + 
                    " <ul class='actions'><li class='last'><a class='room2 edit1' href='#myModal' data-toggle='modal'>编辑</a>  <a class='room2 delete1' >删除</a></li></ul>" + "</td>";
                var row = document.createElement("tr");
                row.innerHTML = cont;
                tbody.append(row);
            }
           
            $(".room2.delete1").click(function () {
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
            $(".room2.edit1").click(function () {

                var jsonPa = {
                    roomid: parseInt($(this).parent().parent().parent().parent().find("[name='roomid']").text())
                }

                $.ajax({
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    url: Globals.ServiceUrl + "GetRoom",
                    //async: false,
                    data: JSON.stringify(jsonPa),
                    success: function (data) {
                        var s = JSON.parse(data.d);
                        $("#txtName").val(s.Name);
                        $("#roomid").val(s.RoomId);
                        $("#roomcode").val(s.RoomCode);

                    }, error: function (xhr) {
                        alert(xhr);
                    }
                })
            })

        }, error: function (xhr) {
            alert(xhr);
        }
    })

}