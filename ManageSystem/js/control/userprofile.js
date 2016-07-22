/// <reference path="../../Page/child/edit-user.html" />
var userJs, userVar;
var id = $.getUrlParam("id");
var createdby = Globals.getCookie("SystemUserId");

function initConfig() {
    //初始化模块JS
    userJs = new Globals.user();

    //页面变量
    userVar = {

    };

    (function () {


        $(document).ready(function () {


            jsonPara = {
                userId: id
            };

            $.ajax({
                type: "post",
                url: Globals.ServiceUrl + "GetUserById",
                data: JSON.stringify(jsonPara),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var user = JSON.parse(data.d);

                    $("#tleName").text(user.Name);
                    $("#spanUsername").text(user.Username);
                }, error: function (xhr) {
                    alert("请联系管理员");
                    return false;
                }
            
            });
        });

        $(".delete-user").click(function () {
            jsonPara = {
                userId: id
            };

            if (confirm("确认要删除该用户?")) {
                $.ajax({
                    type: "post",
                    url: Globals.ServiceUrl + "Delete",
                    data: JSON.stringify(jsonPara),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d) {
                            alert("删除成功");
                            location.href = "user-list.html";
                        }
                        else
                            alert("删除失败");
                        return false;
                    }, error: function (xhr) {
                        alert("请联系管理员");
                        return false;
                    }
                   
                    
                });
            }
        });

        $(".edit").click(function () {
            $(".modal .modal-body").load("form-showcase.html");
            $('.modal').modal('show').css({ width: '800px', height: "500px" });

            $('.modal .modal-body').modal('show').css({ width: '750px'});

            $('.modal').modal('show').on('shown', function () {
                $(".btn-primary").attr('href', 'Del.asp?id=' + id); 

            });


        });
    })();
}

$(function () {
    initConfig();
});


