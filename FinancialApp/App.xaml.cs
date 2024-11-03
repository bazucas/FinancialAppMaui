using FinancialApp.Views;

namespace FinancialApp;

public partial class App
{
    public App(TransactionList listPage)
    {
        InitializeComponent();
        MainPage = new NavigationPage(listPage);
    }
}