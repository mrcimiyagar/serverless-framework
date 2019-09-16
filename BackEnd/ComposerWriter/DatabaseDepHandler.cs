using System.Collections.Generic;
using System.IO;
using BackEnd.Models.Modules;

namespace BackEnd.ComposerWriter
{
    public class DatabaseDepHandler
    {
        public DatabaseDepHandler(List<DatabaseConfig> databases, string appName, string appPath)
        {
            if (databases.Count > 0)
            {
                File.AppendAllLines(
                    $"{appPath}/docker-compose.yml",
                    new string[]
                    {
                        "    depends_on:"
                    });
            }

            foreach (var database in databases)
            {
                File.AppendAllLines(
                    $"{appPath}/docker-compose.yml",
                    new string[]
                    {
                        database.DatabaseProvider == Databases.PostgreSql ? $"      - \"{appName}_postgres_image\"" :
                        database.DatabaseProvider == Databases.SqlServer ? $"      - \"{appName}_sqlserver_image\"" :
                        database.DatabaseProvider == Databases.MySql ? $"      - \"{appName}_mysql_image\"" :
                        database.DatabaseProvider == Databases.MongoDb ? $"      - \"{appName}_mongodb_image\"" :
                        ""
                    });
            }
        }
    }
}