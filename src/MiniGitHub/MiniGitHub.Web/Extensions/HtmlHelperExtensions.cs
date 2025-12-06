using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MiniGitHub.Web.Extensions;

public static class HtmlHelperExtensions {
    
    public static IHtmlContent SubmitButton(this IHtmlHelper self, string text, object? htmlAttributes = null) {
        TagBuilder builder = new TagBuilder("button");
        builder.MergeAttribute("type", "submit");
        builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
        builder.InnerHtml.SetContent(text);
        return builder;
    }
    
}