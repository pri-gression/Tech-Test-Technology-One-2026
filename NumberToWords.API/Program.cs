var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

// CORS
builder.Services.AddCors( options=>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3001")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowReactApp");

// Endpoint 
app.MapGet("/api/convert", (string number) =>
{
    try
    {
        var converter = new NumberToWords.API.NumberToWords();
        string result = converter.Convert(number);
        return Results.Ok(new { success = true, result = result });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { success = false, error = ex.Message });
    }
});

// TEMPORARY TESTING 
// var converter = new NumberToWords.API.NumberToWords();
// Console.WriteLine(converter.Convert("123.45"));
// Console.WriteLine(converter.Convert("1000"));
// Console.WriteLine(converter.Convert("0.50"));
// Console.WriteLine(converter.Convert("1000000.01"));
// Console.WriteLine(converter.Convert("999999999999999"));

app.Run();