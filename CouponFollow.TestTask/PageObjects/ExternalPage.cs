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

        public Task GoBack() => Page.GoBackAsync();

        public Task VisitPreviousPage() => Page.GotoAsync(_previousPage);
    }
}