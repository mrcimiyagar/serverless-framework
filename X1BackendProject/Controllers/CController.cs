using System;
using System.IO;

namespace X1BackendProject.Controllers
{
    public class CController : BaseController
    {
        public CController(string appName, string sourceDirPath, string[] cFiles) : base(appName, sourceDirPath)
        {
            var cFilesStr = "";
            foreach (var cFile in cFiles) cFilesStr += cFile + " ";
            cFilesStr = cFilesStr.Trim();
            
            File.AppendAllLines(
                $"{AppPath}/Dockerfile", 
                new string[] 
                {
                    "FROM gcc:4.9",
                    $"COPY . /{appName}",
                    $"WORKDIR /{appName}",
                    $"RUN gcc -o {appName} {cFilesStr}",
                    $"CMD [\"./{appName}\"]"
                });
            
            foreach (var res in $"cd {AppPath}; docker build -t {AppName} .;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }
    }
}