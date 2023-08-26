namespace EStore.Domain.Attributes;

public class EventAttribute : Attribute
{
    public EventAttribute(string entityName)
    {
        EntityName = entityName;
    }

    public string EntityName { get; }
}