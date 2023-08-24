using JasperFx.CodeGeneration;
using Oakton;
using Oakton.Resources;
using SequentialOutbox.Application;
using SequentialOutbox.Application.Commands;
using SequentialOutbox.Infrastructure.Database;
using SequentialOutbox.Infrastructure.Outbox;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;
using Wolverine.Postgresql;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine((cnx, opt) =>
{
    opt.PersistMessagesWithPostgresql(cnx.Configuration.GetConnectionString("Postgres")!);
    opt.Services.AddDatabase(cnx.Configuration);
    opt.Services.AddOutbox();
    opt.Services.AddValidators();
    opt.UseEntityFrameworkCoreTransactions();
    opt.Discovery.IncludeAssembly(typeof(ApplicationExtensions).Assembly);
    opt.Policies.AutoApplyTransactions();
    opt.AutoBuildMessageStorageOnStartup = true;
    opt.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;
});

builder.Services.AddResourceSetupOnStartup();

builder.Host.ApplyOaktonExtensions();

var app = builder.Build();

app.MapPostToWolverine<CreateOrderCommand, long>("api/orders");
app.MapPostToWolverine<MarkOrderAsPaidCommand, long>("api/orders/mark-as-paid");

app.MapWolverineEndpoints(a =>
{
    a.UseFluentValidationProblemDetailMiddleware();
});

await app.RunOaktonCommands(args);