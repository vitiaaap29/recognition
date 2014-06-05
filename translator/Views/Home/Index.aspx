<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Index</title>
    <%--
        http://stackoverflow.com/questions/5021552/how-to-reference-a-css-file-on-a-razor-view
        http://stackoverflow.com/questions/121382/is-there-a-way-to-comment-out-markup-in-an-aspx-page
    --%>
    <link href="~/Content/IndexStyles.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/tooltipster.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Scripts/jquery-1.7.1.min.js"></script> 

    <%-- http://www.codeproject.com/Questions/283914/adding-script-reference-to-aspx-page --%>
    <script type="text/javascript" src="/Scripts/home-index.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.tooltipster.min.js"></script> 
    <script>
        $(document).ready(function () {
            $('.word').tooltipster({
                content: 'Loading...',
                functionBefore: function (origin, continueTooltip) {

                    // we'll make this function asynchronous and allow the tooltip to go ahead and show the loading notification while fetching our data
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
        });

    </script>
</head>
<body>
    <div class="editable-area" contenteditable="true">
        
    </div>
</body>
</html>
