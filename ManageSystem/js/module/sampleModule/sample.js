Globals.sample = (function () {

    /*Private Member*/
    var _Name = "Sample";

    /*Private Function*/
    function _findCore() {
        alert("该方法为" + _Name);
    }

    function _getSampleClassText(sampleClass)
    {
        var sampleClassText = "";
        switch (sampleClass)
        {
            case 1:
                sampleClassText = "样品";
                break;
            case 2:
                sampleClassText = "附件";
                break;
        }

        return sampleClassText;
    }

    function constructor() {
        this.getSampleClassText = _getSampleClassText;
    }



    return constructor;

})();