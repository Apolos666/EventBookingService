namespace BuildingBlocks.Exceptions;

public class InvalidOperationException : Exception
{
    public InvalidOperationException(string message) : base(message)
    {
    }

    public InvalidOperationException(string entityName, string operation, string reason)
        : base($"Operation \"{operation}\" on entity \"{entityName}\" failed: {reason}.")
    {
    }
}