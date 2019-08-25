
using System;
using Microsoft.EntityFrameworkCore;
using DbContext = SharedArea.DbContext;

namespace TestCsharpSolution
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbName = "TicketServerDb"; 
            var username = "keyhan"; 
            var password = "3g5h165tsK65j1s564L69ka5R168kk37sut5ls3Sk2t";
            var connstring = $"Server={Environment.GetEnvironmentVariable("MYSQL_HOST")}; Database={dbName}; UID={username}; Password=\"{password}\";";
            optionsBuilder.UseMySQL(connstring);
        }
    }
}