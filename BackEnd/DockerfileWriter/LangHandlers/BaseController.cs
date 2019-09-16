using System.Collections.Generic;
using System.IO;
using BackEnd.Tools;

namespace BackEnd.DockerfileWriter.LangHandlers
{
    public class BaseController
    {
        public string AppName { get; set; }
        public string AppPath { get; set; }
        
        public BaseController(string appName, string sourceDirPath)
        {
            AppName = appName;
            AppPath = $"{Directory.GetCurrentDirectory()}/containers/{appName}";
            Directory.CreateDirectory(AppPath);
            DirectoryHandler.DirectoryCopy(sourceDirPath, AppPath);
        }

        public virtual IEnumerable<char> Run()
        {
            return $"docker container start -i {AppName}".Bash();
        }
    }
}