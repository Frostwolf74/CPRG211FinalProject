namespace CPRG211FinalProject.Components.Utils;

public class InvalidInputException(string message = "Please enter valid characters") : Exception(message)
{
    public new string Message { get; set; } = message;
}