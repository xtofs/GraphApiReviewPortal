namespace ADO;

internal record class AdoHttpClient(HttpClient client, string organization, string project) : IDisposable
{
    const string DEFAULT_ORGANIZATION = "microsoftgraph";
    const string DEFAULT_PROJECT = "onboarding";

    internal static AdoHttpClient Create(string personalAccessToken)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.AddAcceptJson();
        client.DefaultRequestHeaders.AddPersonalAccessToken(personalAccessToken);

        return new AdoHttpClient(client, DEFAULT_ORGANIZATION, DEFAULT_PROJECT);
    }

    public void Dispose()
    {
        ((IDisposable)client).Dispose();
    }

    public async Task<QueryResult> GetWorkItemQueryResultAsync(string queryId)
    {
        var url = $"https://dev.azure.com/{organization}/{project}/_apis/wit/wiql/{queryId}?api-version=7.0";
        var response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsJsonAsync<QueryResult>();
        return result;
    }

    public async Task<WorkItem> GetWorkItemAsync(long workItemId)
    {
        var url = $"https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/{workItemId}?$expand=relations&api-version=7.0";

        using HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var wi = await response.Content.ReadAsJsonAsync<WorkItem, long>("wi", w => w.Id);
        return wi;
    }

    public async Task<PullRequest> GetPullRequestAsync(long pullRequestId)
    {
        var url = $"https://dev.azure.com/{organization}/{project}/_apis/git/pullrequests/{pullRequestId}?api-version=7.0";

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var pr = await response.Content.ReadAsJsonAsync<PullRequest, long>("pr", p => p.PullRequestId);
        return pr;
    }
}