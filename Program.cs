using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using ServiceSitoPanel.src.context;
using ServiceSitoPanel.src.interfaces;
using ServiceSitoPanel.src.services;
using ServiceSitoPanel.Helpers;
using ServiceSitoPanel.src.mappers.users;
using serviceSidafWeb.Functions;


var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", corsBuilder =>
    {
        corsBuilder.WithOrigins(builder.Configuration["CORS:AllowedOrigins"]);
        corsBuilder.AllowAnyMethod();
        corsBuilder.AllowAnyHeader();
        corsBuilder.AllowCredentials();
    });
});

// Adiciona controllers
builder.Services.AddControllers();

// Obtém a string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão não foi encontrada no appsettings.json.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), npgsqlOptions => { }));

// Configuração do Swagger + JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ServiceSitoPanel",
        Version = "v1",
        Description = "API Sito Panel.",
    });

    // 🔐 Configuração do esquema JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Insira o token JWT desta forma: **Bearer {seu token}**",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Configuracao do JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["accessToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddScoped<ApplicationDbContext>(provider =>
{
    var options = provider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
    var jwtService = provider.GetRequiredService<JwtService>();

    var context = new ApplicationDbContext(options);

    var tenantId = jwtService.GetTenantFromToken();
    if (int.TryParse(tenantId, out var tenant_id))
        context.CurrentTenantId = tenant_id;

    var id = jwtService.GetIdFromToken();
    if (int.TryParse(id, out var user_id))
        context.CurrentUserId = user_id;

    return context;
});

builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGeneralService, GeneralService>();
builder.Services.AddScoped<IOrdersService, OrdersServices>();
builder.Services.AddScoped<IExpenses, ExpensesService>();
builder.Services.AddScoped<ISolicitationsService, SolicitationsService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<HelperService>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<ISettingsService, SettingsService>();

var app = builder.Build();

app.MapHealthChecks("/healthz");

// Middleware do Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Sito Panel. v1");
    c.RoutePrefix = string.Empty;
});

// Middleware do CORS
app.UseCors("CorsPolicy");

// Habilita WebSockets
app.UseWebSockets();

// Mapeia os controllers
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

// Executa a aplicação
await app.RunAsync();