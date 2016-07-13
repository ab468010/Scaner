//登陆查询用户
//$("."+s[i].Tablename).attr("display","none")
function existcookie()
{
    var de='delete1';
    var ed='edit1';
    var cr='create1';
    var jsonPar;
    if ((Globals.getCookie("UserName") == "" || Globals.getCookie("UserName") == null) || Globals.getCookie("RoleId") == null || Globals.getCookie("RoleId") == "")
    {

        location.href = "login.html";
        alert("请登录");
    }
    else {
        
        $("#UserName").text(Globals.getCookie("UserName"));
        $(".project1").attr({ style: "display:none" });
        $(".task1").attr({ style: "display:none" });
        $(".sample1").attr({ style: "display:none" });
        $(".customer1").attr({ style: "display:none" });
        $(".contact1").attr({ style: "display:none" });
        $(".container1").attr({ style: "display:none" });
        $(".systemuser1").attr({ style: "display:none" });
        $(".role1").attr({ style: "display:none" });
        $(".shelf1").attr({ style: "display:none" });
        $(".privilege1").attr({ style: "display:none" });
        $(".room1").attr({ style: "display:none" });
        if (Globals.getCookie("RoleId") == 6) {
            $("#changepwd").attr({ style: "margin-right:inline" });
        } else {
            $(".projecttemplate1").attr({ style: "display:none" });
            $(".containerwarning1").attr({ style: "display:none" });
        }
      
       
    

        $(".project2.edit1").attr({ style: "display:none" });
        $(".task2.edit1").attr({ style: "display:none" });
        $(".sample2.edit1").attr({ style: "display:none" });
        $(".customer2.edit1").attr({ style: "display:none" });
        $(".contact2.edit1").attr({ style: "display:none" });
        $(".container2.edit1").attr({ style: "display:none" });
        $(".systemuser2.edit1").attr({ style: "display:none" });
        $(".role2.edit1").attr({ style: "display:none" });
        $(".shelf2.edit1").attr({ style: "display:none" });
        $(".privilege2.edit1").attr({ style: "display:none" });
        $(".room2.edit1").attr({ style: "display:none" });

        $(".project2.delete1").attr({ style: "display:none" });
        $(".task2.create1").attr({ style: "display:none" });
        $(".sample2.create1").attr({ style: "display:none" });
        $(".customer2.create1").attr({ style: "display:none" });
        $(".contact2.create1").attr({ style: "display:none" });
        $(".container2.create1").attr({ style: "display:none" });
        $(".systemuser2.create1").attr({ style: "display:none" });
        $(".role2.create1").attr({ style: "display:none" });
        $(".shelf2.create1").attr({ style: "display:none" });
        $(".privilege2.create1").attr({ style: "display:none" });
        $(".room2.create1").attr({ style: "display:none" });

        $(".project2.delete1").attr({ style: "display:none" });
        $(".task2.delete1").attr({ style: "display:none" });
        $(".sample2.delete1").attr({ style: "display:none" });
        $(".customer2.delete1").attr({ style: "display:none" });
        $(".contact2.delete1").attr({ style: "display:none" });
        $(".container2.delete1").attr({ style: "display:none" });
        $(".systemuser2.delete1").attr({ style: "display:none" });
        $(".role2.delete1").attr({ style: "display:none" });
        $(".shelf2.delete1").attr({ style: "display:none" });
        $(".privilege2.delete1").attr({ style: "display:none" });
        $(".room2.delete1").attr({ style: "display:none" });

        jsonPar = {
            roleid: Globals.getCookie("RoleId")
        };
        $.ajax({
            type: "post",
            contentType: "application/json; charset=utf-8",
            url: Globals.ServiceUrl + "GetRolePrivilegeList",
            data: JSON.stringify(jsonPar),
            success: function (data) {
                var s = JSON.parse(data.d);
               
                for (var i in s) {

                    $("." + s[i].Tablename + 1).attr({ style: "display:inline" });
                  
                    if (s[i].CanDelete == true) {
                        $("."+s[i].Tablename+2+"."+ de).attr({ style: "display:inline" });
                    }
                    if (s[i].CanCreate == true) {
                        $("." + s[i].Tablename + 2 + "." + cr).attr({ style: "display:inline" });
                    }
                    if (s[i].CanWrite == true) {
                        $("." + s[i].Tablename + 2 + "." + ed).attr({ style: "display:inline" });
                    }
                }
              
            }, error: function (xhr) {
                alert(xhr);
            }
        })
    }

}

$(function () {
   existcookie();
});