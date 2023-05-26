

window.blazorInterops = {

    initBootstrapSelects: function (targetSelector) {
        console.log("initBootstrapSelects: " + targetSelector);
        $(targetSelector).selectpicker();
    }
};
