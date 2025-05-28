using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PureScanApp.Services
{
      
        public class ProductInfoService
        {
            private readonly HttpClient _httpClient;
            private readonly string baseUrl = "https://jejnzrustmqducbvyulz.supabase.co/rest/v1";
            private readonly string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Implam56cnVzdG1xZHVjYnZ5dWx6Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDcxMjg3NTUsImV4cCI6MjA2MjcwNDc1NX0.5jFtRQL5LBcF0FscQetL3rENSRPjcSdf-1V90eAu-B4";

            public ProductInfoService()
            {
                _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            }

            public async Task<JsonObject?> GetProductInfoByName(string name)
            {
                var encodedName = Uri.EscapeDataString(name);
                var requestUrl = $"{baseUrl}/products?name=eq.{encodedName}";

                var response = await _httpClient.GetAsync(requestUrl);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                var array = JsonNode.Parse(json)?.AsArray();
                return array?.FirstOrDefault()?.AsObject();
            }
        }
}



