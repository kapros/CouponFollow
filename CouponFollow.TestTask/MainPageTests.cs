using CouponFollow.TestTask.Framework;
using CouponFollow.TestTask.PageObjects;
using FluentAssert;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace CouponFollow.TestTask
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture("Chromium", "Galaxy S8")]
    [TestFixture("Webkit", "iPhone 13")]
    [TestFixture("Chromium", "Desktop Chrome")]
    public class MainPageTests : BaseTest
    {
        private MainPage _mainPage;
        public MainPageTests(string browser, string device) : base(browser, device)
        {
        }
        [SetUp]
        public async Task Setup()
        {
            var mainPage = new MainPage(Page);
            await mainPage.Visit();
            _mainPage = mainPage;
        }

        [Test]
        public async Task Should_show_3_Top_Deals_from_3_6_or_9()
        {
            var topDealCoupons = await _mainPage.GetTopDealCoupons();
            topDealCoupons.Count.ShouldBeGreaterThanOrEqualTo(3);
            (topDealCoupons.Count % 3).ShouldBeEqualTo(0);
            (await topDealCoupons.CountAsync(async x => await x.IsInView())).ShouldBeEqualTo(3);
        }

        [Test]
        public async Task Should_have_at_least_30_Trending_Coupons()
        {
            const int min = 30;
            var trendingCoupons = await _mainPage.GetTrendingCoupons();
            trendingCoupons.ShouldBeGreaterThan(min - 1);
        }

        [Test]
        public async Task Should_have_unique_Staff_Picks()
        {
            var staffPicks = await _mainPage.GetStaffPickCoupons();
            var count = staffPicks.Count;
            var details = staffPicks.Select(async x => await x.GetCoupon()).ToList();
            details.Distinct().Count().ShouldBeEqualTo(count);
        }

        [Test]
        public async Task Should_take_to_stores_website()
        {
            const string searchPhrase = Data.SEARCH_PHRASE;
            const string expectedStoreWebsite = Data.EXPECTED_STORE_WEBSITE;
            const string expectedStoreDestinationUrl = Data.NIKE_TARGET_PAGE;
            var expectedPopupUrl = Data.ExpectedPopupUrl;
            var searchResults = await _mainPage.SearchFor(searchPhrase);
            var firstResult = searchResults.First();
            var storeName = await firstResult.GetStoreName();
            var storeWebsite = await firstResult.GetStoreWebsite();
            var storePage = await firstResult.EnterStore();
            var firstCoupon = (await storePage.GetAllCoupons()).First();
            var (externalPage, popup) = await firstCoupon.FollowCoupon();
            storeName.ShouldContain(searchPhrase);
            storeWebsite.ShouldBeEqualTo(expectedStoreWebsite);
            popup.GetUrl().ShouldStartWith(expectedPopupUrl);
            externalPage.GetUrl().ShouldStartWith(expectedStoreDestinationUrl);
        }
    }
}