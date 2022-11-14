using Microsoft.Playwright;
using System.Collections.Generic;

namespace CouponFollow.TestTask.Framework
{
    public static class Extensions
    {
        public static IEnumerable<ILocator> AsEnumerable(this ILocator locator)
        {
            for (int i = 0; i < locator.CountAsync().Result; i++)
            {
                yield return locator.Locator($"nth={i}");
            }
        }
    }
}
