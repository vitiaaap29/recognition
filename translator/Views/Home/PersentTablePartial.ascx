<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<script type="text/javascript">
    function sendAjaxByLanguageLink(id) {
        var wordUnderTolltip = $('#word_in_tooltip').val();
        var language = $('#' + id).text().trim();
       
        $.ajax({
            type: 'POST',
            url: "/Home/AddWord",
            data: { 'word': wordUnderTolltip, 'lang': language },
            success: function (response) {
                $('.tooltip-content').html(response);
            }
        });

        return false;
    }

    function displayFormAddLang() {

    }
</script>

<span class="tooltip-content">
    <table>
        <% if (!@Model.IsWordTooSmall) %>
            <% foreach (KeyValuePair<String, float> percentAndLang in @Model.PercentTable)
               { %>
                <tr>
                    <td>
                        <a href="#" id="<%= percentAndLang.Key %>" onclick="sendAjaxByLanguageLink(this.id);" style="color: thistle">
                            <%= percentAndLang.Key + " "%>
                        </a>
                    </td>
                    <td>
                        <%= percentAndLang.Value.ToString() + '%' %>
                    </td>
                     
                </tr>
            <% } %>
        <% else {%>
            <tr><td>Very small word</td></tr>
        <%} %>
    </table>
    <%--<div id="form_add_lang">
        <a href="#" onclick="displayFormAddLang()">Add language</a>
        <form id="lang-form" action="/Home/AddLang" method="POST" data-ajax-url="/Home/AddCommentAjax" data-ajax-success="OnSuccessComment" data-ajax-method="POST" data-ajax="true">
            <input type="text" name="lang" value="Any Lang"/>
            <input type="submit" value="Add">
        </form>
    </div>
    <a href="#" onclick="displayFormAddLang()">Add language</a>--%>
    <input id="word_in_tooltip" type="hidden" value="<%=@Model.CurrentWord %>"/>
</span>
