namespace ADO;

using System;
using System.Text.Json.Serialization;

internal partial class QueryResult
{
    [JsonPropertyName("queryType")]
    public string? QueryType { get; set; }

    [JsonPropertyName("queryResultType")]
    public string? QueryResultType { get; set; }

    [JsonPropertyName("asOf")]
    public DateTimeOffset AsOf { get; set; }

    [JsonPropertyName("columns")]
    public Column[]? Columns { get; set; }

    [JsonPropertyName("sortColumns")]
    public SortColumn[]? SortColumns { get; set; }

    [JsonPropertyName("workItems")]
    public WorkItemReference[]? WorkItems { get; set; }
}

internal partial class Column
{
    [JsonPropertyName("referenceName")]
    public string? ReferenceName { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }
}

internal partial class SortColumn
{
    [JsonPropertyName("field")]
    public Column? Field { get; set; }

    [JsonPropertyName("descending")]
    public bool Descending { get; set; }
}

internal partial class WorkItemReference
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }
}

