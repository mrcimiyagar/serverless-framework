using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace X1BackendProject.Controllers
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
            Tools.DirectoryCopy(sourceDirPath, AppPath);
        }

        public virtual IEnumerable<char> Run()
        {
            return $"docker run {AppName}".Bash();
        }
    }
}