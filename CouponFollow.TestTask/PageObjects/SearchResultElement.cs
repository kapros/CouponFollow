using CouponFollow.TestTask.Framework;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.PageObjects
{
    public class SearchResultElement
    {
        private readonly ILocator _locator;

        private SearchResultElement(ILocator locator)
        {
            _locator = locator;
        }

        public async Task<string> GetStoreName() => await _locator.Locator("css=p.name").InnerTextAsync();
        public async Task<string> GetStoreWebsite() => await _locator.Locator("css=p.domain").InnerTextAsync();

        public async Task<StorePage> EnterStore()
        {
            await _locator.ClickAsync();
            return new StorePage(_locator.Page);
        }

        public static IReadOnlyCollection<SearchResultElement> Get(IPage page)
        {
            var locator = page.Locator("css=a.mobile-suggestion-item,a.suggestion-item");
            try
            {
                locator.Nth(0).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 }).Wait();
                var list = locator.AsEnumerable().Select(x => new SearchResultElement(x)).ToList().AsReadOnly();
                return list;
            }
            catch (AggregateException ex) when (ex.InnerExceptions.Any(x => x is TimeoutException))
            {
                return Enumerable.Empty<SearchResultElement>().ToList().AsReadOnly();
            }
        }
    }
}
