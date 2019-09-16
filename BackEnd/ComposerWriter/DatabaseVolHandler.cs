using System.Collections.Generic;
using System.IO;
using BackEnd.Models.Modules;

namespace BackEnd.ComposerWriter
{
    public class DatabaseVolHandler
    {
        public DatabaseVolHandler(List<DatabaseConfig> databases, string appName, string AppPath)
        {
            File.AppendAllLines(
                $"{AppPath}/docker-compose.yml",
                new string[]
                {
                    "volumes:"
                });

            foreach (var database in databases)
            {
                File.AppendAllLines(
                    $"{AppPath}/docker-compose.yml",
                    new string[]
                    {
                        database.DatabaseProvider == Databases.PostgreSql ? $"  postgres_db_volume:" :
                        database.DatabaseProvider == Databases.SqlServer ? $"  sqlserver_db_volume:" :
                        database.DatabaseProvider == Databases.MySql ? $"  mysql_db_volume:" :
                        database.DatabaseProvider == Databases.MongoDb ? $"  mongodb_db_volume:" :
                        ""
                    });
            }
        }
    }
}