using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.ModelRestrictions;
using QMPT.Data.Models.Devices;
using QMPT.Data.Models.Organizations;
using QMPT.Data.Models.Organizations.ContactPersons;
using QMPT.Models.Requests.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Data.Models
{
    public class User : BaseModel, IRemovable
    {
        [Required]
        [MaxLength(UserRestrictions.loginMaxLength)]
        public string Login { get; set; }

        [Required]
        [MaxLength(UserRestrictions.firstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(UserRestrictions.lastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(UserRestrictions.passwordMaxLength)]
        public string Password { get; set; }

        #region Removable
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }
        public User Remover { get; set; }
        #endregion Removable

        #region Organizations
        public ICollection<Organization> CreatedOrganizations { get; set; }
        public ICollection<Organization> EditedOrganizations { get; set; }
        public ICollection<Organization> RemovedOrganizations { get; set; }
        #endregion Organizations
        #region OrganizationFiles
        public ICollection<OrganizationFile> CreatedOrganizationFiles { get; set; }
        public ICollection<OrganizationFile> EditedOrganizationFiles { get; set; }
        public ICollection<OrganizationFile> RemovedOrganizationFiles { get; set; }
        #endregion OrganizationFiles
        #region OrganizationNotes
        public ICollection<OrganizationNote> CreatedOrganizationNotes { get; set; }
        public ICollection<OrganizationNote> EditedOrganizationNotes { get; set; }
        public ICollection<OrganizationNote> RemovedOrganizationNotes { get; set; }
        #endregion OrganizationNotes
        #region ContactPersons
        public ICollection<ContactPerson> CreatedContactPeople { get; set; }
        public ICollection<ContactPerson> EditedContactPeople { get; set; }
        public ICollection<ContactPerson> RemovedContactPeople { get; set; }
        #endregion ContactPersons
        #region ContactPersonEmails
        public ICollection<ContactPersonEmail> CreatedContactPersonEmails { get; set; }
        public ICollection<ContactPersonEmail> EditedContactPersonEmails { get; set; }
        public ICollection<ContactPersonEmail> RemovedContactPersonEmails { get; set; }
        #endregion ContactPersonEmails
        #region ContactPersonPhoneNumbers
        public ICollection<ContactPersonPhoneNumber> CreatedContactPersonPhoneNumbers { get; set; }
        public ICollection<ContactPersonPhoneNumber> EditedContactPersonPhoneNumbers { get; set; }
        public ICollection<ContactPersonPhoneNumber> RemovedContactPersonPhoneNumbers { get; set; }
        #endregion ContactPersonPhoneNumbers
        #region Prices
        public ICollection<Price> CreatedPrices { get; set; }
        public ICollection<Price> EditedPrices { get; set; }
        public ICollection<Price> RemovedPrices { get; set; }
        #endregion Prices
        #region Devices
        public ICollection<Device> CreatedDevices { get; set; }
        public ICollection<Device> EditedDevices { get; set; }
        public ICollection<Device> RemovedDevices { get; set; }
        #endregion Devices
        #region Accessories
        public DbSet<Accessory> RemovedAccessories { get; set; }
        #endregion
        public ICollection<User> RemovedUsers { get; set; }

        public static void OnModelCreating(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(u => u.Login).IsUnique();

            entityTypeBuilder
                .HasOne(u => u.Remover)
                .WithMany(u => u.RemovedUsers)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public User() { }

        public User(UserRequest userRequest)
        {
            Login = userRequest.Login;
            Password = userRequest.Password;
            FirstName = userRequest.FirstName;
            LastName = userRequest.LastName;
        }
    }
}
