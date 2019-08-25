using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using X1BackendProject.ComposerWriter;
using X1BackendProject.Models;

namespace X1BackendProject.DockerfileWriter.LangHandlers
{
    public class NodejsController : BaseController
    {
        public NodejsController(string appName, string sourceDirPath, string driverFilePath, int portNumber, DatabaseConfig[] databases) : base(appName, sourceDirPath)
        {
            File.WriteAllLines(
                $"{AppPath}/Dockerfile", 
                new string[]
                {
                    "FROM node:10",
                    $"WORKDIR /{appName}",
                    "COPY package*.json ./",
                    "RUN npm install",
                    "COPY . .",
                    $"CMD [\"node\", \"{driverFilePath}\"]"
                });
            
            new MainComWriter(databases.ToList(), appName, AppPath, portNumber);

            Console.WriteLine("running docker-compose...");
            foreach (var res in $"cd {AppPath}; docker-compose up --no-start;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }

        public IEnumerable<char> RunWebsite()
        {
            return $"cd {AppPath}; docker-compose up".Bash();
        }
    }
}