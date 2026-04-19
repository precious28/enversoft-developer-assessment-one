using SupplierApp.API.Repository;
using SupplierApp.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var connStr = builder.Configuration.GetConnectionString("SupplierDb")
    ?? throw new InvalidOperationException("Connection string 'SupplierDb' not found.");
builder.Services.AddScoped<ISupplierRepository>(_ => new SupplierRepository(connStr));
builder.Services.AddScoped<ISupplierService, SupplierService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.Run();
