using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackEnd.Models.Modules;

namespace BackEnd.ComposerWriter
{
    public class MainComWriter
    {
        public MainComWriter(List<DatabaseConfig> databases, string AppName, string AppPath, int portNumber)
        {
            File.WriteAllLines(
                $"{AppPath}/docker-compose.yml",
                new string[]
                {
                    "version: \"3.4\"",
                    "",
                    "networks:",
                    $"  {AppName}:",
                    "    driver: bridge",
                    "",
                    "services:"
                });

            new DatabaseHandler(databases, AppName, AppPath);

            File.AppendAllLines(
                $"{AppPath}/docker-compose.yml",
                new string[]
                {
                    $"  {AppName}:",
                    $"    container_name: {AppName}",
                    $"    image: {AppName}:latest"
                });

            new DatabaseDepHandler(databases.ToList(), AppName, AppPath);

            File.AppendAllLines(
                $"{AppPath}/docker-compose.yml",
                new string[]
                {
                    "    build:",
                    "      context: .",
                    "      dockerfile: Dockerfile",
                });

            if (portNumber >= 0)
            {
                File.AppendAllLines(
                    $"{AppPath}/docker-compose.yml",
                    new string[]
                    {
                        "    ports:",
                        $"      - \"8080:{portNumber}\""
                    });
            }

            new DatabaseEnvHandler(databases.ToList(), AppName, AppPath);

            File.AppendAllLines(
                $"{AppPath}/docker-compose.yml",
                new string[]
                {
                    "    networks:",
                    $"      - {AppName}"
                });

            new DatabaseVolHandler(databases.ToList(), AppName, AppPath);
        }
    }
}