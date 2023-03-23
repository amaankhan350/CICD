using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

[HtmlTargetElement(LocalizeTagName)] // This is for <localize>Content here</localize> syntax
[HtmlTargetElement(Attributes = LocalizeTagName)] // This is for <p localize>Content here</p> syntax, which I personally prefer
public class LocalizeTagHelper : TagHelper
{
    private const string LocalizeTagName = "localize";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var childContent = await output.GetChildContentAsync();
        var content = childContent.GetContent();

        // Removes the "localize" tag
        if (output.TagName == LocalizeTagName)
            output.TagName = null;

        // Empty tag content...
        if (string.IsNullOrEmpty(content))
        {
            await base.ProcessAsync(context, output);
            return;
        }

        // Get the localization (here using .NET ResourceManager), or the original content if not found
        //var localization = AppResources.ResourceManager.GetString(content) ?? content;

        output!.Content!.SetHtmlContent("");
    }
}