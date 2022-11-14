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

        public async Task<string> GetTitle()
        {
            return await _page.TitleAsync();
        }

        public string GetUrl()
        {
            return _page.Url;
        }

        public async Task ClosePage()
        {
            await _page.CloseAsync();
        }
    }
}