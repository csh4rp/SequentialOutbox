namespace SequentialOutbox.Application.Commands;

public record MarkOrderAsPaidCommand(long OrderId);