
namespace QMPT.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public static string UserIdJwtKey { get { return "uid"; } }
    }
}
