using CommunityToolkit.Mvvm.Messaging;
using FinancialApp.Libs.Utils;
using FinancialApp.Models;
using FinancialApp.Repositories;
using System.Globalization;
using System.Text;

namespace FinancialApp.Views;

public partial class TransactionEdit : ContentPage
{
    private Transaction _transaction;
    private readonly ITransactionRepository _repository;

    public TransactionEdit(ITransactionRepository repository)
    {
        _repository = repository;
        InitializeComponent();
    }

    public void SetTransactionToEdit(Transaction transaction)
    {
        _transaction = transaction;

        if (transaction.Type == TransactionType.Income)
        {
            RadioIncome.IsChecked = true;
        }
        else
        {
            RadioExpenses.IsChecked = true;
        }

        EntryName.Text = transaction.Name;

        DatePickerDate.Date = transaction.Date.Date;

        EntryValue.Text = transaction.Value.ToString();

    }

    private async void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        try
        {
            KeyboardBugFixAndroid.HideKeyboard();

            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            // Log the exception and show an error message to the user
            Console.WriteLine($"Exception occurred: {ex.Message}");
            await DisplayAlert("Error", "An unexpected error occurred while navigating back.", "OK");
        }
    }

    private async void Button_OnClicked_UpdateDb(object? sender, EventArgs e)
    {
        try
        {
            if (!IsValidData())
            {
                return;
            }

            UpdateTransactionInDatabase();

            KeyboardBugFixAndroid.HideKeyboard();

            await Navigation.PopModalAsync();

            WeakReferenceMessenger.Default.Send("update");

            var totalEntries = _repository.GetAll().Count;

            // Show success message
            await App.Current.MainPage.DisplayAlert("Message", $"Transaction entries - {totalEntries}", "Ok");
        }
        catch (Exception ex)
        {
            // Log the exception and show an error message to the user
            Console.WriteLine($"Exception occurred: {ex.Message}");
            await DisplayAlert("Error", "An unexpected error occurred while saving the transaction.", "OK");
        }
    }

    private void UpdateTransactionInDatabase()
    {
        try
        {
            var transaction = new Transaction()
            {
                Id = _transaction.Id,
                Name = EntryName.Text,
                Date = DatePickerDate.Date,
                Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expenses,
                Value = Math.Abs(decimal.Parse(EntryValue.Text, CultureInfo.InvariantCulture))
            };

            _repository.Update(transaction);
        }
        catch (FormatException ex)
        {
            // Log and display an error for invalid input format
            Console.WriteLine($"Format exception: {ex.Message}");
            DisplayAlert("Error", "Invalid value format. Please enter a valid decimal number.", "OK");
        }
        catch (Exception ex)
        {
            // Log and handle other unexpected errors
            Console.WriteLine($"Exception occurred while saving transaction: {ex.Message}");
            DisplayAlert("Error", "An error occurred while saving the transaction. Please try again.", "OK");
        }
    }

    private bool IsValidData()
    {
        var valid = true;
        var sb = new StringBuilder();

        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            sb.AppendLine("Name field must be filled");
            valid = false;
        }

        if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
        {
            sb.AppendLine("Value field must be filled");
            valid = false;
        }
        else if (!decimal.TryParse(EntryValue.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out _))
        {
            sb.AppendLine("Value field is invalid");
            valid = false;
        }

        if (!valid)
        {
            LabelError.IsVisible = true;
            LabelError.Text = sb.ToString();
        }

        return valid;
    }
}