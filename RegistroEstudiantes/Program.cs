using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiantes.Aplicacion.Servicios.Interface;
using RegistroEstudiantes.Infraestructura.Data;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Servicios
// =======================

builder.Services.AddDbContext<RegistroEstudiantesDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IInscripcionService, InscripcionService>();

builder.Services.AddControllers();

// Swagger clásico (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

// =======================
// Pipeline HTTP
// =======================

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "Registro de Estudiantes API";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
