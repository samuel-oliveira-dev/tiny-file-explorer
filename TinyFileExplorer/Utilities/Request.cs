using Microsoft.AspNetCore.Http;

namespace TinyFileExplorer.Utilities
{
    public class Request
    {
        public string Path {  get; set; }
        public string Action { get; set; }
        public IFormFile Files { get; set; }
    }
}
