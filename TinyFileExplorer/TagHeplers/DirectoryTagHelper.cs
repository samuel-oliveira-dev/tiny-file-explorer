using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;
using System.Text;
using TinyFileExplorer.Configurations;
using TinyFileExplorer.Utilities;

namespace TinyFileExplorer.TagHelpers
{
    [HtmlTargetElement("directory")]
    public class DirectoryTagHelper : TagHelper
    {
        private readonly IOptions<TinyFileExplorerServiceOptions> _options;

        [HtmlAttributeName("asp-controller")]
        public string AspController {  get; set; }

        [HtmlAttributeName("asp-action")]
        public string AspAction {  get; set; }

        [HtmlAttributeName("asp-root")]
        public string AspRoot {  get; set; }

        public DirectoryTagHelper(IOptions<TinyFileExplorerServiceOptions> options)
        {
            _options = options;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var rootDirectory = _options.Value.RootDirectories.Where(r => r.Name == AspRoot).FirstOrDefault();
            var path = rootDirectory.Path != null ? rootDirectory.Path : "";

            //string rootPath = "C:\\Users\\samuel.oliveira\\Documents\\RootFolder";
            var virtualDirectory = new VirtualDirectory(path);

            // Load JavaScript and CSS files

            Console.WriteLine("Ok");
            foreach(var op in _options.Value.RootDirectories)
            {
                Console.WriteLine(op);
            }

    

            var script = Properties.Resources.tinyFileExplorerScript.ToString();
            var style = Properties.Resources.tinyFileExplorerStyle.ToString();
            var contextMenu = @"<div id=""contextMenu"" class=""context-menu"">
                                <div class=""item"">
                                    <i></i> Delete
                                </div>
                                <div class=""item"">
                                    <i></i> Copy
                                </div>

                            </div>";

            var scriptTag = new TagBuilder("script");
            scriptTag.InnerHtml.AppendHtml(script);
            
            var styleTag = new TagBuilder("style");
            styleTag.InnerHtml.AppendHtml(style);

            output.PostElement.AppendHtml(scriptTag);
            output.PostElement.AppendHtml(styleTag);
            output.Attributes.SetAttribute("id", "directory-container");

            output.Attributes.SetAttribute("asp-controller", "");
            output.Attributes.SetAttribute("asp-action", "");
            if (!string.IsNullOrEmpty(AspController))
            {
                output.Attributes.SetAttribute("asp-controller", AspController);
                
            }

            if(!string.IsNullOrEmpty(AspAction))
            {
                output.Attributes.SetAttribute("asp-action", AspAction);
            }

            // Generate HTML for the directory structure
            var html = virtualDirectory.GenerateHtml(virtualDirectory.Tree.Root);
            output.Content.AppendHtml(html);
            output.TagName = "div";
        }

  
    }
}
