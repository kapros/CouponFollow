using CouponFollow.TestTask.Framework;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects.DomainObjects
{
    public class StoreCoupon
    {
        private readonly ILocator _locator;

        private StoreCoupon(ILocator locator)
        {
            _locator = locator;
        }

        public async Task<(ExternalPage FollowedPage, PopupPage Popup)> FollowCoupon()
        {
            var locator = _locator.Locator("css=a");
            var page = _locator.Page;
            var prevPage = page.Url;
            var newPage = await page.RunAndWaitForPopupAsync(async () => await locator.ClickAsync());
            newPage.Close += NewPage_Close;
            return (new ExternalPage(page, prevPage), new PopupPage(newPage));
        }

        private void NewPage_Close(object sender, IPage e)
        {
            _locator.Page.BringToFrontAsync();
        }

        public static IReadOnlyCollection<StoreCoupon> Get(IPage page)
        {
            var locator = page.Locator("css=div.deal,article.type-deal");
            try
            {
                locator.Nth(0).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 }).Wait();
                var list = locator.AsEnumerable().Select(x => new StoreCoupon(x)).ToList().AsReadOnly();
                return list;
            }
            catch (AggregateException ex) when (ex.InnerExceptions.Any(x => x is TimeoutException))
            {
                return Enumerable.Empty<StoreCoupon>().ToList().AsReadOnly();
            }
        }
    }
}
