using System;
using System.IO;
using System.Linq;
using BackEnd.ComposerWriter;
using BackEnd.Models.Modules;

namespace BackEnd.DockerfileWriter.LangHandlers
{
    public class JavaController : BaseController
    {
        public JavaController(string appName, string sourceDirPath, string entryClassName, DatabaseConfig[] databases) : base(appName, sourceDirPath)
        {
            File.AppendAllLines(
                $"{AppPath}/Dockerfile", 
                new string[] 
                {
                    "FROM java:8", 
                    "COPY . /var/www/java", 
                    "WORKDIR /var/www/java", 
                    $"RUN javac {entryClassName}.java", 
                    $"CMD [\"java\", \"{entryClassName}\"]"
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