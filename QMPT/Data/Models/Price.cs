using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelHelpers.EditingModel;
using QMPT.Data.ModelHelpers.RemovingModel;
using QMPT.Data.ModelRestrictions;
using QMPT.Models.Requests.Prices;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMPT.Data.Models
{
    public class Price : BaseModel, IEditable<Price>, IRemovable, ICloneable
    {
        [Required]
        [MaxLength(PriceRestrictions.nameMaxLength)]
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public PriceType Type { get; set; }

        #region Editable
        public int? OriginalId { get; set; }
        public Price Original { get; set; }
        public int? EditorId { get; set; }
        public DateTime? EditingTime { get; set; }
        public bool IsEdited { get; set; }
        public int Version { get; set; }
        public bool IsRelevant { get; set; }

        public User Editor { get; set; }

        public string CurrentVersionKey => $"{nameof(Price)}_{nameof(CurrentVersionKey)}_{OriginalId}";

        public void CreateNewVersionValue()
        {
            KeyValuePair.CreateNewKeyValuePair(CurrentVersionKey, "1");
        }
        #endregion Editable

        #region Removable
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }
        public User Remover { get; set; }
        #endregion Removable

        public enum PriceType
        {
            Repair,
            Delivery
        }

        public static void OnModelCreating(EntityTypeBuilder<Price> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(p => p.Creator)
                .WithMany(u => u.CreatedPrices)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(p => p.Editor)
                .WithMany(u => u.EditedPrices)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
                .HasOne(p => p.Remover)
                .WithMany(u => u.RemovedPrices)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public Price() { }

        public Price(PriceRequest priceRequest, int creatorId) : base(creatorId)
        {
            ModelEditingHandler.OnCreate(this);
            CopyData(priceRequest);
        }

        public Price(Price price) : base(price.CreatorId)
        {
            Name = price.Name;
            Type = price.Type;
            Cost = price.Cost;

            ModelEditingHandler.OnCopy(this, price);
            ModelRemovingHandler.Copy(this, price);
        }

        public void ChangeData(object newData, int editorId, int version)
        {
            CopyData(newData as PriceRequest);

            ModelEditingHandler.OnEdit(this, editorId, version);
        }

        public object Clone()
        {
            return new Price(this);
        }

        private void CopyData(PriceRequest priceRequest)
        {
            Cost = priceRequest.Cost;
            Type = priceRequest.Type;
            Name = priceRequest.Name;
        }
    }
}
