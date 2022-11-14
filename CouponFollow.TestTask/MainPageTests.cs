using CouponFollow.TestTask.PageObjects;
using FluentAssert;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace CouponFollow.TestTask
{
    [NonParallelizable]
    [TestFixture("Chromium", "Galaxy S8")]
    [TestFixture("Webkit", "iPhone 13")]
    [TestFixture("Chromium", "Desktop Chrome")]
    public class MainPageTests
    {
        private IPlaywright _playwright;
        private IBrowser _browserContext;
        private IPage _page;
        private MainPage _mainPage;
        private readonly string _browser;
        private readonly string _deviceName;
        private BrowserNewContextOptions _device;

        public MainPageTests(string browser, string device)
        {
            _browser = browser;
            _deviceName = device;
        }

        [OneTimeSetUp]
        public async Task BeforeAllTests()
        {
            _playwright = await Playwright.CreateAsync();
            _device = _playwright.Devices[_deviceName];
            _browserContext = await _playwright[_browser].LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        }

        [SetUp]
        public async Task BeforeEachTest()
        {
            _page = await (await _browserContext.NewContextAsync(_device)).NewPageAsync();
            var mainPage = new MainPage(_page);
            await mainPage.Visit();
            _mainPage = mainPage;
        }

        [TearDown]
        public async Task AfterEachTest()
        {
            await _page.CloseAsync();
            _page = null;
        }

        [OneTimeTearDown]
        public async Task AfterAllTests()
        {
            await _browserContext.CloseAsync();
            _playwright.Dispose();
        }

        [Test]
        public void Should_show_3_Top_Deals_from_3_6_or_9()
        {
            var topDealCoupons = _mainPage.GetTopDealCoupons();
            topDealCoupons.Count.ShouldBeGreaterThanOrEqualTo(3);
            (topDealCoupons.Count % 3).ShouldBeEqualTo(0);
            topDealCoupons.Count(x => x.IsInView().Result).ShouldBeEqualTo(3);
        }

        [Test]
        public async Task Should_have_at_least_30_Trending_Coupons()
        {
            const int min = 30;
            var trendingCoupons = await _mainPage.GetTrendingCoupons();
            trendingCoupons.ShouldBeGreaterThan(min - 1);
        }

        [Test]
        public void Should_have_unique_Staff_Picks()
        {
            var staffPicks = _mainPage.GetStaffPickCoupons();
            var count = staffPicks.Count;
            var details = staffPicks.Select(x => x.GetCoupon().Result).ToList();
            details.Distinct().Count().ShouldBeEqualTo(count);
        }

        [Test]
        public async Task Should_take_to_stores_website()
        {
            const string searchPhrase = "Nike";
            const string expectedStoreWebsite = "nike.com";
            const string expectedPopupUrl = "https://couponfollow.com/site/nike.com";
            const string expectedStoreDestinationUrl = "https://www.nike.com/";
            var searchResults = await _mainPage.SearchFor(searchPhrase);
            var firstResult = searchResults.First();
            var storeName = await firstResult.GetStoreName();
            var storeWebsite = await firstResult.GetStoreWebsite();
            var storePage = await firstResult.EnterStore();
            var firstCoupon = storePage.GetAllCoupons().First();
            var (externalPage, popup) = await firstCoupon.FollowCoupon();
            storeName.ShouldContain(searchPhrase);
            storeWebsite.ShouldBeEqualTo(expectedStoreWebsite);
            popup.GetUrl().ShouldStartWith(expectedPopupUrl);
            externalPage.GetUrl().ShouldStartWith(expectedStoreDestinationUrl);
        }
    }
}