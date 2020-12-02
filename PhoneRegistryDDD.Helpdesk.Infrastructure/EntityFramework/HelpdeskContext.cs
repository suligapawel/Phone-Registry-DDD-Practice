using Microsoft.EntityFrameworkCore;
using PhoneRegistryDDD.Helpdesk.Core.Entities;

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework
{
    public class HelpdeskContext : DbContext
    {
        public HelpdeskContext(DbContextOptions<HelpdeskContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HelpdeskContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
