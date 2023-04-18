using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SRP.TagHelpers
{
    [HtmlTargetElement(Attributes = "active-sidebar")]
    public class ActiveSidebarTagHelper : TagHelper
    {
        public string ActiveSidebar { get; set; }
        private readonly IHttpContextAccessor _accessor;

        public ActiveSidebarTagHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_accessor.HttpContext.Request.Path.Value.StartsWith(ActiveSidebar, StringComparison.CurrentCultureIgnoreCase))
            {
                var existingAttributes = output.Attributes["class"]?.Value;
                output.Attributes.SetAttribute("class",
                    "active__sidebar " + existingAttributes?.ToString());
            }
        }
    }
    [HtmlTargetElement("a", Attributes = "active-submenu")]
    public class ActiveSubmenuTagHelper : TagHelper
    {
        public string ActiveSubmenu { get; set; }
        private readonly IHttpContextAccessor _accessor;

        public ActiveSubmenuTagHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_accessor.HttpContext.Request.Path.Value.EndsWith(ActiveSubmenu, StringComparison.CurrentCultureIgnoreCase))
            {
                var existingAttributes = output.Attributes["class"]?.Value;
                output.Attributes.SetAttribute("class",
                    "active__sidebar " + existingAttributes?.ToString());
            }
        }
    }
    [HtmlTargetElement("a", Attributes = "active-page")]
    public class ActiveTagHelper : TagHelper
    {
        public string ActivePage { get; set; }
        private readonly IHttpContextAccessor _accessor;

        public ActiveTagHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_accessor.HttpContext.Request.QueryString.Value.Contains(ActivePage, StringComparison.CurrentCultureIgnoreCase))
            {
                var existingAttributes = output.Attributes["class"]?.Value;
                output.Attributes.SetAttribute("class",
                    "active " + existingAttributes?.ToString());
            }
        }
    }
    [HtmlTargetElement("a", Attributes = "active-filter")]
    public class ActiveFilterTagHelper : TagHelper
    {
        public string ActiveFilter { get; set; }
        private readonly IHttpContextAccessor _accessor;

        public ActiveFilterTagHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_accessor.HttpContext.Request.QueryString.Value.Contains(ActiveFilter, StringComparison.CurrentCultureIgnoreCase))
            {
                var existingAttributes = output.Attributes["class"]?.Value;
                output.Attributes.SetAttribute("class",
                    "active activeFilter " + existingAttributes?.ToString());
            }
        }
    }
}
