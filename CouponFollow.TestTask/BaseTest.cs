using CouponFollow.TestTask.Framework;
using CouponFollow.TestTask.PageObjects;
using FluentAssert;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace CouponFollow.TestTask
{
    public class BaseTest
    {
        protected IPlaywright Playwright;
        protected IBrowser BrowserContext;
        protected IPage Page;
        protected readonly string Browser;
        protected readonly string DeviceName;
        protected BrowserNewContextOptions Device;

        protected BaseTest(string browser, string device)
        {
            Browser = browser;
            DeviceName = device;
        }

        [OneTimeSetUp]
        public async Task BeforeAllTests()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Device = Playwright.Devices[DeviceName];
            BrowserContext = await Playwright[Browser].LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        }

        [SetUp]
        public async Task BeforeEachTest()
        {
            Page = await (await BrowserContext.NewContextAsync(Device)).NewPageAsync();
        }

        [TearDown]
        public async Task AfterEachTest()
        {
            await Page.CloseAsync();
            Page = null;
        }

        [OneTimeTearDown]
        public async Task AfterAllTests()
        {
            await BrowserContext.CloseAsync();
            Playwright.Dispose();
        }
    }
}