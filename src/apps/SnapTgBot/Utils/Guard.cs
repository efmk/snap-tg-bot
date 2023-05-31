using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace SnapTgBot.Utils;

internal static class Guard
{
    [DebuggerStepThrough]
    [ContractAnnotation("halt <= assertion: false")]
    public static void Assert([DoesNotReturnIf(false)] bool assertion, string message)
    {
        if (!assertion)
        {
            throw new InvalidOperationException(message);
        }
    }
}
