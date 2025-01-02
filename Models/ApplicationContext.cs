using Microsoft.EntityFrameworkCore;
using TrainingProjectAPI.Models.DB;

namespace TrainingProjectAPI.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Item> Items { get; set; }
    }
}
