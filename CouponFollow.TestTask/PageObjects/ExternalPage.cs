using Microsoft.Playwright;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects
{
    public class ExternalPage : BasePage
    {
        private readonly string _previousPage;

        public ExternalPage(IPage page, string previousPage) : base(page)
        {
            _previousPage = previousPage;
        }

        public async Task GoBack() => await Page.GoBackAsync();

        public async Task VisitPreviousPage() => await Page.GotoAsync(_previousPage);
    }
}