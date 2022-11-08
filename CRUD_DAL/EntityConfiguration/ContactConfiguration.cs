using CRUD_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CRUD_DAL.EntityConfiguration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts")
                .HasKey(k => k.Id);

            builder.Property(p => p.Name)
                .IsRequired().HasMaxLength(40);

            builder.Property(p => p.JobTitle)
                .IsRequired().HasMaxLength(50);

            builder.Property(p => p.MobilePhone)
                .IsRequired().HasMaxLength(25);

            builder.Property(p => p.BirthDate)
                .HasColumnType("date").IsRequired();
        }
    }
}
