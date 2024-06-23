using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace TinyFileExplorer.Configurations
{
    
    public class EmbebedStaticFilesMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly string _namespace;
        private readonly Assembly _assembly;

        public EmbebedStaticFilesMiddlware(RequestDelegate next, string @namespace, Assembly assembly)
        {
            _next = next;
            _namespace = @namespace;
            _assembly = assembly;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value.TrimStart('/');
            var resourcePath = $"{_namespace}.wwwroot.{path.Replace("/", ".")}";

            if (_assembly.GetManifestResourceStream(resourcePath) != null)
            {
                using (var stream = _assembly.GetManifestResourceStream(resourcePath))
                {
                    var response = context.Response;
                    response.ContentType = GetContentType(path);
                    await stream.CopyToAsync(response.Body);
                }
            }
            else
            {
                await _next(context);
            }
        }

        private string GetContentType(string path)
        {
            return path.EndsWith(".js") ? "application/javascript" :
                   path.EndsWith(".css") ? "text/css" : "application/octet-stream";
        }


    }

    public static class EmbeddedStaticFilesMiddlewareExtensions
    {
        public static IApplicationBuilder UseEmbeddedStaticFiles(this IApplicationBuilder builder, string @namespace, Assembly assembly)
        {
            return builder.UseMiddleware<EmbebedStaticFilesMiddlware>(@namespace, assembly);
        }
    }



}
