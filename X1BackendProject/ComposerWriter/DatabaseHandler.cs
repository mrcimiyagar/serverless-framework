using System.Collections.Generic;
using System.IO;
using X1BackendProject.Models;

namespace X1BackendProject.ComposerWriter
{
    public class DatabaseHandler
    {
        public DatabaseHandler(IEnumerable<DatabaseConfig> databases, string AppName, string AppPath)
        {
            foreach (var database in databases)
            {
                if (database.DatabaseProvider == Databases.PostgreSql)
                {
                    File.AppendAllLines(
                        $"{AppPath}/docker-compose.yml",
                        new string[]
                        {
                            "",
                            $"  {AppName}_postgres_image:",
                            $"    container_name: {AppName}_postgres_image",
                            "    image: postgres:11",
                            "    ports:",
                            "      - 5432",
                            "    restart: always",
                            "    volumes:",
                            "      - postgres_db_volume:/var/lib/postgresql/data",
                            "    environment:",
                            $"      POSTGRES_USER: \"{database.Username}\"",
                            $"      POSTGRES_PASSWORD: \"{database.Password}\"",
                            $"      POSTGRES_DB: \"{database.Database}\"",
                            "    networks:",
                            $"      - {AppName}"
                        });
                }
                else if (database.DatabaseProvider == Databases.MySql)
                {
                    File.AppendAllLines(
                        $"{AppPath}/docker-compose.yml",
                        new string[]
                        {
                            "",
                            $"  {AppName}_mysql_image:",
                            $"    container_name: {AppName}_mysql_image",
                            "    image: mysql",
                            "    command: mysqld --default-authentication-plugin=mysql_native_password",
                            "    environment:",
                            $"      MYSQL_ROOT_PASSWORD: {database.Password}",
                            "    volumes:",
                            "      - mysql_db_volume:/var/lib/mysql",
                            "      - ./_MySQL_Init_Script:/docker-entrypoint-initdb.d",
                            "    restart: always",
                            "    networks:",
                            $"      - {AppName}"
                        });
                }
                else if (database.DatabaseProvider == Databases.SqlServer)
                {
                    File.AppendAllLines(
                        $"{AppPath}/docker-compose.yml",
                        new string[]
                        {
                            "",
                            $"  {AppName}_sqlserver_image:",
                            $"    container_name: {AppName}_sqlserver_image",
                            "    image: \"mcr.microsoft.com/mssql/server\"",
                            "    environment:",
                            $"      SA_PASSWORD: \"{database.Password}\"",
                            "      ACCEPT_EULA: \"Y\"",
                            "    restart: always",
                            "    networks:",
                            $"      - {AppName}"
                        });
                }
                else if (database.DatabaseProvider == Databases.MongoDb)
                {
                    File.AppendAllLines(
                        $"{AppPath}/docker-compose.yml",
                        new string[]
                        {
                            $"  {AppName}_mongodb_image:",
                            $"    container_name: {AppName}_mongodb_image",
                             "    image: mongo",
                             "    environment:",
                            $"      MONGO_INITDB_ROOT_USERNAME: {database.Username}",
                            $"      MONGO_INITDB_ROOT_PASSWORD: {database.Password}",
                             "    ports:",
                             "      - 27017:27017",
                             "    restart: always",
                             "    networks:",
                            $"      - {AppName}"
                        });
                }
            }
        }
    }
}