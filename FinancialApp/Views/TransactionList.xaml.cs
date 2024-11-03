using CommunityToolkit.Mvvm.Messaging;
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

    private async void OnButtonClicked_To_TransactionEdit(object? sender, EventArgs e)
    {
        try
        {
            var editPage = _serviceProvider.GetService<TransactionEdit>();
            if (editPage != null)
            {
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
}