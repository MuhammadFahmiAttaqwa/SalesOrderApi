using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalesOrderApi.Entity;
using System.Data;

namespace SalesOrderApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<SoOrder> SoOrder { get; set; }

        public DbSet<SoItem> SoItem { get; set; }

        public DbSet<ComCustomer> ComCustomer { get; set; }

    }
}
