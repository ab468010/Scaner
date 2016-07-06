Globals.project = (function () {

    /*Private Member*/
    var _Name = "Project";
    /*Private Function*/
    function _findCore() {
        alert("该方法为" + _Name);
    }
    function _bulidstatus(a) {
        var statusName = "";
        switch (a)
        {
            case 1:
                statusName = "项目创建";
                break;
            case 2:
                statusName = "项目测试";
                break;
            case 3:
                statusName = "测试完成";
                break;
            case 4:
                statusName = "归还货架";
                break;
            case 5:
                statusName = "项目完成";
                break;
        }
        return statusName;
    }


    function constructor()
    {
        this.bulidstatus = _bulidstatus;
    }



    return constructor;

})();