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

        public DirectoryTagHelper(IOptions<TinyFileExplorerServiceOptions> options)
        {
            _options = options;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            string rootPath = "C:\\Users\\samuk\\Documents\\FileHub\\RootFolder";
            var rootDirectory = new VirtualDirectory(rootPath);
            rootDirectory.Tree.Explore(rootDirectory.Tree.Root);

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

                            </div>
";

            var scriptTag = new TagBuilder("script");
            scriptTag.InnerHtml.AppendHtml(script);
            
            var styleTag = new TagBuilder("style");
            styleTag.InnerHtml.AppendHtml(style);

            output.PostElement.AppendHtml(scriptTag);
            output.PostElement.AppendHtml(styleTag);

            // Generate HTML for the directory structure
            var html = rootDirectory.GenerateHtml(rootDirectory.Tree.Root);
            //Console.WriteLine(Resources.Css + html + Resources.Javascript);
            output.Content.AppendHtml(contextMenu + html );
        }

  
    }
}
