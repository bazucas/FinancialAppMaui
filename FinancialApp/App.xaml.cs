using FinancialApp.Views;

namespace FinancialApp;

public partial class App
{
    public App()
    {
        InitializeComponent();

        //MainPage = new AppShell();
        MainPage = new TransactionList();
    }
}