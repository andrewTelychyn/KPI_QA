using Microsoft.Playwright;
using System.Threading.Tasks;

namespace LAB6.PageObjects
{
    class MainPage : PageObjectBase
    {
        public const string URL = "https://www.demoblaze.com/index.html";

        public const string CartButtonSelector = "#cartur";

        private readonly ILocator _signUpButton;
        private readonly ILocator _loginButton;
        private readonly ILocator _cartButton;

        public MainPage(IPage page) : base(page)
        {
            _signUpButton = page.Locator("#signin2");
            _loginButton = page.Locator("#login2");
            _cartButton = page.Locator(CartButtonSelector);
        }

        public async Task GotoAsync()
        {
            await _page.GotoAsync(URL);
        }

        public async Task OpenSignUpFormAsync()
        {
            await _signUpButton.ClickAsync();
        }

        public async Task OpenLoginFormAsync()
        {
            await _loginButton.ClickAsync();
        }

        public async Task OpenCartAsync()
        {
            await _cartButton.ClickAsync();
        }
    }
}
