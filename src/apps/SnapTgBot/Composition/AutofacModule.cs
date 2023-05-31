using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SnapTgBot.Commands;
using SnapTgBot.Messaging;
using SnapTgBot.Security;
using SnapTgBot.Settings;
using SnapTgBot.Settings.Providers;
using SnapTgBot.Startup;
using SnapTgBot.WebCam;
using Telegram.Bot;

namespace SnapTgBot.Composition;

internal sealed class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterLogging(builder);
        RegisterSettings(builder);
        RegisterBot(builder);
        RegisterServices(builder);
    }

    private void RegisterLogging(ContainerBuilder builder)
    {
        var services = new ServiceCollection();
        services.AddLogging(l =>
        {
            l.ClearProviders();
            l.AddConsole();
            l.AddDebug();
        });

        builder.Populate(services);
    }

    private void RegisterSettings(ContainerBuilder builder)
    {
        RegisterConfiguration(builder);

        builder.RegisterType<ConfigurationSettingsProvider>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterSettings<TelegramBotSettings>();
        builder.RegisterSettings<SecuritySettings>();
        builder.RegisterSettings<WebCamSettings>();
    }

    private void RegisterConfiguration(ContainerBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddUserSecrets(ThisAssembly, optional: true);
        var configuration = configurationBuilder.Build();

        builder.RegisterInstance(configuration).AsImplementedInterfaces();
    }

    private void RegisterBot(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.IsAssignableTo(typeof(IStartupTask)))
            .AsImplementedInterfaces();

        builder
            .Register(ctx => new TelegramBotClient(ctx.Resolve<TelegramBotSettings>().AccessToken))
            .As<ITelegramBotClient>()
            .SingleInstance();

        builder.RegisterType<Bot.SnapTgBot>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<TelegramUpdateHandler>().AsImplementedInterfaces();
        builder.RegisterType<TelegramMessageHandler>().AsImplementedInterfaces();
        builder.RegisterType<SecurityGuard>().AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.IsAssignableTo(typeof(ITelegramCommand)))
            .AsImplementedInterfaces();
    }

    private void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<WebCamService>().AsImplementedInterfaces().SingleInstance();
    }
}
