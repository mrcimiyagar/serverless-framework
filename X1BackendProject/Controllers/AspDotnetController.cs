using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace X1BackendProject.Controllers
{
    public class AspDotnetController : BaseController
    {
        public AspDotnetController(string appName, string sourceDirPath, string projectName, string outputDllName) : base(appName, sourceDirPath)
        {
            File.WriteAllLines(
                $"{AppPath}/{projectName}/Dockerfile", 
                new string[] 
                {
                    "FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env",
                    $"WORKDIR /{projectName}/",
                    $"COPY *.csproj ./",
                    "RUN dotnet restore",
                    "COPY . ./",
                    "RUN dotnet publish -c Release -o out",
                    "FROM mcr.microsoft.com/dotnet/core/aspnet:2.2",
                    $"WORKDIR /{projectName}/",
                    $"COPY --from=build-env /{projectName}/out/ .",
                    $"ENTRYPOINT [\"dotnet\", \"{outputDllName}.dll\"]",
                });
            
            foreach (var res in $"cd {AppPath}/{projectName}; docker build -t {AppName} .;".Bash())
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