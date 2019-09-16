using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackEnd.ComposerWriter;
using BackEnd.Models.Modules;

namespace BackEnd.DockerfileWriter.LangHandlers
{
    public class PhpController : BaseController
    {
        public PhpController(string appName, string sourceDirPath, DatabaseConfig[] databases) : base(appName, sourceDirPath)
        {
            Directory.CreateDirectory($"/var/www/html/{appName}/");
            File.WriteAllLines(
                $"{AppPath}/virtual-host.conf",
                new []
                {
                    "<VirtualHost *:80>\n" +
                        $"DocumentRoot /var/www/html/{appName}\n" +
                        "DirectoryIndex index.html\n" + 
                    "</VirtualHost>"
                });
            File.AppendAllLines(
                $"{AppPath}/Dockerfile", 
                new string[] 
                {
                    "FROM php:7.3-apache-stretch",
                    $"COPY . /var/www/html/{appName}/"
                });
            
            new MainComWriter(databases.ToList(), appName, AppPath, -1);

            Console.WriteLine("running docker-compose...");
            foreach (var res in $"cd {AppPath}; docker-compose up --no-start;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }
        
        public IEnumerable<char> RunWebsite(int portNumber)
        {
            return $"docker container start -i -p {portNumber}:80 {AppName}".Bash();
        }
    }
}