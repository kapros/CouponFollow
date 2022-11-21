using CouponFollow.TestTask.Framework;
using CouponFollow.TestTask.PageObjects.DomainObjects;
using Microsoft.Playwright;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects
{
    public class MainPage : BasePage
    {
        public const string MAIN_PAGE_URL = "https://couponfollow.com/";
        private readonly ILocator _trendingCouponsLocator;
        private readonly ILocator _searchBoxLocator;
        private readonly ILocator _openSearchBoxLocator;

        public MainPage(IPage page) : base(page)
        {
            _trendingCouponsLocator = Page.Locator("css=article.trending-mobile,article.trending-offer");
            _searchBoxLocator = Page.Locator("css=form input.mobile-search-input,input.search-field");
            _openSearchBoxLocator = Page.Locator("css=#openSearch,input.search-field");
        }

        public Task Visit()
        {
            return Page.GotoAsync(MAIN_PAGE_URL);
        }

        public Task<int> GetTrendingCoupons()
            => _trendingCouponsLocator.CountAsync();

        public Task<IReadOnlyCollection<TopDealCoupon>> GetTopDealCoupons()
            => TopDealCoupon.Get(Page);

        public async Task<IReadOnlyCollection<SearchResultElement>> SearchFor(string search)
        {
            await (await _openSearchBoxLocator.AsEnumerableAsync().FirstAwaitAsync(async x => await x.IsVisibleAsync())).Nth(0).ClickAsync();
            var searchBox = (await _searchBoxLocator.AsEnumerableAsync().FirstAwaitAsync(async x => await x.IsVisibleAsync())).Nth(0);
            await searchBox.ClickAsync();
            await searchBox.TypeAsync(search, new LocatorTypeOptions { Delay = 100 });
            var results = await SearchResultElement.Get(Page);
            return results;
        }

        public Task<IReadOnlyCollection<StaffPickCouponElement>> GetStaffPickCoupons()
        {
            return StaffPickCouponElement.Get(Page);
        }
    }
}
