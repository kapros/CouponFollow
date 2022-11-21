using CouponFollow.TestTask.Framework;
using Microsoft.Playwright;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects.DomainObjects
{
    public class TopDealCoupon
    {
        private readonly ILocator _locator;

        private TopDealCoupon(ILocator locator)
        {
            _locator = locator;
        }

        public async Task<bool> IsInView()
        {
            var boundingBox = await _locator.BoundingBoxAsync();
            return boundingBox.X > 0;
        }

        public async static Task<IReadOnlyCollection<TopDealCoupon>> Get(IPage page)
        {
            var locator = page.Locator("css=div.top-deal.swiper-slide:not(.swiper-slide-duplicate)");
            var list = (await locator.AsEnumerableAsync().Select(x => new TopDealCoupon(x)).ToListAsync()).AsReadOnly();
            return list;
        }
    }
}
