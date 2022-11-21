using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponFollow.TestTask.Framework
{
    public static class Extensions
    {
        public static async IAsyncEnumerable<ILocator> AsEnumerableAsync(this ILocator locator)
        {
            var total = await locator.CountAsync();
            for (int i = 0; i < total; i++)
            {
                 yield return locator.Locator($"nth={i}");
            }
        }

        public static async Task<int> CountAsync<T>(this IEnumerable<T> source, Func<T, Task<bool>> predicate)
        {
            var met = 0;
            foreach (var element in source)
            {
                if (await predicate(element))
                    met++;
            }
            return met;
        }
    }
}
