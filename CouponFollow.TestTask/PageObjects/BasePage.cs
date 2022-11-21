using Microsoft.Playwright;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects
{
    public class BasePage
    {
        protected readonly IPage Page;

        public BasePage(IPage page)
        {
            Page = page;
        }

        public string GetUrl() => Page.Url;

        public Task<string> GetTitle()
        {
            return Page.TitleAsync();
        }
    }
}
