using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QMPT.Data.ModelRestrictions;
using QMPT.Data.Services;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Data.Models
{
    public class KeyValuePair : BaseModel
    {
        [Required]
        [MaxLength(KeyValuePairRestrictions.keyMaxLength)]
        public string Key { get; set; }
        [Required]
        [MaxLength(KeyValuePairRestrictions.valueMaxLength)]
        public string Value { get; set; }

        public static void OnModelCreating(EntityTypeBuilder<KeyValuePair> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(kvp => kvp.Key).IsUnique();
        }

        public static void CreateNewKeyValuePair(string key, string value)
        {
            var keyValuePair = new KeyValuePair
            {
                Key = key,
                Value = value
            };

            var keyValuePairsService = new KeyValuePairsService();
            keyValuePairsService.Insert(keyValuePair);
        }
    }
}
