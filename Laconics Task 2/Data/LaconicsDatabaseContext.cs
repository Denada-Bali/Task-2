using LaconicsCrm.webapi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LaconicsCrm.webapi.Data
{
    public class LaconicsDatabaseContext:DbContext
    {
        public LaconicsDatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
