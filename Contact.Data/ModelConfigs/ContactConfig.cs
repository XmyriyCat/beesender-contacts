using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Data.ModelConfigs;

public class ContactConfig : IEntityTypeConfiguration<Models.Contact>
{
    public void Configure(EntityTypeBuilder<Models.Contact> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(x => x.JobTitle)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(x => x.BirthDate)
            .IsRequired();
    }
}