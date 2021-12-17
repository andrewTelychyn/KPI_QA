using Microsoft.Playwright;
using System.Threading.Tasks;

namespace LAB6.PageObjects
{
    public class MainPageLogined : PageObjectBase
    {
        public readonly string UsernameSelector = "#nameofuser";

        private readonly ILocator _welcomingSign;

        public MainPageLogined(IPage page) : base(page)
        {
            _welcomingSign = page.Locator(UsernameSelector);
        }

        public async Task<string> GetUsernameAsync()
        {
            return await _welcomingSign.TextContentAsync();
        }
    }
}
