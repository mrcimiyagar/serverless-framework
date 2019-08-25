
using Microsoft.EntityFrameworkCore;
using DbContext = SharedArea.DbContext;

namespace TestCsharpSolution
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var password = "3g5h165tsK65j1s564L69ka5R168kk37sut5ls3Sk2t";
            var connstring = $"Server=db;Database=master;User=sa;Password={password};";
            optionsBuilder.UseSqlServer(connstring);
        }
    }
}