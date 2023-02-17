namespace Framework
{
    public abstract class BasePage
    {
        private readonly Browser browser;
        private readonly string slug;

        public BasePage(Browser browser, string slug)
        {
            this.browser = browser;
            this.slug = slug;
        }

        public BasePage Go()
        {
            Uri url = this.browser.CreateLinkFromBaseUrl('/' + this.slug);
            this.browser.GoToUrl(url);
            return this;
        }

        public string IsAt()
        {
            return this.slug;
        }
    }
}