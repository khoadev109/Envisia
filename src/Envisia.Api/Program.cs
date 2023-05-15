using Envisia.Core;
using Envisia.Infrastructure;
using Envisia.Infrastructure.Persistance;
using Envisia.Library;
using Envisia.Library.Attributes;
using Envisia.Library.Security;
using Envisia.Library.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var env = builder.Environment;

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddDebug();
    builder.SetMinimumLevel(LogLevel.Information);
});

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new JwtResolver(configuration).GetTokenParameters();
});

services.AddAuthorizations();

services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionResultFilterAttribute));
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new ExceptionConverter());
});


var corsPolicy = "CorsPolicy";

var validOrigins = configuration.GetSection("Client:ValidOrigins").Get<string[]>() ?? Array.Empty<string>();

services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, builder => builder
        .WithOrigins(validOrigins)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

services.AddEndpointsApiExplorer();
services.AddMemoryCache();
services.AddSwagger("Envisia API");
services.AddApplication();
services.AddInfrastructure();
services.AddScoped<JwtResolver>();
services.AddScoped<RsaResolver>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await DataInitializer.BootstrapDb(app);

app.UseCors(corsPolicy);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
