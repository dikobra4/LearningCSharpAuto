namespace Framework
{
    public class Button : Text
    {
        public Button(Browser browser, string by_method, string locator, string elementName) : 
            base(browser, by_method, locator, elementName)
        {
        }

        public void Click()
        {
            this.Webelement.Click();
        }
    }
}
