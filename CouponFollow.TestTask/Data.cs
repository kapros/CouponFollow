using NUnit.Framework;

namespace CouponFollow.TestTask
{
    public class Data
    {
        public const string NIKE_TARGET_PAGE = "https://www.nike.com/";
        public const string SEARCH_PHRASE = "Nike";
        public const string EXPECTED_STORE_WEBSITE = "nike.com";
        public static readonly string BaseUrl = TestContext.Parameters.Get(nameof(BaseUrl), "https://couponfollow.com/");
        public static string ExpectedPopupUrl => $"{BaseUrl}site/nike.com";
    }
}
