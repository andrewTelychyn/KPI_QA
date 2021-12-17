using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace LAB55
{
    public class PlaywrightTest
    {
        private IPlaywright _playwright;
        private IBrowser _browser;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                //Headless = false,
                //SlowMo = 1000
            });
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            _playwright.Dispose();
            await _browser.DisposeAsync();
        }

        [Test]
        public async Task SignUp_Successful()
        {
            //arrange
            string startingPageUrl = "https://www.demoblaze.com/index.html";
            string seed = Guid.NewGuid().ToString().Substring(0, 5);
            string login = $"andrew.telychyn.{seed}";
            string password = "andrew.telychyn";

            //act 
            var page = await _browser.NewPageAsync();
            await page.GotoAsync(startingPageUrl);
            page.Dialog += page_Dialog1_EventHandler;
            await page.ClickAsync("#signin2");

            await page.FillAsync("#sign-username", login);
            await page.FillAsync("#sign-password", password);

            await page.ClickAsync("#signInModal > div > div > div.modal-footer > button.btn.btn-primary");

            //assert
            async void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialog.Message.Should().Be("Sign up successful.");
                await dialog.AcceptAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            page.Url.Should().Be(startingPageUrl);

            await page.WaitForSelectorAsync("#signInModal", new PageWaitForSelectorOptions { State = WaitForSelectorState.Hidden });
            var signInForm = await page.QuerySelectorAsync("#signInModal");
            var signInFormAriaHidden = await signInForm.GetAttributeAsync("aria-hidden");
            signInFormAriaHidden.Should().Be("true");
        }

        [Test]
        public async Task SignUp_Failed()
        {
            //arrange
            string startingPageUrl = "https://www.demoblaze.com/index.html";
            string login = "andrew.telychyn";
            string password = "andrew.telychyn";

            //act 
            var page = await _browser.NewPageAsync();
            await page.GotoAsync(startingPageUrl);
            page.Dialog += page_Dialog1_EventHandler;

            await page.ClickAsync("#signin2");

            await page.FillAsync("#sign-username", login);
            await page.FillAsync("#sign-password", password);

            await page.ClickAsync("#signInModal > div > div > div.modal-footer > button.btn.btn-primary");

            //assert
            async void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialog.Message.Should().Be("This user already exist.");
                await dialog.AcceptAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            page.Url.Should().Be(startingPageUrl);

            await page.WaitForSelectorAsync("#signInModal", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            var signInForm = await page.QuerySelectorAsync("#signInModal");
            var signInFormStyle = await signInForm.GetAttributeAsync("style");
            signInFormStyle.Should().Contain("display: block");
        }

        [Test]
        public async Task LogIn_Successful()
        {
            //arrange
            string startingPageUrl = "https://www.demoblaze.com/index.html";
            string login = "andrew.telychyn";
            string password = "andrew.telychyn";

            //act 
            var page = await _browser.NewPageAsync();
            await page.GotoAsync(startingPageUrl);
            await page.ClickAsync("#login2");

            await page.FillAsync("#loginusername", login);
            await page.FillAsync("#loginpassword", password);

            await page.ClickAsync("#logInModal > div > div > div.modal-footer > button.btn.btn-primary");

            await page.WaitForSelectorAsync("#nameofuser", new PageWaitForSelectorOptions() { State = WaitForSelectorState.Visible });

            //assert
            string username = await page.TextContentAsync("#nameofuser");
            username.Should().Contain(login);
            page.Url.Should().Be(startingPageUrl);

            await page.WaitForSelectorAsync("#logInModal", new PageWaitForSelectorOptions { State = WaitForSelectorState.Hidden });
            var loginForm = await page.QuerySelectorAsync("#logInModal");
            var loginFormAriaHidden = await loginForm.GetAttributeAsync("aria-hidden");
            loginFormAriaHidden.Should().Be("true");
        }

        [Test]
        public async Task LogIn_Failed_UserDoesntExist()
        {
            //arrange
            string startingPageUrl = "https://www.demoblaze.com/index.html";
            string seed = Guid.NewGuid().ToString();
            string login = seed.Substring(0, 5);
            string password = seed.Substring(5, 5);

            //act 
            var page = await _browser.NewPageAsync();
            await page.GotoAsync(startingPageUrl);
            page.Dialog += page_Dialog1_EventHandler;

            await page.ClickAsync("#login2");

            await page.FillAsync("#loginusername", login);
            await page.FillAsync("#loginpassword", password);

            await page.ClickAsync("#logInModal > div > div > div.modal-footer > button.btn.btn-primary");

            //assert
            async void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialog.Message.Should().Be("User does not exist.");
                await dialog.AcceptAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            page.Url.Should().Be(startingPageUrl);

            await page.WaitForSelectorAsync("#logInModal", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            var loginForm = await page.QuerySelectorAsync("#logInModal");
            var loginFormStyle = await loginForm.GetAttributeAsync("style");
            loginFormStyle.Should().Contain("display: block");
        }

        [Test]
        public async Task LogIn_Failed_WrongPassword()
        {
            //arrange
            string startingPageUrl = "https://www.demoblaze.com/index.html";
            string seed = Guid.NewGuid().ToString();
            string login = "andrew.telychyn";
            string password = seed.Substring(0, 5);

            //act 
            var page = await _browser.NewPageAsync();
            await page.GotoAsync(startingPageUrl);
            page.Dialog += page_Dialog1_EventHandler;

            await page.ClickAsync("#login2");

            await page.FillAsync("#loginusername", login);
            await page.FillAsync("#loginpassword", password);

            await page.ClickAsync("#logInModal > div > div > div.modal-footer > button.btn.btn-primary");

            //assert
            async void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialog.Message.Should().Be("Wrong password.");
                await dialog.AcceptAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

            page.Url.Should().Be(startingPageUrl);

            await page.WaitForSelectorAsync("#logInModal", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            var loginForm = await page.QuerySelectorAsync("#logInModal");
            var loginFormStyle = await loginForm.GetAttributeAsync("style");
            loginFormStyle.Should().Contain("display: block");
        }

        [Test]
        public async Task MakePurchase_Successful()
        {
            //arrange
            string startingPageUrl = "https://www.demoblaze.com/index.html";
            string cartUrl = "https://www.demoblaze.com/cart.html";
            string name = "Andrew Telychyn";
            string country = "Ukraine";
            string city = "Kyiv";
            string creditCard = "1234567890";
            string month = "11";
            string year = "2021";
            string successMessage = "Thank you for your purchase!";

            //act 
            var page = await _browser.NewPageAsync();
            await page.GotoAsync(startingPageUrl);
            await page.ClickAsync("#cartur");

            //assertion
            page.Url.Should().Be(cartUrl);

            await page.ClickAsync("#page-wrapper > div > div.col-lg-1 > button");

            await page.FillAsync("#name", name);
            await page.FillAsync("#country", country);
            await page.FillAsync("#city", city);
            await page.FillAsync("#card", creditCard);
            await page.FillAsync("#month", month);
            await page.FillAsync("#year", year);
            await page.ClickAsync("#orderModal > div > div > div.modal-footer > button.btn.btn-primary");

            await page.WaitForSelectorAsync("body > div.sweet-alert.showSweetAlert.visible");

            //assertion
            var message = await page.TextContentAsync("body > div.sweet-alert.showSweetAlert.visible > h2");
            message.Should().Be(successMessage);

            await page.ClickAsync("body > div.sweet-alert.showSweetAlert.visible > div.sa-button-container > div > button");

            //assertion
            page.Url.Should().Be(startingPageUrl);
        }

        [Test]
        public async Task AddToCart_Successful()
        {
            //arrange
            string startingPageUrl = "https://www.demoblaze.com/index.html";
            string productUrl = "https://www.demoblaze.com/prod.html?idp_=1";
            string cartUrl = "https://www.demoblaze.com/cart.html";

            //act 
            var page = await _browser.NewPageAsync();
            await page.GotoAsync(startingPageUrl);
            page.Dialog += page_Dialog1_EventHandler;

            await page.ClickAsync("//*[@id=\"tbodyid\"]/div[1]/div/div/h4/a");

            //assertion
            page.Url.Should().Be(productUrl);

            await page.ClickAsync("#tbodyid > div.row > div > a");

            await page.ClickAsync("#navbarExample > ul > li:nth-child(4) > a");

            //assertion
            page.Url.Should().Be(cartUrl);

            await page.WaitForSelectorAsync("#tbodyid > tr.success");

            //assertion
            var tableRows = await page.QuerySelectorAllAsync("#tbodyid > tr");
            tableRows.Count.Should().Be(1);

            //assertion
            async void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialog.Message.Should().Be("Product added");
                await dialog.AcceptAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }
        }
    }
}