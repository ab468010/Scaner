Globals.container = (function () {

    /*Private Member*/
    var _Name = "Container";

    /*Private Function*/
    function _findCore() {
        alert("该方法为" + _Name);
    }
    function _bulidstatus(a) {
        var statusName = "";
        switch (a) {
            case 1:
                statusName = "空";
                break;
            case 2:
                statusName = "占用";
                break;
        }
        return statusName;
    }


    function constructor() {
        this.bulidstatus = _bulidstatus;
    }



    return constructor;

})();