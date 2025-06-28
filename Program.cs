using Microsoft.EntityFrameworkCore;
using minicore_comiciones.DBContext;
using minicore_comiciones.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// DEBUG: descomenta si quieres ver en consola la cadena que está usando
// Console.WriteLine($"[DEBUG] ConnectionString: '{builder.Configuration.GetConnectionString("DefaultConnection")}'");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IComisionService, ComisionService>();

builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        // Omite automáticamente referencias circulares
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
