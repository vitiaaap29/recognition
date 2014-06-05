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

    <%--
        https://github.com/iamceege/tooltipster
        --%>
        $(document).ready(function () {
            
        });

    </script>
</head>
<body>
    <div class="editable-area" contenteditable="true">
        
    </div>
</body>
</html>
