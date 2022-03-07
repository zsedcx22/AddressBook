using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TesterProject.Authorization.Roles;
using TesterProject.Authorization.Users;
using TesterProject.MultiTenancy;
using TesterProject.ContactManagement.Entities;
using TesterProject.CarManagement.Entities;
using System.Reflection;

namespace TesterProject.EntityFrameworkCore
{
    public class TesterProjectDbContext : AbpZeroDbContext<Tenant, Role, User, TesterProjectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<AddressAndContact> AddressAndContacts { get; set; }

        public TesterProjectDbContext(DbContextOptions<TesterProjectDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            

            /*modelBuilder.Entity<AddressAndContact>().HasKey(sc => new { sc.AddressID, sc.ContactID });

            modelBuilder.Entity<AddressAndContact>()
                .HasOne<Contact>(ac => ac.Contact)
                .WithMany(c => c.AddressAndContacts)
                .HasForeignKey(ac => ac.ContactID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AddressAndContact>()
                .HasOne<Address>(ac => ac.Address)
                .WithMany(a => a.AddressAndContacts)
                .HasForeignKey(ac => ac.AddressID)
                .OnDelete(DeleteBehavior.NoAction);*/


        }
    }
}
