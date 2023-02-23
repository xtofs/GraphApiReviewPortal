namespace ADO;

using System;
using System.Text.Json.Serialization;

internal partial class WorkItem
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("rev")]
    public long Rev { get; set; }

    [JsonPropertyName("fields")]
    public WorkItemFields Fields { get; set; } = new WorkItemFields();

    [JsonPropertyName("relations")]
    public Relation[]? Relations { get; set; }

    [JsonPropertyName("_links")]
    public WorkItemLinks? Links { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    public long GetPullRequestId()
    {
        var pullRequestUrl = Relations?.Where(rel => rel.Attributes?.Name == "Pull Request").Select(rel => rel.Url).FirstOrDefault();
        var pullRequestId = pullRequestUrl?.Split("/").Last().Split("%2F").Last() ?? "0";
        return long.TryParse(pullRequestId, out var id) ? id : 0;
    }
}

internal partial class WorkItemFields
{
    [JsonPropertyName("System.AreaPath")]
    public string? SystemAreaPath { get; init; }

    [JsonPropertyName("System.TeamProject")]
    public string? SystemTeamProject { get; set; }

    [JsonPropertyName("System.IterationPath")]
    public string? SystemIterationPath { get; set; }

    [JsonPropertyName("System.WorkItemType")]
    public string? SystemWorkItemType { get; set; }

    [JsonPropertyName("System.State")]
    public string? State { get; set; }

    [JsonPropertyName("System.Reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("System.AssignedTo")]
    public ApiReviewer? SystemAssignedTo { get; set; }

    [JsonPropertyName("System.CreatedDate")]
    public DateTimeOffset SystemCreatedDate { get; set; }

    [JsonPropertyName("System.CreatedBy")]
    public ApiReviewer? SystemCreatedBy { get; set; }

    [JsonPropertyName("System.ChangedDate")]
    public DateTimeOffset SystemChangedDate { get; set; }

    [JsonPropertyName("System.ChangedBy")]
    public ApiReviewer? SystemChangedBy { get; set; }

    [JsonPropertyName("System.CommentCount")]
    public long SystemCommentCount { get; set; }

    [JsonPropertyName("System.Title")]
    public string? Title { get; set; }

    [JsonPropertyName("System.BoardColumn")]
    public string? SystemBoardColumn { get; set; }

    [JsonPropertyName("System.BoardColumnDone")]
    public bool SystemBoardColumnDone { get; set; }

    [JsonPropertyName("Microsoft.VSTS.Common.StateChangeDate")]
    public DateTimeOffset StateChangeDate { get; set; }

    [JsonPropertyName("Microsoft.VSTS.Common.StackRank")]
    public double MicrosoftVstsCommonStackRank { get; set; }

    [JsonPropertyName("Onboarding.Owner")]
    public ApiReviewer? Owner { get; set; }

    [JsonPropertyName("Microsoft.VSTS.CMMI.Committed")]
    public string? MicrosoftVstsCmmiCommitted { get; set; }

    [JsonPropertyName("WEF_DEC3B17EE0EF4480BB3E5648659AF1AC_Kanban.Column")]
    public string? WefDec3B17Ee0Ef4480Bb3E5648659Af1AcKanbanColumn { get; set; }

    [JsonPropertyName("WEF_DEC3B17EE0EF4480BB3E5648659AF1AC_Kanban.Column.Done")]
    public bool WefDec3B17Ee0Ef4480Bb3E5648659Af1AcKanbanColumnDone { get; set; }

    [JsonPropertyName("Onboarding.Onroadmap")]
    public bool OnboardingOnRoadmap { get; set; }

    [JsonPropertyName("Onboarding.LinktoVSOparentitem")]
    public string? ParentItem { get; set; }

    [JsonPropertyName("Custom.Exceptionneededforpreview")]
    public bool ExceptionNeededForPreview { get; set; }

    [JsonPropertyName("Custom.GAexceptionreason")]
    public string? GaExceptionReason { get; set; }

    [JsonPropertyName("Custom.PrivacyReviewRequired")]
    public bool CustomPrivacyReviewRequired { get; set; }

    [JsonPropertyName("Custom.SecurityReviewRequired")]
    public bool CustomSecurityReviewRequired { get; set; }

    [JsonPropertyName("Custom.WorkloadID")]
    public string? WorkloadId { get; set; }

    [JsonPropertyName("Custom.APIreviewer1")]
    public ApiReviewer? ApiReviewer1 { get; set; }

    [JsonPropertyName("Custom.APIreviewer2")]
    public ApiReviewer? ApiReviewer2 { get; set; }

    [JsonPropertyName("Custom.APIchangesize")]
    public string? CustomApiChangeSize { get; set; }

    [JsonPropertyName("System.Description")]
    public string? SystemDescription { get; set; }

    [JsonPropertyName("Onboarding.ApiReview_instructions")]
    public Uri? OnboardingApiReviewInstructions { get; set; }
}

internal partial class ApiReviewer
{
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("_links")]
    public CustomAPIReviewerLinks? Links { get; set; }

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("uniqueName")]
    public string? UniqueName { get; set; }

    [JsonPropertyName("imageUrl")]
    public Uri? ImageUrl { get; set; }

    [JsonPropertyName("descriptor")]
    public string? Descriptor { get; set; }
}

internal partial class CustomAPIReviewerLinks
{
    [JsonPropertyName("avatar")]
    public HtmlClass? Avatar { get; set; }
}

internal partial class HtmlClass
{
    [JsonPropertyName("href")]
    public Uri? Href { get; set; }
}

internal partial class WorkItemLinks
{
    [JsonPropertyName("self")]
    public HtmlClass? Self { get; set; }

    [JsonPropertyName("workItemUpdates")]
    public HtmlClass? WorkItemUpdates { get; set; }

    [JsonPropertyName("workItemRevisions")]
    public HtmlClass? WorkItemRevisions { get; set; }

    [JsonPropertyName("workItemComments")]
    public HtmlClass? WorkItemComments { get; set; }

    [JsonPropertyName("html")]
    public HtmlClass? Html { get; set; }

    [JsonPropertyName("workItemType")]
    public HtmlClass? WorkItemType { get; set; }

    [JsonPropertyName("fields")]
    public HtmlClass? Fields { get; set; }
}

internal partial class Relation
{
    [JsonPropertyName("rel")]
    public string? Rel { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("attributes")]
    public Attributes? Attributes { get; set; }
}

internal partial class Attributes
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("isLocked")]
    public bool? IsLocked { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("authorizedDate")]
    public DateTimeOffset? AuthorizedDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("resourceCreatedDate")]
    public DateTimeOffset? ResourceCreatedDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("resourceModifiedDate")]
    public DateTimeOffset? ResourceModifiedDate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("revisedDate")]
    public DateTimeOffset? RevisedDate { get; set; }
}

