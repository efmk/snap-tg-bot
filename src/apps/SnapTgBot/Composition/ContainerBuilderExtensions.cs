using Autofac;
using SnapTgBot.Settings.Providers;

namespace SnapTgBot.Composition;

internal static class ContainerBuilderExtensions
{
    public static void RegisterSettings<TSettings>(this ContainerBuilder builder)
        where TSettings : notnull, new()
    {
        builder
            .Register(ctx => ctx.Resolve<ISettingsProvider>().GetSettings<TSettings>())
            .AsSelf()
            .InstancePerLifetimeScope()
            .IfNotRegistered(typeof(TSettings));
    }
}
