using AgencyPlatform.API.Middlewares;
using AgencyPlatform.Application.Interfaces.Repositories;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Application.Interfaces.Services.Cupones;
using AgencyPlatform.Application.Interfaces.Services.Puntos;
using AgencyPlatform.Application.Validators;
using AgencyPlatform.Application.Validators.CuponesCliente;
using AgencyPlatform.Infrastructure.Data;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Infrastructure.Repositories;
using AgencyPlatform.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using AgencyPlatform.Infrastructure.Services.Cupones;
using AgencyPlatform.Application.Interfaces.Services.MembresiasVip;
using AgencyPlatform.Application.Interfaces.Services.SuscripcionesVip;
using AgencyPlatform.Infrastructure.Services.SuscripcionesVip;
using AgencyPlatform.Application.Validators.SuscripcionesVip;
using AgencyPlatform.Application.Interfaces.Services.VisitasPerfil;
using AgencyPlatform.Infrastructure.Services.VisitasPerfil;
using AgencyPlatform.Application.Validators.VisitasPerfil;

var builder = WebApplication.CreateBuilder(args);

// ===== CONFIGURACIÓN GENERAL =====
var config = builder.Configuration;
var jwtSettings = config.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];
var jwtIssuer = jwtSettings["Issuer"];
var maxUploadSizeBytes = (int.Parse(config["Uploads:MaxFileSizeMB"] ?? "10")) * 1024 * 1024;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// ===== BASE DE DATOS =====
builder.Services.AddDbContext<AgencyPlatformDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString("ConexionPruebaLocal")));

// ===== DEPENDENCIAS =====
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAgenciaRepository, AgenciaRepository>();
builder.Services.AddScoped<IAgenciaService, AgenciaService>();
builder.Services.AddScoped<IImagenPerfilService, ImagenPerfilService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();  // Añadido PerfilRepository
builder.Services.AddScoped<IPerfilService, PerfilService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IPuntoRepository, PuntoRepository>();
builder.Services.AddScoped<IPuntoService, PuntoService>();

builder.Services.AddScoped<ICuponService, CuponService>(); //Implementar 
builder.Services.AddScoped<ICuponRepository, CuponRepository>();
// Interfaces y servicios CuponesCliente
builder.Services.AddScoped<ICuponesClienteRepository, CuponesClienteRepository>();
builder.Services.AddScoped<ICuponesClienteService, CuponesClienteService>();

builder.Services.AddScoped<IMembresiaVipService, MembresiaVipService>();
builder.Services.AddScoped<IMembresiaVipRepository, MembresiaVipRepository>();

builder.Services.AddScoped<ISuscripcionVipService, SuscripcionVipService>();
builder.Services.AddScoped<ISuscripcionVipRepository, SuscripcionVipRepository>();

builder.Services.AddScoped<IVisitasPerfilRepository, VisitasPerfilRepository>();
builder.Services.AddScoped<IVisitasPerfilService, VisitasPerfilService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();









// ===== SERVICIO DE EMAIL =====
builder.Services.AddScoped<IEmailSender, EmailSender>();

// Comprobar configuración de SMTP
var smtpUser = builder.Configuration["Smtp:User"];
var smtpPass = builder.Configuration["Smtp:Password"];
var smtpHost = builder.Configuration["Smtp:Host"];

if (string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass) || string.IsNullOrEmpty(smtpHost))
{
    Console.WriteLine();
    Console.WriteLine("======================= ADVERTENCIA =======================");
    Console.WriteLine("Falta configuración SMTP en appsettings.json.");
    Console.WriteLine("Los correos electrónicos podrían no enviarse correctamente.");
    Console.WriteLine("==========================================================");
    Console.WriteLine();
}

// ===== VALIDADORES =====
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CrearAgenciaDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateAgenciaDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CrearPerfilDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<CrearPerfilDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CrearCuponClienteValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCuponClienteValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CrearSuscripcionVipDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateSuscripcionVipDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CrearVisitaPerfilDtoValidator>();




//builder.Services.AddScoped<ICuponesClienteService, CuponesClienteService>();
//builder.Services.AddScoped<ICuponesClienteRepository, CuponesClienteRepository>();


builder.Services.AddHttpContextAccessor();

// ===== JWT AUTH =====
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    // Aquí configuramos el tiempo de expiración y otros parámetros del JWT
    var jwtExpireMinutes = int.Parse(jwtSettings["ExpireMinutes"] ?? "1440"); // Por ejemplo 24 horas

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
        ClockSkew = TimeSpan.Zero // Esto es opcional: evita el desajuste en la expiración de la sesión
    };
});

// ===== CORS =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ===== LIMITES PARA ARCHIVOS GRANDES =====
builder.Services.Configure<IISServerOptions>(o => o.MaxRequestBodySize = maxUploadSizeBytes);
builder.Services.Configure<KestrelServerOptions>(o => o.Limits.MaxRequestBodySize = maxUploadSizeBytes);
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = maxUploadSizeBytes;
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

// ===== CONTROLLERS + VALIDACIÓN =====
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// ===== SWAGGER CONFIGURACIÓN COMPLETA =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgencyPlatform API", Version = "v1" });

    var jwtScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Description = "Escribe solo el token JWT (sin 'Bearer')",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtScheme.Reference.Id, jwtScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtScheme, Array.Empty<string>() }
    });

    c.OperationFilter<SwaggerFileUploadFilter>();
});

var app = builder.Build();

// ===== CREAR CARPETA DE SUBIDAS SI NO EXISTE =====
var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
Directory.CreateDirectory(uploadPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadPath),
    RequestPath = "/uploads"
});

// ===== SWAGGER SOLO EN DESARROLLO =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgencyPlatform API V1");
        c.DisplayRequestDuration();
    });
}

// ===== MIDDLEWARE PIPELINE =====
app.UseRouting();
app.UseCors("AllowAll");

// Solo redirigir HTTPS si no estamos en desarrollo
if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseExceptionHandlingMiddleware();
app.UseValidationExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// ===== SOPORTE PARA SUBIDA DE ARCHIVOS EN SWAGGER =====
public class SwaggerFileUploadFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        const string mime = "multipart/form-data";
        if (operation.RequestBody == null || !operation.RequestBody.Content.ContainsKey(mime))
            return;

        var fileParams = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile));

        foreach (var param in fileParams)
        {
            operation.RequestBody.Content[mime].Schema.Properties[param.Name!] = new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            };
        }
    }
}