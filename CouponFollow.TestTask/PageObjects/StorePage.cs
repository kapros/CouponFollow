using CouponFollow.TestTask.PageObjects.DomainObjects;
using Microsoft.Playwright;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects
{
    public class StorePage : BasePage
    {
        public StorePage(IPage page) : base(page)
        {
        }

        public Task<IReadOnlyCollection<StoreCoupon>> GetAllCoupons()
        {
            var results = StoreCoupon.Get(Page);
            return results;
        }
    }
}