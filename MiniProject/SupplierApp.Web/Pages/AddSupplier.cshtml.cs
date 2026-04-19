using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SupplierApp.Web.Pages
{
    public class AddSupplierModel : PageModel
    {
        private readonly IHttpClientFactory _http;

        public AddSupplierModel(IHttpClientFactory http) => _http = http;

        [BindProperty] public string Name { get; set; } = string.Empty;
        [BindProperty] public string Telephone { get; set; } = string.Empty;
        public string? Message { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _http.CreateClient("API");
            var body = JsonSerializer.Serialize(new { Name, Telephone, Code = 0 });
            var response = await client.PostAsync("api/suppliers",
                new StringContent(body, Encoding.UTF8, "application/json"));

            Message = response.IsSuccessStatusCode
                ? "Supplier added successfully."
                : "Failed to add supplier. Please try again.";

            return Page();
        }
    }
}
