//here plan how to do this site:
//http://stackoverflow.com/questions/2918434/javascript-displaying-tooltip-when-mouse-is-hovered-a-certain-word-in-a-textare


function ProcessMouseEventInTextArea() {
    _this = this;
    oldMousePosition = {x: 0, y: 0};
    wasEditing = false;

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
                    //console.log("Sie bla before tooltip " + 
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
                console.log(Date.now() + ' you edit textarea');
            }
        );

        $("div.editable-area").on("mousemove",
            function (e) {
                if (wasEditing) {
                    var diff = { x: Math.abs(oldMousePosition.x - e.pageX), y: Math.abs(oldMousePosition.y - e.pageY) };
                    var distance = Math.sqrt(diff.x * diff.x + diff.y * diff.y);

                    console.log(Date.now() + ' you movve mouse up area distance= ' + distance);
                    oldMousePosition.x = e.pageX;
                    oldMousePosition.y = e.pageY;
                    if (distance > 10) {

                        wrapWordsInSpans();

                        //$('.word').tooltipster('destroy');
                        initTooltipster();

                        console.log(Date.now() + ' DISTANCE LESS 10 ');
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
        // http://clck.ru/9CmRy
        innerHtml = innerHtml.replace(/([\wа-яА-ЯёЁ]+(?=\s(?!class)|<[\w\s]{1,4}>|[\.,\:\'\"]))/g,
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
}

var pseudoObject = null;
$().ready(function () {
    pseudoObject = new ProcessMouseEventInTextArea();
    pseudoObject.init();
});