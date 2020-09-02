using Microsoft.EntityFrameworkCore;
using Npgsql;
using QMPT.Data.Models;
using QMPT.Data.Models.Devices;
using QMPT.Data.Models.Organizations;
using QMPT.Data.Models.Organizations.ContactPersons;

namespace QMPT.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<ContactPersonPhoneNumber> ContactPersonPhoneNumbers { get; set; }
        public DbSet<ContactPersonEmail> ContactPersonEmails { get; set; }
        public DbSet<ContactPerson> ContactPeople { get; set; }
        public DbSet<OrganizationNote> OrganizationNotes { get; set; }
        public DbSet<OrganizationFile> OrganizationFiles { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<KeyValuePair> KeyValuePairs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;" +
                "Database=QMPT;Username=postgres;Password=1111");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Price.PriceType>();

            KeyValuePair.OnModelCreating(modelBuilder.Entity<KeyValuePair>());
            RefreshToken.OnModelCreating(modelBuilder.Entity<RefreshToken>());
            User.OnModelCreating(modelBuilder.Entity<User>());
            Organization.OnModelCreating(modelBuilder.Entity<Organization>());
            OrganizationFile.OnModelCreating(modelBuilder.Entity<OrganizationFile>());
            OrganizationNote.OnModelCreating(modelBuilder.Entity<OrganizationNote>());
            ContactPerson.OnModelCreating(modelBuilder.Entity<ContactPerson>());
            ContactPersonEmail.OnModelCreating(modelBuilder.Entity<ContactPersonEmail>());
            ContactPersonPhoneNumber.OnModelCreating(modelBuilder.Entity<ContactPersonPhoneNumber>());
            Price.OnModelCreating(modelBuilder.Entity<Price>());
            Device.OnModelCreating(modelBuilder.Entity<Device>());
            Accessory.OnModelCreating(modelBuilder.Entity<Accessory>());
        }

        static DatabaseContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Price.PriceType>();
        }
    }
}
