using NUnit.Framework;
using System.Text;

namespace Framework
{
    [TestFixture]   
    public class BaseTestUI
    {
        protected Browser browser;
        private StringBuilder verificationErrors;
        protected Uri baseURL;

        [SetUp]
        public void DriverInit()
        {
            this.baseURL = new Uri("https://demoqa.com");
            this.browser = new Browser(baseURL).Start();
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void DriverTearDown()
        {
            this.browser.Quit();
            Assert.That(verificationErrors.Length, Is.EqualTo(0));
        }
    }
}
