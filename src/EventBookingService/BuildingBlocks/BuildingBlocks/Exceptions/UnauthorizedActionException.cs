namespace BuildingBlocks.Exceptions;

public class UnauthorizedActionException : Exception
{
    public UnauthorizedActionException(string message = "Unauthorized action.") : base(message)
    {
    }

    protected UnauthorizedActionException(string action, string userId)
        : base($"User with ID {userId} is not authorized to perform the action: {action}.")
    {
    }
}
