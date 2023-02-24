namespace Model;

internal static class StringExtensions
{
    public static string PrefixOf(this string str, char sep)
    {
        var ix = str.IndexOf(sep);
        return ix < 0 ? str : str.Substring(0, ix);
    }
}
