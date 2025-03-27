using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build(); 

// GET: api/carros
// Exemplo de requisição


// Endpoints relacionados ao recurso de carros
// GET: Lista todos os carros cadastrados
app.MapGet("/api/carros", ([FromServices] AppDataContext ber) => {
    if(ber.Carros.Any()){
        return Results.Ok(ber.Carros.ToList());
    }
    return Results.NotFound();
});

app.MapPost("/api/carros", ([FromBody] Carro carro, [FromServices] AppDataContext ber) => {
        ber.Carros.Add(carro);
        ber.SaveChanges();
        return Results.Created("", carro);
});

app.MapGet("/api/carros/{id}", ([FromRoute] int id, [FromServices] AppDataContext ber) => {
    Carro? carro = ber.Carros.Find(id);

    if(carro != null){
        return Results.Ok(carro);
    }

    return Results.NotFound();
});
// Put: atualiza os dados do CArro pelo Id
app.MapPut("/api/carros/{id}", ([FromRoute] int id, [FromBody] Carro carro, [FromServices] AppDataContext ber) => {
    Carro? entidade = ber.Carros.Find(id);
    
    if(entidade != null){
        entidade.Name = carro.Name;
        ber.Carros.Update(entidade);
        ber.SaveChanges();
        return Results.Ok(carro);
    }
    return Results.NotFound();
});
// Delete: Remove um carro pelo id
app.MapDelete("/api/carros/{id}", ([FromRoute] int id, [FromServices] AppDataContext ber) => {
    Carro? carro = ber.Carros.Find(id);
    if(carro == null){
        return Results.NotFound();
    }
    ber.Carros.Remove(carro);
    ber.SaveChanges();
    return Results.NoContent();
});

app.Run();
