namespace WebAPI.Common.Helpers;

public static class DateHelpers
{
    public static string ToShortJsonDateString(this DateTime date) =>
        $"{date.Year}-{date.Month:00}-{date.Day:00}";
}
