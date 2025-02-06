using CosplayApp.Views;
using Microsoft.Extensions.Logging;

namespace CosplayApp
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
                });
            builder.Services.AddTransient<CosplaysPage>();
            builder.Services.AddTransient<CosPlan>();
            builder.Services.AddTransient<ToDoItemPage>();
            builder.Services.AddTransient<ToDoListPage>();

            builder.Services.AddSingleton<CosPlanItemDatabase>();
            builder.Services.AddSingleton<ToDoItemDatabase>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
