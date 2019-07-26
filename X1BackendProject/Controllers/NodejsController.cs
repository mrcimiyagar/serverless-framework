using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace X1BackendProject.Controllers
{
    public class NodejsController : BaseController
    {
        public NodejsController(string appName, string sourceDirPath, string driverFilePath) : base(appName, sourceDirPath)
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
            
            foreach (var res in $"cd {AppPath}; docker build -t {AppName} .;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }

        public IEnumerable<char> RunWebsite(int portNumber)
        {
            return $"docker run --name {AppName} -p {portNumber}:3000 {AppName}".Bash();
        }
    }
}