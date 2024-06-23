namespace TinyFileExplorer.Utilities
{
    
    public class FileHandler
    {
    
        public void DeleteFile(string root, string path)
        {
            
            System.IO.File.Delete(root + path);
        }
    }
}
