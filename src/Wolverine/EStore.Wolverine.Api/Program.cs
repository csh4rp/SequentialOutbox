using JasperFx.CodeGeneration;
using Oakton;
using Oakton.Resources;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;
using Wolverine.SqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine((cnx, opt) =>
{
    opt.PersistMessagesWithSqlServer(cnx.Configuration.GetConnectionString("SqlServer")!);
    opt.UseEntityFrameworkCoreTransactions();
    opt.Policies.AutoApplyTransactions();
    opt.AutoBuildMessageStorageOnStartup = true;
    opt.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;
});

builder.Services.AddResourceSetupOnStartup();

builder.Host.ApplyOaktonExtensions();

var app = builder.Build();

app.MapWolverineEndpoints(a =>
{
    a.UseFluentValidationProblemDetailMiddleware();
});

await app.RunOaktonCommands(args);