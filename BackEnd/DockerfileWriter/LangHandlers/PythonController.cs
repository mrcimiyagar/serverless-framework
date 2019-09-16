using System;
using System.IO;
using System.Linq;
using BackEnd.ComposerWriter;
using BackEnd.Models.Modules;

namespace BackEnd.DockerfileWriter.LangHandlers
{
    public class PythonController : BaseController
    {
        public PythonController(string appName, string sourceDirPath, string[] librariesList, string executionCommand, DatabaseConfig[] databases) : base(appName, sourceDirPath)
        {
            File.WriteAllLines(
                $"{AppPath}/Dockerfile", 
                new []
                {
                    "FROM python:3",
                    $"WORKDIR /{appName}",
                    "COPY . ."
                });
            foreach (var libName in librariesList)
            {
                if (string.IsNullOrEmpty(libName)) continue;
                File.AppendAllLines(
                    $"{AppPath}/Dockerfile",
                    new []
                    {
                        $"RUN pip install {libName}"
                    });
            }
            File.AppendAllLines(
                $"{AppPath}/Dockerfile",
                new []
                {
                    $"CMD [\"{executionCommand}\"]"
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