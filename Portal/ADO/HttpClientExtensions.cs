namespace System.Net.Http;

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

internal static class HttpClientExtensions
{
    public static void AddAcceptJson(this HttpRequestHeaders headers)
    {
        headers.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static void AddPersonalAccessToken(this HttpRequestHeaders headers, string personalAccessToken)
    {
        var header = Convert.ToBase64String(
                Encoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", "", personalAccessToken)));
        headers.Authorization = new AuthenticationHeaderValue("Basic", header);
    }


    public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
    {
        var body = await content.ReadAsStringAsync();
        var value = JsonSerializer.Deserialize<T>(body)!;
        return value;
    }

    public static async Task<T> ReadAsJsonAsync<T, S>(this HttpContent content)
    {
        var body = await content.ReadAsStringAsync();
        var value = JsonSerializer.Deserialize<T>(body)!;
        return value;
    }

    public static async Task<T> ReadAsJsonAsync<T, S>(this HttpContent content, string fileName, Func<T, S> getId)
    {
        return await ReadAsJsonAsync(content, fileName, getId, "log");
    }

    public static async Task<T> ReadAsJsonAsync<T, S>(this HttpContent content, string fileName, Func<T, S> getId, string logDirectory)
    {
        var body = await content.ReadAsStringAsync();
        var value = JsonSerializer.Deserialize<T>(body)!;

        var id = getId(value);
        while (true)
        {
            try
            {
                File.WriteAllText($"{logDirectory}/{fileName}-{id}.json", System.Text.Json.JsonSerializer.Serialize<T>(value, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
                break;
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        return value;
    }
}