using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SequentialOutbox.Infrastructure.Outbox.Entities;
using SequentialOutbox.Infrastructure.Outbox.Models;

namespace SequentialOutbox.Infrastructure.Outbox.Services;

internal sealed class OutboxMessageReader
{
    private readonly IConfiguration _configuration;

    public OutboxMessageReader(IConfiguration configuration) => _configuration = configuration;

    public async IAsyncEnumerable<OutboxMessageWrapper> GetUnpublishedMessagesAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var connectionString = _configuration.GetConnectionString("SqlServer");
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(_configuration.GetValue<int>("Outbox:PoolingInterval")));
        
        await using var sqlConnection = new SqlConnection(connectionString);
        await sqlConnection.OpenAsync(cancellationToken);
        
        while (!cancellationToken.IsCancellationRequested)
        {
            await timer.WaitForNextTickAsync(cancellationToken);

            await using var transaction = sqlConnection.BeginTransaction(IsolationLevel.RepeatableRead);

           await using var readCommand = sqlConnection.CreateCommand();
           readCommand.Transaction = transaction;
           readCommand.CommandText = "SELECT 1";

           var messages = new List<OutboxMessageWrapper>();
           
           await using var reader = await readCommand.ExecuteReaderAsync(cancellationToken);
           while (await reader.ReadAsync(cancellationToken))
           {
               var wrapper = new OutboxMessageWrapper
               {
                   Generation = reader.GetInt64("Generation"),
                   SequenceNumber = reader.GetInt64("SequenceNumber"),
                   PrevGenLastSequenceNumber = reader.GetInt64("PrevGenLastSequenceNumber"),
                   Message = new OutboxMessage
                   {
                       Id = reader.GetInt64(nameof(OutboxMessage.Id)),
                       CreatedAt = reader.GetDateTime(nameof(OutboxMessage.CreatedAt)),
                       Payload = reader.GetString(nameof(OutboxMessage.Payload)),
                       PayloadType = reader.GetString(nameof(OutboxMessage.PayloadType)),
                       Stream = reader.GetString(nameof(OutboxMessage.Stream)),
                       Topic = reader.GetString(nameof(OutboxMessage.Topic)),
                   }
               };
               
               messages.Add(wrapper);

               yield return wrapper;
           }

           var parameters = messages.Select((m, i) => new SqlParameter($"@{i}", m.Message.Id));

           var parametersString = string.Join(",", parameters.Select(p => p.ParameterName));
           
           await using var updateCommand = sqlConnection.CreateCommand();
           updateCommand.Transaction = transaction;
           updateCommand.CommandText = "UPDATE [dbo].[OutboxMessage] " +
                                       "SET [PublishedAt] = GETDATE() " +
                                       $"WHERE [Id] IN ({parametersString});";

           await updateCommand.ExecuteNonQueryAsync(cancellationToken);
           
           
           await transaction.CommitAsync(cancellationToken);
        }
    }
}