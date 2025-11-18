using DotNetEnv;
using SmartTravelAI.Config;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.ConfigureSwagger();

builder.WithCORS()
       .WithAuthentication()
       .WithAuthorization();

builder.UsePostgresDatabase();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerWithUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors(AuthConfig.CorsPolicyKey);

app.Run();

Console.WriteLine("Running...");