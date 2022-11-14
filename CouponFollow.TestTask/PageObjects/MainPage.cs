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

        public async Task Visit()
        {
            await Page.GotoAsync(MAIN_PAGE_URL);
        }

        public IReadOnlyCollection<TopDealCoupon> GetTopDealCoupons()
            => TopDealCoupon.Get(Page);

        public async Task<int> GetTrendingCoupons()
            => await _trendingCouponsLocator.CountAsync();

        public async Task<IReadOnlyCollection<SearchResultElement>> SearchFor(string search)
        {
            await _openSearchBoxLocator.AsEnumerable().First(x => x.IsVisibleAsync().Result).Nth(0).ClickAsync();
            var searchBox = _searchBoxLocator.AsEnumerable().First(x => x.IsVisibleAsync().Result).Nth(0);
            await searchBox.ClickAsync();
            await searchBox.TypeAsync(search, new LocatorTypeOptions { Delay = 100 });
            var results = SearchResultElement.Get(Page);
            return await Task.FromResult(results);
        }

        public IReadOnlyCollection<StaffPickCouponElement> GetStaffPickCoupons()
        {
            return StaffPickCouponElement.Get(Page);
        }
    }
}
