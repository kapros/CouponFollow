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

        public Task<string> GetStoreName() => _locator.Locator("css=p.name").InnerTextAsync();
        public Task<string> GetStoreWebsite() => _locator.Locator("css=p.domain").InnerTextAsync();

        public async Task<StorePage> EnterStore()
        {
            await _locator.ClickAsync();
            return new StorePage(_locator.Page);
        }

        public async static Task<IReadOnlyCollection<SearchResultElement>> Get(IPage page)
        {
            var locator = page.Locator("css=a.mobile-suggestion-item,a.suggestion-item");
            try
            {
                locator.Nth(0).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 }).Wait();
                var list = (await locator.AsEnumerableAsync().Select(x => new SearchResultElement(x)).ToListAsync()).AsReadOnly();
                return list;
            }
            catch (AggregateException ex) when (ex.InnerExceptions.Any(x => x is TimeoutException))
            {
                return Enumerable.Empty<SearchResultElement>().ToList().AsReadOnly();
            }
        }
    }
}
