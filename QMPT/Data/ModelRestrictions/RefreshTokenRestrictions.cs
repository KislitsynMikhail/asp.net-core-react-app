
namespace QMPT.Data.ModelRestrictions
{
    public static class RefreshTokenRestrictions
    {
        public const int tokenMaxLength = 200;
        public const string tokenSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const int tokenExpireDays = 30;
    }
}
