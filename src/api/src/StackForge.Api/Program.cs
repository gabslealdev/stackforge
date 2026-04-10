
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackForge.Infrastructure.Authentication;
using StackForge.Infrastructure.Data.Seed.Extensions;
using StackForge.Infrastructure.DependencyInjection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("StackForgeWeb", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var jwtSection = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSection["SecretKey"]!;
var issuer = jwtSection["Issuer"];
var audience = jwtSection["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("MentorOnly", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("profileType", "Mentor");
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("StackForgeWeb");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Ok(new { status = "Ok"})).WithName("HealthCheck");

await app.Services.InitializeStackAsync();

app.Run();