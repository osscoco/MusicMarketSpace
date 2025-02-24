using back.Interfaces;
using back.Repositories;
using back.Services;
using EFCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Identity;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "[token]"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
        {
            var now = DateTime.UtcNow;
            return (notBefore == null || notBefore <= now) && (expires == null || expires > now);
        }
    };
});

// Injection Services
var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        serverVersion
    )
);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var missingFields = context.ModelState
            .Where(e => e.Key == "$")
            .SelectMany(e => e.Value.Errors)
            .Select(err => err.ErrorMessage)
            .FirstOrDefault();

        string extractedField = null;

        if (!string.IsNullOrEmpty(missingFields))
        {
            var match = System.Text.RegularExpressions.Regex.Match(
                missingFields,
                @"missing required properties, including the following: (\w+)"
            );

            if (match.Success)
            {
                extractedField = match.Groups[1].Value;
            }
        }

        var response = new
        {
            success = false,
            message = extractedField != null
                ? $"Le champ '{extractedField}' est requis."
                : "Requête invalide. Vérifiez les champs obligatoires."
        };

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<TranslateException>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

//Pas sur qu'il soit nécessaire
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
