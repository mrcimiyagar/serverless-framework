using System;
using System.Collections.Generic;
using System.IO;

namespace X1BackendProject.Controllers
{
    public class PhpController : BaseController
    {
        public PhpController(string appName, string sourceDirPath) : base(appName, sourceDirPath)
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
            foreach (var res in $"cd {AppPath}; docker build -t {AppName} .;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }
        
        public IEnumerable<char> RunWebsite(int portNumber)
        {
            return $"docker run -p {portNumber}:80 {AppName}".Bash();
        }
    }
}