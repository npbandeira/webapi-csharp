using Microsoft.EntityFrameworkCore;
using webapi.models;

namespace webapi.infra
{
    public class ConnectionContext : DbContext
    {
        // Configuration Database
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Database=webapi;Uid=root;Pwd=;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}