var id = $.getUrlParam("id");
var address;
$(function () {
 
    $("#login").click(function () {
        if (confirm("确定注销？")) {
            location.href = "login.html";
        }
    });
    $.ajax({
        type: "post",
        contentType: "application/json; charset=utf-8",
        url: Globals.ServiceUrl + "ScustomerId",
        success: function (data) {
            var s = JSON.parse(data.d);
            var option = $("#Select1").empty();
            for (var i in s) {
                option.append($("<option>").val(s[i].Customerid).text(s[i].Name));
            }
            $('#Select1').selectpicker('refresh');
            if (id!=""&&id!=null) {
                $("#Select1").attr("disabled", "disabled");
                $("#Select1 option").attr("selected", false);
                $("#Select1 option[value=" + id + "]").attr("selected", true);
                $('#Select1').selectpicker('refresh');
                
                 address = "customer-profile.html?id=" + id;
            } else {
                $('#Select1').selectpicker('refresh');
                 address = "contact-list.html";
            }
            
        }, error: function (xhr) {
            alert(xhr);
        }
    })
 
    $("#btnSave").click(function () {
        if (Globals.trim($("#name").val())!= "") {
            var jsonPar = {
                contact: {
                    name: $("#name").val(),
                    customerid: $("#Select1").val(),
                    description: $("#Description").val()
                }
            }
            $.ajax({
                type: "post",
                contentType: "application/json;charset=utf-8",
                url: Globals.ServiceUrl + "Incontact",
                data: JSON.stringify(jsonPar),
                dataType: "json",
                success: function (data) {
                    var s = JSON.parse(data.d);
                    if (s) {
                        alert("创建成功");
                        location.href = address;
                    } else {
                        alert("创建失败");
                    }

                }, error: function (xhr) {
                    alert(xhr);
                }
            })
        } else {
            alert("姓名不能为空");
        }
           
        })
})