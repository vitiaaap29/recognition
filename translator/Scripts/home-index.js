//here plan how to do this site:
//http://stackoverflow.com/questions/2918434/javascript-displaying-tooltip-when-mouse-is-hovered-a-certain-word-in-a-textare


function ProcessMouseEventInTextArea() {
    _this = this;
    oldMousePosition = {x: 0, y: 0};
    wasEditing = false;

    this.init = function () {
        /* set handlers for events */

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
                        //http://www.sitepoint.com/forums/showthread.php?230443-Saving-restoring-caret-position-in-a-contentEditable-div
                        wrapWordsInSpans();
                        console.log(Date.now() + ' DISTANCE LESS 10 ');
                    }
                    wasEditing = false;
                }
           }
       );

        //$('.word').tooltipster();
    }

    /* public methods*/


    /* private methods*/

    function wrapWordsInSpans() {
        var innerHtml = $("div.editable-area").html();
        // First need delete spans from previos invoke methods.
        innerHtml = innerHtml.replace(/<span class=\"word\">([\wа-яА-ЯёЁ\<\>]+?)<\/span>/g,
            "$1");

        // link on regular expression.
        // http://clck.ru/9CmRy
        innerHtml = innerHtml.replace(/([\wа-яА-ЯёЁ]+(?=\s(?!class)|<[\w\s]{1,4}>|[\.,\:\'\"]))/g,
            "<span class=\"word\">$1</span>");

        //if situation <span>loo</span><span>k</span>
        innerHtml = innerHtml.replace(/<\/span><span class=\"word\">/g, "");

        //delete collision <span onmouseover=...></span>
        innerHtml = innerHtml.replace(/<span class=\"word\"><\/span>/g, "");
        $("div.editable-area").html(innerHtml);
    }

}

var pseudoObject = null;
$().ready(function () {
    pseudoObject = new ProcessMouseEventInTextArea();
    pseudoObject.init();
});