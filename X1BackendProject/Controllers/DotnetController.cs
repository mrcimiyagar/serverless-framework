using System;
using System.IO;

namespace X1BackendProject.Controllers
{
    public class DotnetController : BaseController
    {
        public DotnetController(string appName, string sourceDirPath, string projectName, string outputDllName) : base(appName, sourceDirPath)
        {
            File.AppendAllLines(
                $"{AppPath}/{projectName}/Dockerfile", 
                new string[] 
                {
                    "FROM mcr.microsoft.com/dotnet/core/runtime:2.2", 
                    $"COPY bin/Release/netcoreapp2.2/ .", 
                    $"ENTRYPOINT [\"dotnet\", \"{outputDllName}.dll\"]"
                });
            
            foreach (var res in $"cd {AppPath}/{projectName}; docker build -t {AppName} .;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }
    }
}