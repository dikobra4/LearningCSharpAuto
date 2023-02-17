using System.Text;
using Framework;
using Pages;

namespace Tests
{
    [TestFixture]
    public class Tests : BaseTestUI
    {
        public readonly string bookName = "Speaking JavaScript";

        [SetUp]
        public void GoToMainPageSetUp()
        {
            MainPage mainPage = new(browser);
            mainPage.Go();
        }

        [Test]
        public void Test01ListLoad()
        {
            MainPage mainPage = new(browser);
               
            Assert.That(mainPage.bookCardTitle.Count(), Is.EqualTo(8));
        }

        [Test]
        public void Test02SearchExsistByFullName()
        {
            string bookName = "Speaking JavaScript";
            MainPage mainPage = new(browser);
            
            mainPage.searchField.TypeText(bookName);
            
            Assert.Multiple(() =>
            {
                Assert.That(mainPage.bookCardTitle.Count(), Is.EqualTo(1));
                Assert.That(mainPage.bookCardTitle.GetText(), Is.EqualTo(bookName));
            });
        }

        [Test]
        public void Test03SearchNotExsist() 
        {
            string bookName = "Some incorrect name of book";
            MainPage mainPage = new(browser);

            mainPage.searchField.TypeText(bookName);

        }

        [Test]
        public void Test04SearchExsistByPartName()
        {
            string bookName = "Speaking JavaScript";
            string partOfBookName = bookName.Substring(0, 9);
            MainPage mainPage = new(browser);

            mainPage.searchField.TypeText(partOfBookName);

            Assert.Multiple(() =>
            {
                Assert.That(mainPage.bookCardTitle.Count(), Is.EqualTo(1));
                Assert.That(mainPage.bookCardTitle.GetText(), Is.EqualTo(bookName));
            });
        }

        public void Test05GoToBookPage()
        {
            MainPage mainPage = new(browser);

            mainPage.searchField.TypeText(this.bookName);
            mainPage.bookCardTitle.Click();
            Uri expectedUrl = this.browser.CreateLinkFromBaseUrl("/books?book=9781449365035");
            Assert.That(this.browser.GetCurrentUrl(), Is.EqualTo(expectedUrl.ToString()));
        }
    }
}