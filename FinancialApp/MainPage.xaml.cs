namespace FinancialApp;

public partial class MainPage
{
    private int _count;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _count++;

        CounterBtn.Text = $"Clicked {_count} time(s)";

        SemanticScreenReader.Announce($"Clicked {_count} time(s)");
    }
}