using CommunityToolkit.Mvvm.Messaging;
using FinancialApp.Models;
using FinancialApp.Repositories;

namespace FinancialApp.Views;

public partial class TransactionList : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITransactionRepository _repository;

    public TransactionList(IServiceProvider serviceProvider, ITransactionRepository repository)
    {
        _serviceProvider = serviceProvider;
        _repository = repository;
        InitializeComponent();
        Reload();
        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) => Reload());
    }

    private void Reload()
    {
        var items = _repository.GetAll();
        CollectionView.ItemsSource = items;

        var income = items.Where(a => a.Type == Models.TransactionType.Income).Sum(a => a.Value);
        var expenses = items.Where(a => a.Type == Models.TransactionType.Expenses).Sum(a => a.Value);
        var balance = income - expenses;

        LabelIncome.Text = income.ToString("C");
        LabelExpenses.Text = expenses.ToString("C");
        LabelBalance.Text = balance.ToString("C");
    }

    private async void OnButtonClicked_To_TransactionAdd(object? sender, EventArgs e)
    {
        try
        {
            var addPage = _serviceProvider.GetService<TransactionAdd>();
            if (addPage != null)
            {
                await Navigation.PushModalAsync(addPage);
            }
            else
            {
                // Error handling: show an alert if the page could not be created
                await DisplayAlert("Error", "Failed to create the add page", "OK");
            }
        }
        catch (Exception ex)
        {
            // Log the exception and show an error message to the user
            Console.WriteLine($"Exception occurred: {ex.Message}");
            await DisplayAlert("Error", "An unexpected error occurred while navigating to the add page.", "OK");
        }
    }

    private async void TapGestureRecognizer_OnTapped_ToTransactionEdit(object? sender, TappedEventArgs e)
    {
        try
        {
            var editPage = _serviceProvider.GetService<TransactionEdit>();

            if (editPage != null)
            {
                var grid = (Grid)sender;
                var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
                var transaction = (Transaction)gesture.CommandParameter;

                editPage.SetTransactionToEdit(transaction);
                await Navigation.PushModalAsync(editPage);
            }
            else
            {
                // Error handling: show an alert if the page could not be created
                await DisplayAlert("Error", "Failed to create the edit page", "OK");
            }
        }
        catch (Exception ex)
        {
            // Log the exception and show an error message to the user
            Console.WriteLine($"Exception occurred: {ex.Message}");
            await DisplayAlert("Error", "An unexpected error occurred while navigating to the edit page.", "OK");
        }
    }

    private async void TapGestureRecognizer_OnTapped_ToDeleteTransaction(object? sender, TappedEventArgs e)
    {
        await AnimationBorder((Border)sender, true);
        var result = await App.Current.MainPage.DisplayAlert("Delete", "Are you sure you want to delete entry?", "Yes", "No");

        if (result)
        {
            var transaction = (Transaction)e.Parameter;
            _repository.Delete(transaction);

            Reload();
        }
        else
        {
            await AnimationBorder((Border)sender, false);

        }
    }

    private Color _borderOriginalBackgroundColor;
    private string _labelOriginalText;

    private async Task AnimationBorder(Border border, bool IsDeleteAnimation)
    {
        var label = (Label)border.Content;

        if (IsDeleteAnimation)
        {
            _borderOriginalBackgroundColor = border.BackgroundColor;
            _labelOriginalText = label.Text;
            await border.RotateYTo(90, 500);
            border.BackgroundColor = Colors.Red;
            label.TextColor = Colors.White;
            label.Text = "X";
            await border.RotateYTo(180, 500);
        }
        else
        {
            await border.RotateYTo(90, 500);
            label.TextColor = Colors.Black;
            label.Text = _labelOriginalText;
            border.BackgroundColor = _borderOriginalBackgroundColor;
            await border.RotateYTo(0, 500);
        }
    }
}