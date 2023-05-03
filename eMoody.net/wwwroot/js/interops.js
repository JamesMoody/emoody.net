

window.blazorInterops = {

    initBootstrapSelects: function (targetSelector) {
        //$(function () {
            console.log("initBootstrapSelects: " + targetSelector);
            $(targetSelector).selectpicker();
        //});
    }
};
