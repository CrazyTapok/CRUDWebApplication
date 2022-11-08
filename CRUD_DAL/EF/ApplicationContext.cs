using CRUD_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace CRUD_DAL.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;
   
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationContext)));

            Contact contact_1 = new Contact { Id = Guid.NewGuid(), Name = "Dima", JobTitle = "A1", MobilePhone = "+375(33)2345678", BirthDate = DateTime.Now};
            Contact contact_2 = new Contact { Id = Guid.NewGuid(), Name = "Roma", JobTitle = "Global", MobilePhone = "+375(44)2345678", BirthDate = DateTime.Now };
            Contact contact_3 = new Contact { Id = Guid.NewGuid(), Name = "Pasha", JobTitle = "MTC", MobilePhone = "+375(29)2345678", BirthDate = DateTime.Now };

            modelBuilder.Entity<Contact>().HasData(new Contact[] { contact_1, contact_2, contact_3 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
