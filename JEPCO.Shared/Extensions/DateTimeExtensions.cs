using JEPCO.Shared.Constants;

namespace JEPCO.Shared.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ToLocalTimeFromUtc(this DateTime UtcDateTime)
    {
        TimeZoneInfo defaultTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(SettingConstants.TimezoneName);

        var localValue = TimeZoneInfo.ConvertTimeFromUtc(UtcDateTime, defaultTimeZoneInfo);

        return localValue;
    }
    public static DateTime? ToLocalTimeFromUtc(this DateTime? UtcDateTime)
    {
        if (UtcDateTime == null)
            return null;

        return UtcDateTime.Value.ToLocalTimeFromUtc();
    }
}
