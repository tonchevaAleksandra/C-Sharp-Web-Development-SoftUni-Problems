﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetAppForTestingRazor.TagHelper
{
    [HtmlTargetElement("table", Attributes = "bootstrap-table")]
    public class BootstrapTableTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        public string TableName { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PreElement.AppendHtml($"<h2>{TableName}</h2>");
            output.Attributes.RemoveAll("bootstrap-table");
            output.Attributes.Add(new TagHelperAttribute("class", "table table-striped table-hover table-sm"));
        }
    }

    public class Test : BootstrapTableTagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            //output.Attributes[];
        }
    }
}
