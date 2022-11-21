using CouponFollow.TestTask.Framework;
using CouponFollow.TestTask.PageObjects.DomainObjects;
using Microsoft.Playwright;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects
{
    public class StaffPickCouponElement
    {
        private readonly ILocator _locator;

        private StaffPickCouponElement(ILocator locator)
        {
            _locator = locator;
        }

        public Task<string> GetMerchantName() => _locator.Locator("css=.merch").TextContentAsync();

        public Task<Deal> GetDeal() => Task.FromResult((Deal)_locator.Locator("css=p.title").TextContentAsync());

        public async Task<StaffPickCoupon> GetCoupon()
        {
            return new StaffPickCoupon
            {
                MerchantName = await GetMerchantName(),
                Deal = await GetDeal()
            };
        }

        public static async Task<IReadOnlyCollection<StaffPickCouponElement>> Get(IPage page)
        {
            var locator = page.Locator("css=div.staff-pick");
            var list = (await locator.AsEnumerableAsync().Select(x => new StaffPickCouponElement(x)).ToListAsync()).AsReadOnly();
            return list;
        }
    }
}
