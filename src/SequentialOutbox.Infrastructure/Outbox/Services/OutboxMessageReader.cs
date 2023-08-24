using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SequentialOutbox.Infrastructure.Outbox.Models;

namespace SequentialOutbox.Infrastructure.Outbox.Services;

internal sealed class OutboxMessageReader
{
    private readonly IConfiguration _configuration;

    public OutboxMessageReader(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async IAsyncEnumerable<OutboxMessageWrapper> GetUnpublishedMessagesAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var connectionString = _configuration.GetConnectionString("Postgres");
        await using var npgSqlConnection = new NpgsqlConnection(connectionString);
        await npgSqlConnection.OpenAsync(cancellationToken);

        await using (var cmd = npgSqlConnection.CreateCommand())
        {
            cmd.CommandText = "LISTEN outbox_messaged";
            await cmd.ExecuteNonQueryAsync(cancellationToken);
        }
        
        while (!cancellationToken.IsCancellationRequested)
        {
            await npgSqlConnection.WaitAsync(cancellationToken);

           await using var transaction =
                await npgSqlConnection.BeginTransactionAsync(IsolationLevel.RepeatableRead, cancellationToken);

           await using var readCommand = npgSqlConnection.CreateCommand();
           readCommand.Transaction = transaction;
           readCommand.CommandText = "SELECT 1";
           
           await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
           while (await reader.ReadAsync(cancellationToken))
           {
               
           }

           yield return null!;
           

           await transaction.CommitAsync(cancellationToken);
        }
        
        yield break;
    }
}