using Microsoft.Extensions.Options;
using TinyFileExplorer.Configurations;

namespace TinyFileExplorer.Services
{
    public class RootFoldersService
    {
        private readonly IOptions<TinyFileExplorerServiceOptions> _options;
        public RootFoldersService(IOptions<TinyFileExplorerServiceOptions> options) 
        {
            _options = options;
        }
    }
}
