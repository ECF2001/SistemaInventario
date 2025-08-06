
using Manager.Repositorio;
using Manager.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi.Models;
using SistemaInventario.Manager.Servicios;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios y configuraciones necesarias

// Registrar opciones y contexto de acci�n
builder.Services.AddOptions();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext!);
});

// Conexi�n a la base de datos
var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddScoped<AppDbContext.AppDbContext>();  // Aseg�rate de que AppDbContext est� configurado correctamente

// Registrar IProveedorService y su implementaci�n  
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();


// Configuraci�n de controladores y JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Agregar Swagger para documentaci�n API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventario API", Version = "v1" });
});

// Configurar pol�ticas de CORS (permite que cualquier origen realice solicitudes)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // Permitir cualquier origen
              .AllowAnyMethod()    // Permitir cualquier m�todo HTTP
              .AllowAnyHeader();   // Permitir cualquier encabezado
    });
});

// Iniciar la aplicaci�n
var app = builder.Build();

// Configuraci�n de middleware para Swagger
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Farmacia e Inventario API v1"));

// Configuraci�n de redirecci�n HTTPS
app.UseHttpsRedirection();

// Configuraci�n de rutas y CORS
app.UseRouting();
app.UseCors("AllowAll");

// Habilitar autorizaci�n (si es necesario)
app.UseAuthorization();

// Mapeo de controladores
app.MapControllers();



// Ejecutar la aplicaci�n
app.Run();
