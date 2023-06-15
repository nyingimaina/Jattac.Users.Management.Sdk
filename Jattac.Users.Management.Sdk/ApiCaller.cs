using System.Text;
using System.Text.Json;
using Jattac.Users.Management.Sdk.Configuration;

namespace Jattac.Users.Management.Sdk
{
    internal class ApiCaller
    {
        private static readonly HttpClient httpClient = new HttpClient();


        public async Task<TResponse> PostAsync<TResponse>(
            object data,
            string relativeUrl,
            Guid tenantId,
            Action<HttpResponseMessage>? onResponse = null)
        {
            string jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var fullUrl = GetFullUrl(relativeUrl);
            var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
            request.Content = content;
            request.Headers.Add("x-jattac-api-key", tenantId.ToString());

            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (onResponse != null)
            {
                onResponse(response);
            }
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var deserializedResponse = JsonSerializer.Deserialize<TResponse>(responseString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (deserializedResponse != null)
                {
                    return deserializedResponse;
                }
                else
                {
                    throw new Exception("Unable to get response from message sending");
                }
            }
            else
            {
                throw new Exception($"Unable to send message. Status code: {response.StatusCode}");
            }
        }

        private string GetFullUrl(string relativeUrl)
        {
            var baseUrl = JattacUserManagementConfigurationManager.configurationSettings.BaseUrl;
            if (baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }
            var uriBuilder = new UriBuilder(baseUrl);
            if (!relativeUrl.StartsWith("/"))
            {
                relativeUrl = "/" + relativeUrl;
            }
            uriBuilder.Path = relativeUrl;
            return uriBuilder.ToString();
        }
    }
}