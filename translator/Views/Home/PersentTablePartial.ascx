<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<span>
    <table>
        <% if (!@Model.IsWordTooSmall) %>
            <% foreach (KeyValuePair<String, float> percentAndLang in @Model.PercentTable)
               { %>
                <tr>
                    <td>
                        <a href="*" onclick="sendAjaxByLanguageLink();">
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
</span>

<script type="text/javascript">
    function sendAjaxByLanguageLink() {
        console.log(this);
    }
</script>