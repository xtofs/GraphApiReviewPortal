

# how to run

```
cd Portal
dotnet run
```


Also, make sure you save the porsonal access token in the user secrets.
```sh
dotnet user-secrets set "ADO:PersonalAccessToken" "<your porsonal access token>"
```



# internal APIS


query for WorkItems and corresponding PRs

# ADO APIs

## Query By Id

https://learn.microsoft.com/en-us/rest/api/azure/devops/wit/wiql/query-by-id?view=azure-devops-rest-7.0&tabs=HTTP

## Get Work Item

https://learn.microsoft.com/en-us/rest/api/azure/devops/wit/work-items/get-work-item?view=azure-devops-rest-7.0&tabs=HTTP

## Get Pull Request By Id

https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-requests/get-pull-request-by-id?view=azure-devops-rest-7.0&tabs=HTTP

# Secrets

https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0
