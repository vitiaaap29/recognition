//here plan how to do this site:
//http://stackoverflow.com/questions/2918434/javascript-displaying-tooltip-when-mouse-is-hovered-a-certain-word-in-a-textare

function ProcessMouseEventInTextArea() {
    _this = this;
    counterPress = 0;
    

    this.init = function () {
        /* установка обработки нажатий или иных манипуляций */

        //$("div.editable-area").mouseover(
        //    function () {
        //        /* here was up tooltip*/
        //        console.log(Date.now() + ' mouse enter in textarea\n');
        //    }).mouseout(
        //        function () {
        //            /* here was down tooltip*/
        //            console.log(Date.now() + ' mouse leave in textarea\n');
        //        });

        $("div.editable-area").on("paste copy cut",
            function () {
                counterPress--;
                console.log(Date.now() + ' you edit textarea');
            }
        );

        $("div.editable-area").on("keyup",
           function () {
               counterPress++;
               if (counterPress % 4 == 0) {
                   wrapWordsInSpans();
                   console.log(Date.now() + ' you edit textarea ' + counterPress);
               }
           }
       );
    }

    /* public methods*/


    /* private methods*/

    function wrapWordsInSpans() {
        var innerHtml = $("div.editable-area").html();
        /* link on regular expression
         * http://scriptular.com/#[\w]%2B%28%3F%3D\s|%3Cbr%3E%29||||g||||[%22i%20look%20inside%20my%20self%3Cbr%3E%3Cbr%3Eand%20i%20see%20my%20%3Cbr%3E%3Cbr%3Ehurt%20is%20black%3Cbr%3E%22%2C%22%20%20%20%20%20%20%20%20%22%2C%22%20%20%20%20%22]
         */
        innerHtml = innerHtml.replace(/([\w]+(?=\s|<br>))/g, "<span class='word'>$1</span>");
        $("div.editable-area").html(innerHtml);
    }
}

var pseudoObject = null;
$().ready(function () {
    pseudoObject = new ProcessMouseEventInTextArea();
    pseudoObject.init();
});