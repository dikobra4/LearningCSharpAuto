namespace Framework
{
    public class Input : Element
    {
        public Input(Browser browser, string by_method, string locator, string elementName) : base(browser, by_method, locator, elementName)
        {
        }

        public void TypeText(string text)
        {
            this.Webelement.SendKeys(text);
        }
    }
}
