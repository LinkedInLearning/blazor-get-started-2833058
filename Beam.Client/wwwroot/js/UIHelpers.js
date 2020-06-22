window.UIHelpers = {
    focusElement : function (element) { element.focus(); },
    focusFirstChild : function (element) {element.firstElementChild.focus();},
    animateElement : function (element, keyFrames, speed) {
        element.firstElementChild.animate(keyFrames, speed);
    }
}