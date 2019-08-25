using System.Collections.Generic;
using System.IO;
using X1BackendProject.Models;

namespace X1BackendProject.ComposerWriter
{
    public class DatabaseEnvHandler
    {
        public DatabaseEnvHandler(List<DatabaseConfig> databases, string appName, string AppPath)
        {
            if (databases.Count > 0)
            {
                File.AppendAllLines(
                    $"{AppPath}/docker-compose.yml",
                    new string[]
                    {
                        "    environment:"
                    });
                foreach (var database in databases)
                {
                    File.AppendAllLines(
                        $"{AppPath}/docker-compose.yml",
                        new string[]
                        {
                            database.DatabaseProvider == Databases.MySql
                                ? $"      MYSQL_HOST: {appName}_mysql_image"
                                : database.DatabaseProvider == Databases.PostgreSql
                                    ? $"      POSTGRES_HOST: {appName}_postgres_image"
                                    : database.DatabaseProvider == Databases.SqlServer
                                        ? $"      SQLSERVER_HOST: {appName}_sqlserver_image"
                                        : database.DatabaseProvider == Databases.MongoDb
                                            ? $"      MONGODB_HOST: {appName}_mongodb_image"
                                            : ""
                        });
                }
            }
        }
    }
}