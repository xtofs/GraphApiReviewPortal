
using System.Collections.ObjectModel;

namespace Model;


public class ApiReviewPullRequest
{
    public string? Title { get; init; }
    public long Id { get; init; }
    public string? Status { get; init; }

    public string Url => $"https://microsoftgraph.visualstudio.com/_git/onboarding/pullrequest/{Id}";

    public required ReadOnlyCollection<(string Name, string Vote)>? ReviewStatus { get; init; }

}

public class User
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public string FirstName => Name.PrefixOf(' ');

    internal static User From(ADO.ApiReviewer reviewer)
    {
        return new User
        {
            Name = reviewer?.DisplayName ?? "?",
            Id = reviewer?.UniqueName ?? "?",
        };
    }
}

internal static class StringExtensions
{
    public static string PrefixOf(this string str, char sep)
    {
        var ix = str.IndexOf(sep);
        return ix < 0 ? str : str.Substring(0, ix);
    }
}
