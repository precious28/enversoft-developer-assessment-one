var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddHttpClient("API", c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5000/"));

var app = builder.Build();
app.UseStaticFiles();
app.MapRazorPages();
app.MapGet("/", () => Results.Redirect("/AddSupplier"));
app.Run();
