namespace SnapTgBot.Utils;

internal static class StringExtensions
{
    public static string TrimSuffix(
        this string input,
        string suffixToRemove,
        StringComparison comparisonType = StringComparison.OrdinalIgnoreCase) =>
        input.EndsWith(suffixToRemove, comparisonType) ? input[..^suffixToRemove.Length] : input;
}
