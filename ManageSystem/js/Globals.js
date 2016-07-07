var Globals = {};

Globals.namespace = function (str) {
    var arr = str.split("."), o = Globals;

    for (i = (arr[0] == "Globals") ? 1 : 0; i < arr.length; i++) {
        o[arr[i]] = o[arr[i]] || {};
        o = o[arr[i]];
    }
}

Globals.ServiceUrl = "../handle/UserHandler.aspx/";
Globals.datetime_is_null = datetime_is_null;
Globals.getCookie = getCookie;
Globals.trim = trim;
Globals.ltrim = ltrim;
Globals.rtrim = rtrim;

//Jquery 扩展函数 获取url参数
(function ($) {
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
    //判断传值是否为空
    $.IsNull = function (obj) {
        if (!obj && typeof (obj) != "undefined" && obj != 0) {
            return true;
        }
        else
            return false;
    }
})(jQuery);

Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function datetime_is_null(time) {
    var errorDate = new Date("1900/1/1");
    var aTime = new Date(time);

    return (errorDate - aTime == 0) ? "空" : aTime.Format('yyyy-MM-dd');
}

function getCookie(name) {
    var cookie_start = document.cookie.indexOf(name);
    var cookie_end = document.cookie.indexOf(";", cookie_start);
    return cookie_start == -1 ? '' : unescape(document.cookie.substring(cookie_start + name.length + 1, (cookie_end > cookie_start ? cookie_end : document.cookie.length)));
}

//调用： 

//var time1 = new Date().Format("yyyy-MM-dd");
//var time2 = new Date().Format("yyyy-MM-dd HH:mm:ss");

//String.prototype.trim = function () {
//    return this.replace(/(^\s*)|(\s*$)/g, "");
//}
//String.prototype.ltrim = function () {
//    return this.replace(/(^\s*)/g, "");
//}
//String.prototype.rtrim = function () {
//    return this.replace(/(\s*$)/g, "");
//}

//删除左右两端的空格
　 function trim(str){ 
　　     return str.replace(/(^\s*)|(\s*$)/g,"");
　　 }
　　 function ltrim(str){ //删除左边的空格
　　     return str.replace(/(^\s*)/g,"");
　　 }
　　 function rtrim(str){ //删除右边的空格
　　     return str.replace(/(\s*$)/g,"");
　　 }