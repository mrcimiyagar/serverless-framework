using System;
using System.IO;
using System.Linq;
using BackEnd.ComposerWriter;
using BackEnd.Models.Modules;

namespace BackEnd.DockerfileWriter.LangHandlers
{
    public class DotnetController : BaseController
    {
        public DotnetController(string appName, string sourceDirPath, string projectName, string outputDllName, DatabaseConfig[] databases) : base(appName, sourceDirPath)
        {
            File.AppendAllLines(
                $"{AppPath}/{projectName}/Dockerfile", 
                new string[] 
                {
                    "FROM mcr.microsoft.com/dotnet/core/runtime:2.2", 
                    $"COPY bin/Release/netcoreapp2.2/ .", 
                    $"ENTRYPOINT [\"dotnet\", \"{outputDllName}.dll\"]"
                });
            
            new MainComWriter(databases.ToList(), appName, AppPath, -1);

            Console.WriteLine("running docker-compose...");
            foreach (var res in $"cd {AppPath}; docker-compose up --no-start;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }
    }
}