var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// TEMPORARY TESTING 
var converter = new NumberToWords.API.NumberToWords();
Console.WriteLine(converter.Convert("123.45"));
Console.WriteLine(converter.Convert("1000"));
Console.WriteLine(converter.Convert("0.50"));
Console.WriteLine(converter.Convert("1000000.01"));
Console.WriteLine(converter.Convert("999999999999999"));

app.Run();