using Microsoft.EntityFrameworkCore;
using minicore_comiciones.DBContext;
using minicore_comiciones.Repositories;
using minicore_comiciones.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repos y Servicios
builder.Services.AddScoped<IVentasRepository, EfVentasRepository>();
builder.Services.AddScoped<IComisionCalculatorFactory, ComisionCalculatorFactory>();
builder.Services.AddScoped<IComisionService, ComisionService>();

// CORS, JSON options y Swagger
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
