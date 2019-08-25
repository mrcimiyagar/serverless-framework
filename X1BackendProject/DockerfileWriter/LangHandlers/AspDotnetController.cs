using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using X1BackendProject.ComposerWriter;
using X1BackendProject.Models;

namespace X1BackendProject.DockerfileWriter.LangHandlers
{
    public class AspDotnetController : BaseController
    {
        public enum FrontTechs
        {
            ReactJs,
            AngularJs
        }

        private string _projectName;

        public AspDotnetController(string appName, string sourceDirPath, string projectName, string outputDllName,
            int portNumber, string postgresUsername, string postgresPassword, string postgresDatabase,
            FrontTechs[] frontTechs, DatabaseConfig[] databases) : base(appName, sourceDirPath)
        {
            _projectName = projectName;

            List<string> projects = new List<string>();

            foreach (var dir in Directory.EnumerateDirectories(AppPath))
            {
                if (Directory.EnumerateFiles(dir).Any(f => f.EndsWith(".csproj")))
                {
                    projects.Add(Path.GetFileName(dir));
                }
            }

            File.WriteAllLines(
                $"{AppPath}/Dockerfile",
                new string[]
                {
                    "FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as base",
                    $"WORKDIR /",
                    "EXPOSE 80",
                    "FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env",
                    $"WORKDIR /",
                    $"COPY *.sln .",
                });

            foreach (var project in projects)
            {
                var doc = new XmlDocument();
                doc.Load($"{AppPath}/{project}/{project}.csproj");
                var node = doc.CreateNode(XmlNodeType.Element, "PublishWithAspNetCoreTargetManifest", null);
                node.InnerText = "false";
                doc.DocumentElement.GetElementsByTagName("PropertyGroup")[0].AppendChild(node);
                doc.Save($"{AppPath}/{project}/{project}.csproj");
                File.AppendAllLines(
                    $"{AppPath}/Dockerfile",
                    new string[]
                    {
                        $"COPY /{project}/*.csproj ./{project}/"
                    });
            }

            File.AppendAllLines(
                $"{AppPath}/Dockerfile",
                new string[]
                {
                    "RUN dotnet restore",
                    $"COPY . .",
                });

            foreach (var frontTech in frontTechs)
            {
                if (frontTech == FrontTechs.ReactJs)
                {
                    File.AppendAllLines(
                        $"{AppPath}/Dockerfile",
                        new string[]
                        {
                            $"WORKDIR /{projectName}/ClientApp",
                            "RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -",
                            "RUN apt-get install -y nodejs",
                            "RUN npm install npm@latest -g",
                            "RUN npm config set '@bit:registry' https://node.bit.dev",
                            "RUN npm install",
                            $"WORKDIR /"
                        });
                }
                else if (frontTech == FrontTechs.AngularJs)
                {
                    File.AppendAllLines(
                        $"{AppPath}/Dockerfile",
                        new string[]
                        {
                            $"WORKDIR {projectName}/ClientApp",
                            "RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -",
                            "RUN apt-get install -y nodejs",
                            "RUN npm install npm@latest -g",
                            "RUN npm config set '@bit:registry' https://node.bit.dev",
                            "RUN npm install",
                            $"WORKDIR /"
                        });
                }
            }

            File.AppendAllLines(
                $"{AppPath}/Dockerfile",
                new string[]
                {
                    $"RUN dotnet publish -c Release {projectName}",
                    "WORKDIR /",
                    "FROM base AS final",
                    $"COPY --from=build-env /{projectName}/bin/Release/netcoreapp2.2/publish .",
                    $"ENTRYPOINT [\"dotnet\", \"{projectName}.dll\"]"
                });

            new MainComWriter(databases.ToList(), appName, AppPath, portNumber);

            Console.WriteLine("running docker-compose...");
            foreach (var res in $"cd {AppPath}/{projectName}; docker-compose up --no-start;".Bash())
            {
                Console.Write(res);
            }
        }

        public IEnumerable<char> RunWebsite()
        {
            return $"cd {AppPath}; docker-compose up".Bash();
        }
    }
}