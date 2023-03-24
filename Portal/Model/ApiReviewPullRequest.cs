
using System.Collections.ObjectModel;

namespace Model;


public class ApiReviewPullRequest
{
    public string? Title { get; init; }
    public long Id { get; init; }
    public string? Status { get; init; }

    public string Url => $"https://microsoftgraph.visualstudio.com/_git/onboarding/pullrequest/{Id}";

    public required ReadOnlyCollection<(string Name, string Id, string Vote)> ReviewStatus { get; init; }

    public IEnumerable<(string Name, string Id, string Vote)> OrderedReviewStatus =>
        ReviewStatus.Order(ReviewStatusComparer.Default);

    public DateTimeOffset? LastMergeCommitDate { get; init; }
    public required LatestUpdate LatestUpdate { get; init; }
}

public class ReviewStatusComparer : IComparer<(string Name, string Id, string Vote)>
{
    public int Compare((string Name, string Id, string Vote) x, (string Name, string Id, string Vote) y)
    {
        if (x.Id.StartsWith("vstfs:")) return -1;
        if (y.Id.StartsWith("vstfs:")) return 1;
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


public record LatestUpdate(DateTime? ReviewerLatest, string ReviewerAuthor, DateTime? ProducerLatest, string ProducerAuthor)
{

    private static HashSet<string> Reviewers = new(){
        "dkershaw@microsoft.com",
        "mikep@microsoft.com",
        "gdebruin@microsoft.com",
        "chrispre@microsoft.com",
        "eketo@microsoft.com",
        "duchau@microsoft.com",
    };

    public static LatestUpdate From(ADO.PullRequestThreads threads)
    {
        var latestMe = threads.value
            .SelectMany(c => c.comments)
            .Where(c => c.Author.Id != "00000002-0000-8888-8000-000000000000")
            .Where(c => Reviewers.Contains(c.Author.UniqueName))
            .MaxBy(prc => prc.LastContentUpdatedDate);
        var latestOther = threads.value
            .SelectMany(c => c.comments)
            .Where(c => c.Author.Id != "00000002-0000-8888-8000-000000000000")
            .Where(c => !Reviewers.Contains(c.Author.UniqueName))
            .MaxBy(prc => prc.LastContentUpdatedDate);
        return new LatestUpdate(
            latestMe?.LastContentUpdatedDate, latestMe?.Author?.DisplayName,
            latestOther?.LastContentUpdatedDate, latestOther?.Author?.DisplayName
        );
    }
}