using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Security.Cryptography;
using QMPT.Data.Services;
using QMPT.Data.ModelRestrictions;

namespace QMPT.Data.Models
{
    public class RefreshToken : BaseModel
    {
        [MaxLength(RefreshTokenRestrictions.tokenMaxLength)]
        public string Token { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public DateTime ExpirationTime { get; set; }

        public User User { get; set; }

        public static void OnModelCreating(EntityTypeBuilder<RefreshToken> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(rt => rt.Token).IsUnique();
        }

        public RefreshToken() { }

        public RefreshToken(User user)
        {
            UserId = user.Id;
            RefreshData();
        }

        public void RefreshData()
        {
            Token = GetUniqueToken();
            ExpirationTime = DateTime.UtcNow.AddDays(RefreshTokenRestrictions.tokenExpireDays);
        }

        private static string GetUniqueToken()
        {
            var tokenLength = RefreshTokenRestrictions.tokenMaxLength;
            var tokenSymbols = RefreshTokenRestrictions.tokenSymbols;

            var tokenBuilder = new StringBuilder(tokenLength);
            var indexes = new byte[tokenLength];

            using var rnd = RandomNumberGenerator.Create();
            var refreshTokensService = new RefreshTokensService();
            string token = null;

            do
            {
                rnd.GetBytes(indexes);
                for (var i = 0; i < tokenLength; i++)
                    tokenBuilder.Append(tokenSymbols[
                        indexes[i] % tokenSymbols.Length]);

                token = tokenBuilder.ToString();
                tokenBuilder.Clear();

            } while (refreshTokensService.IsExists(token));

            return token;
        }
    }
}
