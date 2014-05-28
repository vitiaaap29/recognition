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

    <script type="text/javascript" src="/Scripts/jquery-1.7.1.min.js"></script> 

    <%-- http://www.codeproject.com/Questions/283914/adding-script-reference-to-aspx-page --%>
    <script type="text/javascript" src="/Scripts/home-index.js"></script>
</head>
<body>
    <div>
        <% using (Html.BeginForm()){ %>
            <%-- http://stackoverflow.com/questions/7895016/mvc3-html-helper-for-large-text-area --%>
            <%= Html.TextArea("MangleWord", "Распознавание языка коротких слов", 5, 100,
                new {@class = "main-text-area", })%>
        <% }%>
    </div>
</body>
</html>
