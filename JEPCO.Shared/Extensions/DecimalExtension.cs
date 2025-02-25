namespace JEPCO.Shared.Extensions;

public static class DecimalExtension
{
    /// <summary>
    /// Takes decimals digits and ceil the number to that decimals digits count.
    /// Default: 1 decimal digit
    /// </summary>
    /// <param name="number"></param>
    /// <param name="decimalCount"></param>
    /// <returns></returns>
    public static decimal ToDefaultFormat(this decimal number, int decimalsCount = 3)
    {
        decimal decimalsValue = (decimal)Math.Pow(10, decimalsCount);

        decimal result = Math.Ceiling(number * decimalsValue) / decimalsValue;

        return result;
    }


    public static decimal? ToDefaultFormat(this decimal? number, int decimalsCount = 3)
    {
        if (!number.HasValue)
        {
            return number;
        }

        return ToDefaultFormat(number.Value, decimalsCount);
    }
}
