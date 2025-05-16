using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Contact.Data;

public class ContactDataContext : DbContext
{
    public ContactDataContext(DbContextOptions<ContactDataContext> options) : base(options)
    {
    }

    public DbSet<Models.Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure all model configurations that implements interface IEntityTypeConfiguration<T> from Assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}