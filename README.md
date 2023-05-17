# Akka.Hosting.Maui

HOCON-less configuration, application lifecycle management, `ActorSystem` startup, and actor instantiation for [Akka.NET](https://getakka.net/). This is an [Akka.Hosting](https://github.com/akkadotnet/Akka.Hosting) adapter for [.NET Maui](https://dotnet.microsoft.com/en-us/apps/maui) applications.

The reason this exists in the first place is because Microsoft.Extensions.Hosting aren't supported on .NET Maui and there are no plans to support it. We've adapted Akka.Hosting to run using a Maui `IMauiInitializeService` instead an `IHostedService` behind the scenes - and this should make it trivially easy to use [Akka.Hosting's 'ActorRegistry' to inject actors into your UI elements and background services](https://github.com/akkadotnet/Akka.Hosting#dependency-injection-outside-and-inside-akkanet).

## Installation

First, you need to install the Akka.Hosting.Maui package into your Maui application:

```shell
dotnet add package Akka.Hosting
```

Next, inside your `MauiProgram.cs` (or equivalent) call the `AddAkkaMaui` method:

```csharp
using Akka.Hosting.Maui;
using Akka.Hosting.MauiSample;
using Microsoft.Extensions.Logging;

namespace Akka.Hosting.MauiDemo;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
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

        return builder.Build();
    }
}
```

And from there, you should be able to use any of the other [Akka.Hosting](https://github.com/akkadotnet/Akka.Hosting) methods to configure your `ActorSystem` and your actors!