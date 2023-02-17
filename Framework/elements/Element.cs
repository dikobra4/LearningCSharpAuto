namespace Framework
{
    public class Element : BaseElement
    {
        public Element(Browser browser, string by_method, string locator, string elementName) : 
            base(browser, by_method, locator, elementName)
        {
        }

        public string GetAttribute(string attributeName)
        {
            return this.Webelement.GetAttribute(attributeName);
        }

    }
}
