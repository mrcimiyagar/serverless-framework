using BackEnd.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Pending> Pendings { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseNpgsql("Host=localhost;" +
                           "Database=X1Db;" +
                           "Username=postgres;" +
                           "Password=3g5h165tsK65j1s564L69ka5R168kk37sut5ls3Sk2t;" +
                           "Timeout=0;" +
                           "Command Timeout=0;");
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}