using Microsoft.Playwright;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects
{
    public class PopupPage
    {
        private readonly IPage _page;

        public PopupPage(IPage page)
        {
            _page = page;
        }

        public Task<string> GetTitle()
        {
            return _page.TitleAsync();
        }

        public string GetUrl()
        {
            return _page.Url;
        }

        public Task ClosePage()
        {
            return _page.CloseAsync();
        }
    }
}