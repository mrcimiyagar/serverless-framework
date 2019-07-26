using System;
using System.IO;

namespace X1BackendProject.Controllers
{
    public class JavaController : BaseController
    {
        public JavaController(string appName, string sourceDirPath, string entryClassName) : base(appName, sourceDirPath)
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
            
            foreach (var res in $"cd {AppPath}; docker build -t {AppName} .;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }
    }
}