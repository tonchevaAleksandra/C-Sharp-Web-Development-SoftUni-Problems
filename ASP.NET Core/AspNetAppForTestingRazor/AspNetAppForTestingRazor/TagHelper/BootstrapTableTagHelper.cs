using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetAppForTestingRazor.TagHelper
{
    [HtmlTargetElement("table", Attributes = "bootstrap-table")]
    public class BootstrapTableTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("bootstrap-table");
            output.Attributes.Add(new TagHelperAttribute("class", "table table-striped table-hover table-sm"));
        }
    }
}
