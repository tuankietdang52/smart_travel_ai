using DotNetEnv;
using SmartTravelAI.Config;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddOpenApi();
builder.ConfigureSwagger();

builder.WithCORS()
       .WithAuthorization()
       .WithAuthentication();

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