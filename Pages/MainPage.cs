using Framework;

namespace Pages
{
    public class MainPage: BasePage
    {
        public Button loginButton;
        public Input searchField;
        public Button searchButton;
        public Button bookCardTitle;

        public MainPage(Browser browser) : base(browser, "books")
        {
            this.loginButton = new Button(browser, "ID", "login", "Login button");
            this.searchField = new Input(browser, "ID", "searchBox", "Search field");
            this.searchButton = new Button(browser, "CSS", ".input-group-append", "Search button");
            this.bookCardTitle = new Button(browser, "CSS", ".rt-tr-group a", "Book card title");
        }
    }
}