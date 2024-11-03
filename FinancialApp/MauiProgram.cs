using FinancialApp.Repositories;
using LiteDB;
using Microsoft.Extensions.Logging;
using static FinancialApp.AppSettings;

namespace FinancialApp
{
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
                .DependencyInjection();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder DependencyInjection(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<ILiteDatabase>(
                _ => new LiteDatabase($"Filename={DatabasePath};Connection=Shared"));

            builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
            return builder;
        }
    }
}
