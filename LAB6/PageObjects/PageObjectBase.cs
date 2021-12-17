using Microsoft.Playwright;

namespace LAB6.PageObjects
{
    public abstract class PageObjectBase
    {
        protected readonly IPage _page;

        public PageObjectBase(IPage page)
        {
            _page = page;
        }
    }
}
