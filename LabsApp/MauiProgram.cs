using CommunityToolkit.Maui;
using LabsApp.Pages;
using LabsApp.Services;
using Microsoft.Extensions.Logging;

namespace LabsApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiCommunityToolkit()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddTransient<IDbService,SqLiteService>();
        builder.Services.AddSingleton<SqLiteDemoViewModel>();
        builder.Services.AddSingleton<SqLiteDemo>();
        

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}