using Akka.Hosting;
using Akka.Hosting.Maui;
using Microsoft.Extensions.Logging;

namespace Akka.MauiDemo;

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

        builder.Services
            .AddAkkaMaui("TestSys", config =>
            {
                config.WithActors((system, registry) =>
                {
                    var echo = system.ActorOf(ClickActor.Props);
                    registry.Register<ClickActor>(echo);
                });
            })
            .AddTransient<MainPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}