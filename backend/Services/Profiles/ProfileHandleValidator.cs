using System.Text.RegularExpressions;
using Backend.DTOs;

namespace Backend.Services.Profiles;

public static partial class ProfileHandleValidator
{
    [GeneratedRegex(ProfileRules.DetailedHandlePattern, RegexOptions.Compiled)]
    private static partial Regex PatternRegex();

    public static string Normalize(string handle) => handle.Trim().ToLowerInvariant();

    public static HandleUnavailableReason? GetFormatError(string handle)
    {
        if (string.IsNullOrWhiteSpace(handle) || handle.Length < ProfileRules.MinLength)
            return HandleUnavailableReason.TooShort;

        if (handle.Length > ProfileRules.MaxLength)
            return HandleUnavailableReason.TooLong;

        return PatternRegex().IsMatch(handle) ? null : HandleUnavailableReason.InvalidFormat;
    }
}
