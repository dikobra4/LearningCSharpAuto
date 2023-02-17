namespace Framework
{
    public class Text : Element
    {
        public Text(Browser browser, string by_method, string locator, string elementName) : 
            base(browser, by_method, locator, elementName)
        {
        }

        public string GetText() 
        {
            return this.Webelement.Text;
        }
    }
}
