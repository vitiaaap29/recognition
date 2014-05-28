//here plan how to do this site:
//http://stackoverflow.com/questions/2918434/javascript-displaying-tooltip-when-mouse-is-hovered-a-certain-word-in-a-textare

function ProcessMouseEventInTextArea() {
    _this = this;

    this.init = function () {
        /* установка обработки нажатий или иных манипуляций */
        $("textarea.main-text-area").mouseover(
            function () {
                console.log(Date.now() + ' mouse enter in textarea\n');
            }).mouseout(
                function () {
                    console.log(Date.now() + ' mouse leave in textarea\n');
                });
    }

    /* другие публичные методы*/
    this.saySomething = function (id) {
        alert("Пыщь-пыщь! : " + id);
    }

    /* другие приватные методы */
    function saySomething(id) {
        alert("Пыщь-пыщь! Но тссс!: " + id);
    }

}

var functionName = null;
$().ready(function () {
    functionName = new ProcessMouseEventInTextArea();
    //functionName.init();
});