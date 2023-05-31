using Autofac;
using SnapTgBot.Bot;
using SnapTgBot.Composition;

var bot = CreateBot();
await bot.Run();

static ISnapTgBot CreateBot()
{
    var builder = new ContainerBuilder();
    builder.RegisterModule<AutofacModule>();
    var container = builder.Build();
    return container.Resolve<ISnapTgBot>();
}
