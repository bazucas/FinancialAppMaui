namespace FinancialApp.Views;

public partial class TransactionList
{
    public TransactionList()
    {
        InitializeComponent();
    }

    private void OnButtonClicked_To_TransactionAdd(object? sender, EventArgs e)
    {
        if (Application.Current is not null)
        {
            Application.Current.MainPage = new TransactionAdd();
        }
    }

    private void OnButtonClicked_To_TransactionEdit(object? sender, EventArgs e)
    {
        if (Application.Current is not null)
        {
            Application.Current.MainPage = new TransactionEdit();
        }
    }
}