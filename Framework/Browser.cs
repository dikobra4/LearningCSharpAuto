using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Framework
{
    public class Browser
    {
        private IWebDriver driver;
        public Uri baseUrl;

        public Browser(Uri baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public Browser Start()
        {   
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximised");

                ChromeDriverService service = ChromeDriverService.CreateDefaultService();

                driver = new ChromeDriver(service, options);
                return this;
            }
            catch (Exception ex)
            {
                // TODO add logging of catched errors
                throw new WebDriverException($"Failed to init driver: {ex}");
            }
        }

        public void Quit()
        {
            if (driver != null)
            {
                try
                {
                    driver.Quit();
                }
                catch (WebDriverException ex)
                {
                    // TODO add logging of catched errors
                }
                finally
                {
                    driver.Dispose();
                }
            }
        }

        // Browser Navigation

        public Uri CreateLinkFromBaseUrl(string path)
        {
            UriBuilder builder = new(this.baseUrl)
            {
                Path = path
            };
            Uri url = builder.Uri;
            return url;
        }

        public void GoToUrl(Uri url)
        {
            this.driver.Navigate().GoToUrl(url);
        }

        public void GoBack()
        {
            this.driver.Navigate().Back();
        }

        public void GoForward()
        {
            this.driver.Navigate().Forward();
        }

        public void Refresh()
        {
            this.driver.Navigate().Refresh();
        }

        //Page information
        public string GetCurrentUrl()
        {
            return this.driver.Url;
        }

        public string GetTitle()
        {
            return this.driver.Title;
        }

        // Cookie and localstorage
        public void AddCookie(Dictionary<string, string> cookies)
        {
            foreach (KeyValuePair<string, string> cookie in cookies)
            {
                this.driver.Manage().Cookies.AddCookie(new Cookie(cookie.Key, cookie.Value));
            }
        }

        // Service
        internal WebDriverWait GetWaitObject(int waitTimeInSeconds)
        {
            return new WebDriverWait(this.driver, TimeSpan.FromSeconds(waitTimeInSeconds));
        }
        internal IWebDriver GetDriver()
        {
            return this.driver;
        }
    }
}
