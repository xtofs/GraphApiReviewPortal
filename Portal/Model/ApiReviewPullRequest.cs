
using System.Collections.ObjectModel;

namespace Model;


public class ApiReviewPullRequest
{
    public string? Title { get; init; }
    public long Id { get; init; }
    public string? Status { get; init; }

    public string Url => $"https://microsoftgraph.visualstudio.com/_git/onboarding/pullrequest/{Id}";

    public required ReadOnlyCollection<(string Name, string Id, string Vote)>? ReviewStatus { get; init; }

    public DateTimeOffset? LastMergeCommitDate { get; init; }
}

public class ReviewStatusComparer : IComparer<(string Name, string Id, string Vote)>
{
    public int Compare((string Name, string Id, string Vote) x, (string Name, string Id, string Vote) y)
    {
        if (x.Id.StartsWith("vstfs:")) return -1;
        if (y.Id.StartsWith("vstfs:")) return 1;
        // System.Console.WriteLine("{0} {1}", x.Id, y.Id);
        return x.Name.CompareTo(y.Name);
    }

    public static ReviewStatusComparer Default = new ReviewStatusComparer();
}

public class User
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public string FirstName => Name.PrefixOf(' ');

    internal static User From(ADO.User reviewer)
    {
        return new User
        {
            Name = reviewer?.DisplayName ?? "?",
            Id = reviewer?.UniqueName ?? "?",
        };
    }
}
