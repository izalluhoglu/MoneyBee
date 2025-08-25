using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MoneyBee.TransferModule.Application;
using MoneyBee.TransferModule.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwt = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !bool.TryParse(jwt["ValidateIssuer"], out var validateIssuer) || validateIssuer,
            ValidateAudience = !bool.TryParse(jwt["ValidateAudience"], out var validateAudience) || validateAudience,
            ValidateLifetime = !bool.TryParse(jwt["ValidateLifetime"], out var validateLifetime) || validateLifetime,
            ValidateIssuerSigningKey = !bool.TryParse(jwt["ValidateIssuerSigningKey"], out var signingKey) || signingKey,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"] ?? "2HFfr6zUCx2iczCE/K28yF4DL7DQCvCFB8cR9qUwQKCMgUXK+fsiO0UUniW79KFu"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
