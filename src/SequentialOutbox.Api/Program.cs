using SequentialOutbox.Application.Commands;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(opt =>
{
    opt.UseEntityFrameworkCoreTransactions();
});



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapWolverineEndpoints(a =>
{
    a.UseFluentValidationProblemDetailMiddleware();
});

app.MapPostToWolverine<CreateOrderCommand>("api/orders");


app.Run();