namespace ADO;

public record PullRequestThreads(List<PullRequestThread> value) { }


public record PullRequestThread(ulong id, DateTime lastUpdatedDate, List<PullRequestComment> comments)
{
}

public record PullRequestComment(ulong Id, PullRequestCommentAuthor Author, string Content, string CommentType, DateTime LastContentUpdatedDate)
{
}

public record PullRequestCommentAuthor(string DisplayName, string UniqueName, string Id)
{
}
