using FinancialApp.Repositories;
using FinancialApp.Views;
using LiteDB;
using Microsoft.Extensions.Logging;
using static FinancialApp.AppSettings;

namespace FinancialApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterDependencyInjection()
            .RegisterViews();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterDependencyInjection(this MauiAppBuilder builder)
    {
        builder.Services.AddScoped<ILiteDatabase>(
            _ => new LiteDatabase($"Filename={DatabasePath};Connection=Shared"));

        builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
        return builder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<TransactionAdd>();
        builder.Services.AddTransient<TransactionEdit>();
        builder.Services.AddTransient<TransactionList>();
        return builder;
    }
}