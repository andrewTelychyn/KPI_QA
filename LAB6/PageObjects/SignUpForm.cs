using Microsoft.Playwright;
using System.Threading.Tasks;

namespace LAB6.PageObjects
{
    public class SignUpForm : PageObjectBase
    {
        public const string Selector = "#signInModal";

        public const string SuccessfulSignUpMessage = "Sign up successful.";
        public const string UserExistsSignUpMessage = "This user already exist.";

        private readonly ILocator _signInForm;
        private readonly ILocator _loginField;
        private readonly ILocator _passwordField;
        private readonly ILocator _signInButton;

        public SignUpForm(IPage page) : base(page)
        {
            _signInForm = page.Locator(Selector);
            _loginField = page.Locator("#sign-username");
            _passwordField = page.Locator("#sign-password");
            _signInButton = _signInForm.Locator("button.btn.btn-primary");

        }

        public async Task FillLoginAsync(string login)
        {
            await _loginField.FillAsync(login);
        }

        public async Task FillPasswordAsync(string password)
        {
            await _passwordField.FillAsync(password);
        }

        public async Task SignUpAsync()
        {
            await _signInButton.ClickAsync();
        }

        public async Task<bool> IsVisibleAsync()
        {
            return await _signInForm.IsVisibleAsync();

            /*
            var signInForm = await page.QuerySelectorAsync("#signInModal");
            var signInFormAriaHidden = await signInForm.GetAttributeAsync("aria-hidden");
            signInFormAriaHidden.Should().Be("true");
            */
        }
    }
}
