namespace ADO;

internal record class AdoHttpClient(HttpClient client, string organization, string project, bool saveApiResponses) : IDisposable
{
    const string DEFAULT_ORGANIZATION = "microsoftgraph";
    const string DEFAULT_PROJECT = "onboarding";

    internal static AdoHttpClient Create(string personalAccessToken, bool saveApiResponses)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.AddAcceptJson();
        client.DefaultRequestHeaders.AddPersonalAccessToken(personalAccessToken);

        return new AdoHttpClient(client, DEFAULT_ORGANIZATION, DEFAULT_PROJECT, saveApiResponses);
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

        var result = await response.Content.ReadAsJsonAsync<QueryResult, string>(saveApiResponses, "wiq", _ => queryId);
        return result;
    }

    public async Task<WorkItem> GetWorkItemAsync(long workItemId)
    {
        var url = $"https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/{workItemId}?$expand=relations&api-version=7.0";

        using HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var wi = await response.Content.ReadAsJsonAsync<WorkItem, long>(saveApiResponses, "wi", wi => wi.Id);
        return wi;
    }

    public async Task<PullRequest> GetPullRequestAsync(long pullRequestId)
    {
        var url = $"https://dev.azure.com/{organization}/{project}/_apis/git/pullrequests/{pullRequestId}?api-version=7.0";

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var pr = await response.Content.ReadAsJsonAsync<PullRequest, long>(saveApiResponses, "pr", pr => pr.PullRequestId);
        return pr;
    }

    public async Task<PullRequestThreads> GetPullRequestThreadsAsync(Guid repoId, long pullRequestId)
    {
        // https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-threads/list?view=azure-devops-rest-7.0&tabs=HTTP
        var url = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repoId}/pullRequests/{pullRequestId}/threads?api-version=7.0";

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var threads = await response.Content.ReadAsJsonAsync<PullRequestThreads, long>(saveApiResponses, "pr-thread", _ => pullRequestId);
        return threads;
    }

}
