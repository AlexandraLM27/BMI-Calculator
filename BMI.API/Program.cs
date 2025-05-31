using BMI.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Adaugă servicii pentru controlere
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Redirect HTTP -> HTTPS
app.UseHttpsRedirection();
// Middleware pentru autorizare (chiar dacă nu folosim încă)
app.UseDefaultFiles();     // caută index.html implicit
app.UseStaticFiles();      // servește fișierele
app.UseRouting();          // permite rutare
app.UseAuthorization();    // (dacă e cazul)
app.UseCors();
app.MapControllers();
app.MapFallbackToFile("index.html");
// Endpoint existent - WeatherForecast
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
