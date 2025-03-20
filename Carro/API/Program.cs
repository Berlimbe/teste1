using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Carro> carros = [
    new Carro { Id = 1, Name = "Ferrari" },
    new Carro { Id = 2, Name =  "Fiat" }
];

// Exemplo de requisição
app.MapGet("/", () => "Hello World!");

// GET: api/carros
app.MapGet("/api/carros", () => {
    return Results.Ok(carros);
});

app.Run();
