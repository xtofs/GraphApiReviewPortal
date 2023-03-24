namespace ADO;


using System;
using System.Text.Json.Serialization;

internal partial class PullRequest
{
    [JsonPropertyName("repository")]
    public required Repository Repository { get; set; }

    [JsonPropertyName("pullRequestId")]
    public long PullRequestId { get; set; }

    [JsonPropertyName("codeReviewId")]
    public long CodeReviewId { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("createdBy")]
    public CreatedBy? CreatedBy { get; set; }

    [JsonPropertyName("creationDate")]
    public DateTimeOffset CreationDate { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("sourceRefName")]
    public string? SourceRefName { get; set; }

    [JsonPropertyName("targetRefName")]
    public string? TargetRefName { get; set; }

    [JsonPropertyName("mergeStatus")]
    public string? MergeStatus { get; set; }

    [JsonPropertyName("isDraft")]
    public bool IsDraft { get; set; }

    [JsonPropertyName("mergeId")]
    public Guid MergeId { get; set; }

    [JsonPropertyName("lastMergeSourceCommit")]
    public LastMergeSourceCommitClass? LastMergeSourceCommit { get; set; }

    [JsonPropertyName("lastMergeTargetCommit")]
    public LastMergeSourceCommitClass? LastMergeTargetCommit { get; set; }

    [JsonPropertyName("lastMergeCommit")]
    public LastMergeCommit? LastMergeCommit { get; set; }

    [JsonPropertyName("reviewers")]
    public Reviewer[]? Reviewers { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("supportsIterations")]
    public bool SupportsIterations { get; set; }

    [JsonPropertyName("artifactId")]
    public string? ArtifactId { get; set; }
}

internal partial class CreatedBy
{
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("uniqueName")]
    public string? UniqueName { get; set; }

    [JsonPropertyName("imageUrl")]
    public Uri? ImageUrl { get; set; }

    [JsonPropertyName("descriptor")]
    public string? Descriptor { get; set; }
}

internal partial class Links
{
    [JsonPropertyName("avatar")]
    public Avatar? Avatar { get; set; }
}

internal partial class Avatar
{
    [JsonPropertyName("href")]
    public Uri? Href { get; set; }
}

internal partial class LastMergeCommit
{
    [JsonPropertyName("commitId")]
    public string? CommitId { get; set; }

    [JsonPropertyName("author")]
    public Author? Author { get; set; }

    [JsonPropertyName("committer")]
    public Author? Committer { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }
}

internal partial class Author
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; set; }
}

internal partial class LastMergeSourceCommitClass
{
    [JsonPropertyName("commitId")]
    public string? CommitId { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }
}

internal partial class Repository
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("project")]
    public Project? Project { get; set; }

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("remoteUrl")]
    public Uri? RemoteUrl { get; set; }

    [JsonPropertyName("sshUrl")]
    public string? SshUrl { get; set; }

    [JsonPropertyName("webUrl")]
    public Uri? WebUrl { get; set; }

    [JsonPropertyName("isDisabled")]
    public bool IsDisabled { get; set; }

    [JsonPropertyName("isInMaintenance")]
    public bool IsInMaintenance { get; set; }
}

internal partial class Project
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("revision")]
    public long Revision { get; set; }

    [JsonPropertyName("visibility")]
    public string? Visibility { get; set; }

    [JsonPropertyName("lastUpdateTime")]
    public DateTimeOffset LastUpdateTime { get; set; }
}

internal partial class Reviewer
{
    [JsonPropertyName("reviewerUrl")]
    public Uri? ReviewerUrl { get; set; }

    [JsonPropertyName("vote")]
    public long Vote { get; set; }

    [JsonPropertyName("hasDeclined")]
    public bool HasDeclined { get; set; }

    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("isRequired")]
    public bool IsRequired { get; set; }

    [JsonPropertyName("isFlagged")]
    public bool IsFlagged { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("uniqueName")]
    public string? UniqueName { get; set; }

    [JsonPropertyName("imageUrl")]
    public Uri? ImageUrl { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("isContainer")]
    public bool? IsContainer { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("votedFor")]
    public VotedFor[]? VotedFor { get; set; }
}

internal partial class VotedFor
{
    [JsonPropertyName("reviewerUrl")]
    public Uri? ReviewerUrl { get; set; }

    [JsonPropertyName("vote")]
    public long Vote { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("uniqueName")]
    public string? UniqueName { get; set; }

    [JsonPropertyName("imageUrl")]
    public Uri? ImageUrl { get; set; }

    [JsonPropertyName("isContainer")]
    public bool IsContainer { get; set; }
}

