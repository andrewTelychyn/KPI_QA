using Microsoft.Playwright;
using System.Threading.Tasks;

namespace LAB6.PageObjects
{
    public class CartPage : PageObjectBase
    {
        public const string URL = "https://www.demoblaze.com/cart.html";
        public const string Selector = "#orderModal";
        public const string AlertSelector = ".sweet-alert"; // body > div.sweet-alert.showSweetAlert.visible
        public static readonly string AlertMessageSelector = $"{AlertSelector} > h2"; // body > div.sweet-alert.showSweetAlert.visible

        public const string SuccessPurchaseMessage = "Thank you for your purchase!";

        private readonly ILocator _orderForm;
        private readonly ILocator _placeOrderButton;
        private readonly ILocator _nameField;
        private readonly ILocator _countryField;
        private readonly ILocator _cityField;
        private readonly ILocator _creditCardField;
        private readonly ILocator _monthField;
        private readonly ILocator _yearField;
        private readonly ILocator _orderButton;

        public CartPage(IPage page) : base(page)
        {
            _orderForm = page.Locator(Selector);
            _placeOrderButton = page.Locator("#page-wrapper button"); // "#page-wrapper > div > div.col-lg-1 > button"
            _nameField = page.Locator("#name");
            _countryField = page.Locator("#country");
            _cityField = page.Locator("#city");
            _creditCardField = page.Locator("#card");
            _monthField = page.Locator("#month");
            _yearField = page.Locator("#year");
            _orderButton = _orderForm.Locator("button.btn.btn-primary");
        }

        public async Task PlaceOrderAsync()
        {
            await _placeOrderButton.ClickAsync();
        }

        public async Task FillNameAsync(string name)
        {
            await _nameField.FillAsync(name);
        }

        public async Task FillCountryAsync(string country)
        {
            await _countryField.FillAsync(country);
        }

        public async Task FillCityAsync(string city)
        {
            await _cityField.FillAsync(city);
        }

        public async Task FillCreditCardAsync(string card)
        {
            await _creditCardField.FillAsync(card);
        }

        public async Task FillMonthAsync(string month)
        {
            await _monthField.FillAsync(month);
        }

        public async Task FillYearAsync(string year)
        {
            await _yearField.FillAsync(year);
        }

        public async Task MakeOrderAsync()
        {
            await _orderButton.ClickAsync();
        }

        public async Task<string> GetAlertTextAsync()
        {
            var alert = _page.Locator(AlertSelector);
            var alertMessage = await alert.Locator("h2").TextContentAsync();

            return alertMessage;
        }

        public async Task ClickAlertOkButtonAsync()
        {
            var alert = _page.Locator(AlertSelector);

            await alert.Locator("button.confirm").ClickAsync();
        }
    }
}
