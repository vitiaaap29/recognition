<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<script type="text/javascript">
    function sendAjaxByLanguageLink(id) {
        var wordUnderTolltip = $('#word_in_tooltip').val();
        var language = $('#' + id).text().trim();
        console.log("sendAjaxByLanguageLink: i am work by click: word => " + wordUnderTolltip
            + " id => " + id + " lang =>" + language);
       
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
</script>

<span class="tooltip-content">
    <table>
        <% if (!@Model.IsWordTooSmall) %>
            <% foreach (KeyValuePair<String, float> percentAndLang in @Model.PercentTable)
               { %>
                <tr>
                    <td>
                        <a href="#" id="<%= percentAndLang.Key %>" onclick="sendAjaxByLanguageLink(this.id);">
                            <%= percentAndLang.Key + " "%>
                        </a>
                    </td>

                     <%= percentAndLang.Value.ToString() + '%' %>
                </tr>
            <% } %>
        <% else {%>
            <tr><td>Very small word</td></tr>
        <%} %>
    </table>
    <input id="word_in_tooltip" type="hidden" value="<%=@Model.CurrentWord %>"/>
</span>
