<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<span>
    <ul>
        <% foreach (KeyValuePair<String, float> percentAndLang in @Model.PercentTable)
           { %>
            <li><%= percentAndLang.Key + " => " + percentAndLang.Value.ToString() %></li>
        <% } %>
    </ul>
</span>