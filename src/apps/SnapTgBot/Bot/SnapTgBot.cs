using SnapTgBot.Startup;

namespace SnapTgBot.Bot;

internal sealed class SnapTgBot : ISnapTgBot
{
    private readonly IReadOnlyCollection<IStartupTask> _startupTasks;

    public SnapTgBot(IEnumerable<IStartupTask> startupTasks)
    {
        _startupTasks = startupTasks.ToList();
    }

    public async Task Run()
    {
        foreach (var startupTask in _startupTasks)
        {
            await startupTask.Run();
        }

        var tcs = new TaskCompletionSource();
        AppDomain.CurrentDomain.ProcessExit += (_, _) => { tcs.SetResult(); };
        await tcs.Task;
    }
}
