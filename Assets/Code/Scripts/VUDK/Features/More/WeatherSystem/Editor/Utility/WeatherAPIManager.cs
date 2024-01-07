namespace VUDK.Features.More.WeatherSystem.Editor.Utility
{
    using VUDK.Features.More.APISystem.Data;

    public static class WeatherAPIManager
    {
        private readonly static string[] DefaultAPIS = { "5222d4ba51mshbb69ed710081f0bp162b01jsn12d158513a9e" };

        public static string[] APIS = DefaultAPIS;

        public static void SetAPIs(APIPackageData apis)
        {
            APIS = apis.APIS;
        }
    }
}