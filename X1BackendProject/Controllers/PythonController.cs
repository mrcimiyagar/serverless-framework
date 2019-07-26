using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace X1BackendProject.Controllers
{
    public class PythonController : BaseController
    {
        public PythonController(string appName, string sourceDirPath, string[] librariesList, string driverFilePath) : base(appName, sourceDirPath)
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
                    $"CMD [\"python\", \"{driverFilePath}.py\"]"
                });


            foreach (var res in $"cd {AppPath}; docker build -t {AppName} .;".Bash())
            {
                Console.Write(res);
            }
            Console.WriteLine();
        }
    }
}