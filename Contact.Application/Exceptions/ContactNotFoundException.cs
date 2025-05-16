namespace Contact.Application.Exceptions;

public class ContactNotFoundException : Exception
{
    public ContactNotFoundException(string message) : base(message)
    {
    }
}