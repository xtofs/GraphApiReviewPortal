
using ADO;

namespace Model;

public class ApiReviewRequest
{
    public long WorkItemId { get; init; }
    public string? Title { get; init; }
    public string? State { get; init; }

    public string Url => $"https://microsoftgraph.visualstudio.com/onboarding/_workitems/edit/{WorkItemId}";

    public ApiReviewPullRequest PullRequest { get; init; } = default!;

    public User[] Reviewers { get; init; } = Array.Empty<User>();

    public User? Owner { get; private set; }

    internal static ApiReviewRequest From(WorkItem wi, ADO.PullRequest pr)
    {
        return new ApiReviewRequest
        {
            WorkItemId = wi.Id,
            Title = wi.Fields.Title,
            State = wi.Fields.State,
            Owner = User.From(wi.Fields.Owner),
            Reviewers = new[] { User.From(wi.Fields.ApiReviewer1), User.From(wi.Fields.ApiReviewer2) },
            PullRequest = new ApiReviewPullRequest
            {
                Id = pr.PullRequestId,
                Status = pr.Status,
                LastMergeCommitDate = pr?.LastMergeCommit?.Author?.Date,
                ReviewStatus = pr.Reviewers
                    .Where(r => r.IsRequired)
                    .Select(r => (Short(r), r.UniqueName, VoteIdentifier(r.Vote)))
                    .ToList().AsReadOnly(),
            }
        };

    }

    static string Short(Reviewer r)
    {
        return r.UniqueName.EndsWith("API Review") ?
           "Reviewers"
        :
            r.DisplayName.PrefixOf(' ');
    }

    private static string VoteIdentifier(long vote)
    {
        return VOTE.TryGetValue(vote, out var str) ? str : $"unknown ({vote})";
    }

    private static Dictionary<long, string> VOTE = new()
    {
        [10] = "approved",
        [5] = "suggestions",
        [0] = "pending",
        [-5] = "waiting",
        [-10] = "rejected"
        // [10] = "Approved",
        // [5] = "Approved with suggestion",
        // [0] = "No review yet",
        // [-5] = "Waiting for author",
        // [-10] = "Rejected"
    };
}
