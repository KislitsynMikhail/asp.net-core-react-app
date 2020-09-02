using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelHelpers.RemovingModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.Models.Devices
{
    public class Accessory : BaseModel, IRemovable
    {
        public string Name { get; set; }

        [ForeignKey(nameof(Remover))]
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }

        public User Remover { get; set; }

        public static void OnModelCreating(EntityTypeBuilder<Accessory> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(a => a.Remover)
                .WithMany(u => u.RemovedAccessories)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
