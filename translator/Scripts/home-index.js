﻿//here plan how to do this site:
//http://stackoverflow.com/questions/2918434/javascript-displaying-tooltip-when-mouse-is-hovered-a-certain-word-in-a-textare


function ProcessMouseEventInTextArea() {
    _this = this;
    oldMousePosition = {x: 0, y: 0};
    wasEditing = false;
    sensitivityMouse = 7;

    this.init = function () {
        /* set handlers for events */

        $('.word').tooltipster({
            interactive: true,
            //autoClose: false,
            content: 'Loading...',
            functionBefore: function (origin, continueTooltip) {

                continueTooltip();

                // next, we want to check if our data has already been cached
                if (origin.data('ajax') !== 'cached') {
                    var wordInCurrentSpan = $(origin).val();
                    $.ajax({
                        type: 'POST',
                        url: "/Home/Recognize",
                        data: { 'word': wordInCurrentSpan },
                        success: function (data) {
                            // update our tooltip content with our returned data and cache it
                            origin.tooltipster('content', data).data('ajax', 'cached');
                        }
                    });
                }
            }
        });

        $("div.editable-area").on("paste copy cut keyup",
            function () {
                wasEditing = true;
            }
        );

        $("div.editable-area").on("mousemove",
            function (e) {
                if (wasEditing) {
                    var diff = { x: Math.abs(oldMousePosition.x - e.pageX), y: Math.abs(oldMousePosition.y - e.pageY) };
                    var distance = Math.sqrt(diff.x * diff.x + diff.y * diff.y);

                    oldMousePosition.x = e.pageX;
                    oldMousePosition.y = e.pageY;
                    if (distance > sensitivityMouse) {
                        wrapWordsInSpans();

                        initTooltipster();
                        placeCaretAtEnd(document.getElementById("editable_div"));
                    }
                    wasEditing = false;
                }
           }
        );
        
    }

    /* public methods*/


    /* private methods*/

    function wrapWordsInSpans() {
        var innerHtml = $("div.editable-area").html();
        // First need delete spans from previos invoke methods.
        // http://clck.ru/9DPyQ
        innerHtml = innerHtml.replace(/<span class=\"word tooltipstered\">(([\s\wа-яА-ЯёЁ\<\>]+?)|())<\/span>/g,
            "$1");

        // Wrap words in spans.
        // http://clck.ru/9DU2v
        innerHtml = innerHtml.replace(/([\wа-яА-ЯёЁ]+(?=\s(?!class)|<[\w\s]{1,4}>|[\.\?\!,\:\'\"\&]|[<]|$))/g,
            "<span class=\"word tooltipstered\">$1</span>");

        //if situation <span>loo</span><span>k</span>
        innerHtml = innerHtml.replace(/<\/span><span class=\"word tooltipstered\">/g, "");

        //delete collision <span onmouseover=...></span>
        innerHtml = innerHtml.replace(/<span class=\"word tooltipstered\"><\/span>/g, "");
        $("div.editable-area").html(innerHtml);
    }

    function initTooltipster() {
        $('.word').tooltipster({
            interactive: true,
            content: 'Loading...',
            functionBefore: function (origin, continueTooltip) {

                continueTooltip();

                // next, we want to check if our data has already been cached
                if (origin.data('ajax') !== 'cached') {
                    var wordInCurrentSpan = $(origin).html();
                    console.log("Sie bla before tooltip " + wordInCurrentSpan);
                    $.ajax({
                        type: 'POST',
                        url: "/Home/Recognize",
                        data: { 'word': wordInCurrentSpan },
                        success: function (data) {
                            // update our tooltip content with our returned data and cache it
                            origin.tooltipster('content', $(data)).data('ajax', 'cached');
                        }
                    });
                }
            }
        });
    }

    //http://stackoverflow.com/questions/4233265/contenteditable-set-caret-at-the-end-of-the-text-cross-browser/4238971#4238971
    function placeCaretAtEnd(el) {
        el.focus();
        if (typeof window.getSelection != "undefined"
                && typeof document.createRange != "undefined") {
            var range = document.createRange();
            range.selectNodeContents(el);
            range.collapse(false);
            var sel = window.getSelection();
            sel.removeAllRanges();
            sel.addRange(range);
        } else if (typeof document.body.createTextRange != "undefined") {
            var textRange = document.body.createTextRange();
            textRange.moveToElementText(el);
            textRange.collapse(false);
            textRange.select();
        }
    }
}

var pseudoObject = null;
$().ready(function () {
    pseudoObject = new ProcessMouseEventInTextArea();
    pseudoObject.init();
    //heigth by screen heigth
    document.getElementById('editable_div').style.height = window.innerHeight + 'px';
});