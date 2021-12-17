using Microsoft.Playwright;
using System.Threading.Tasks;

namespace LAB6.PageObjects
{
    public class LoginForm : PageObjectBase
    {
        public const string Selector = "#logInModal";

        public const string UserDoesntExistLoginMessage = "User does not exist.";
        public const string WrongPasswordLoginMessage = "Wrong password.";


        private readonly ILocator _loginForm;
        private readonly ILocator _loginField;
        private readonly ILocator _passwordField;
        private readonly ILocator _loginButton;

        public LoginForm(IPage page) : base(page)
        {
            _loginForm = page.Locator(Selector);
            _loginField = page.Locator("#loginusername");
            _passwordField = page.Locator("#loginpassword");
            _loginButton = _loginForm.Locator("button.btn.btn-primary");
        }

        public async Task FillLoginAsync(string login)
        {
            await _loginField.FillAsync(login);
        }

        public async Task FillPasswordAsync(string password)
        {
            await _passwordField.FillAsync(password);
        }

        public async Task LoginAsync()
        {
            await _loginButton.ClickAsync();
        }

        public async Task<bool> IsVisibleAsync()
        {
            return await _loginForm.IsVisibleAsync();

            /*
            var signInForm = await page.QuerySelectorAsync("#signInModal");
            var signInFormAriaHidden = await signInForm.GetAttributeAsync("aria-hidden");
            signInFormAriaHidden.Should().Be("true");
            */
        }
    }
}
