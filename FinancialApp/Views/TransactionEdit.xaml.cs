namespace FinancialApp.Views;

public partial class TransactionEdit : ContentPage
{
    public TransactionEdit()
    {
        InitializeComponent();
    }

    private async void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        try
        {
            // Attempt to navigate back
            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            // Log the exception and show an error message to the user
            Console.WriteLine($"Exception occurred: {ex.Message}");
            await DisplayAlert("Error", "An unexpected error occurred while navigating back.", "OK");
        }
    }
}