using ADO;

namespace Model;

internal class ApiReviewService : IDisposable
{

    const string PORTAL_QUERY = "07f74a85-6eea-4c89-ae8a-ca0da83a9734";
    // const string PORTAL_QUERY = "4211f01e-6749-438c-8390-bb8723b8e9b6"; // me

    public ApiReviewService(IConfiguration config)
    {
        var personalAccessToken = config["ADO:PersonalAccessToken"];
        if (personalAccessToken == null)
        {
            throw new Exception("ADO:PersonalAccessToken configuration required");
        }
        client = AdoHttpClient.Create(personalAccessToken);
    }

    private readonly AdoHttpClient client;

    public void Dispose()
    {
        ((IDisposable)client).Dispose();
    }


    internal async Task<IEnumerable<ApiReviewRequest>> GetApiReviewRequests(string? filter = null)
    {
        var result = await client.GetWorkItemQueryResultAsync(PORTAL_QUERY);
        // work item queries contain only IDs
        var reviewRequests = await Task.WhenAll(result.WorkItems!
            .AsParallel()
            .Select(GetApiReviewRequestFromRef)
        );

        if (filter != null)
        {
            return reviewRequests
                .Where(r => r.Reviewers.Any(u => u.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase)))
                .OrderByDescending(r => r.WorkItemId)
                ?? Enumerable.Empty<ApiReviewRequest>();
        }
        else
        {
            return reviewRequests.OrderByDescending(r => r.WorkItemId) ?? Enumerable.Empty<ApiReviewRequest>();
        }
    }

    private async Task<ApiReviewRequest> GetApiReviewRequestFromRef(WorkItemReference wiRef)
    {
        var wi = await client.GetWorkItemAsync(wiRef.Id)!;
        var pr = await client.GetPullRequestAsync(wi.GetPullRequestId())!;

        return ApiReviewRequest.From(wi, pr);
    }
}
