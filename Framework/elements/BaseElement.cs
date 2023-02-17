using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace Framework
{
    public abstract class BaseElement
    {
        public string elementName;
        protected readonly string locator;
        private readonly Browser browser;
        private readonly WebDriverWait wait;
        private ReadOnlyCollection<IWebElement>? _webelements;
        private readonly By by;
        private int elementIndex = 0;

        protected BaseElement(Browser browser, string by_method, string locator, string elementName)
        {
            this.browser = browser;
            this.wait = this.browser.GetWaitObject(10);
            this.locator = locator;
            this.by = this.ChooseSearchMethod(by_method);
            this.elementName = elementName;

        }        

        internal IWebElement Webelement
        {
            get 
            {
                try
                {
                    _webelements = this.wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(this.by));
                    if (_webelements == null)
                    {
                        throw new NoSuchElementException($"Unable to find element {this.elementName} with localor {this.locator}");
                    }
                    return _webelements[elementIndex];
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException(
                        $"You are trying to access an element at index {this.elementIndex}, " +
                        $"but the maximum element index in the found array is {this._webelements.Count - 1}");
                }
                catch (TimeoutException)
                {
                    throw new TimeoutException($"Failed to locate element '{this.elementName}' with locator '{this.locator}'");
                }
            }
        }

        public void SetIndex(int element_index)
        {
            this.elementIndex = element_index;
        }

        public int Count()
        {
            _ = this.Webelement;
            int? elementsCount = this._webelements.Count;
            if (elementsCount == null)
            {
                return 0;
            }
            else
            {
                return (int)elementsCount;
            }
        }

        public bool IsLocated()
        {
            if (this.browser.GetDriver().FindElements(this.by).Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void WaitUntilNotLocated(int timeInSeconds)
        {
            try
            {
                IWebElement element = this.Webelement;
                this.browser.GetWaitObject(timeInSeconds).Until(ExpectedConditions.StalenessOf(element));
                throw new TimeoutException($"Failed to wait for element {this.elementName} to disappear (locator: {this.locator})");
            }
            catch (TimeoutException) { }
            catch (StaleElementReferenceException) { }
        }

        private By ChooseSearchMethod(string by_method)
        {
            return by_method.ToLower() switch
            {
                "css" => By.CssSelector(this.locator),
                "xpath" => By.XPath(this.locator),
                "id" => By.Id(this.locator),
                "name" => By.Name(this.locator),
                "linktext" => By.LinkText(this.locator),
                "classname" => By.ClassName(this.locator),
                _ => By.CssSelector(this.locator),
            };
        }
    }
}
