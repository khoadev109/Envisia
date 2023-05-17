using Envisia.BackgroundService;
using Envisia.BackgroundService.Hangfire;
using Envisia.Library.Helpers;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Register(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppSettingsHelper.Configure(app.Services.GetRequiredService<IConfiguration>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangFire(builder.Configuration);

app.MapControllers();

app.Run();
