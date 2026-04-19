using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SupplierApp.Web.Pages
{
    public class SearchSupplierModel : PageModel
    {
        private readonly IHttpClientFactory _http;

        public SearchSupplierModel(IHttpClientFactory http) => _http = http;

        [BindProperty(SupportsGet = true)] public string? Query { get; set; }
        public List<SupplierResult> Results { get; set; } = new();

        public async Task OnGetAsync()
        {
            if (string.IsNullOrWhiteSpace(Query)) return;

            var client = _http.CreateClient("API");
            var response = await client.GetAsync($"api/suppliers/search?name={Query}");
            if (!response.IsSuccessStatusCode) return;

            var json = await response.Content.ReadAsStringAsync();
            Results = JsonSerializer.Deserialize<List<SupplierResult>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        }

        public record SupplierResult(int Id, int Code, string Name, string Telephone);
    }
}
