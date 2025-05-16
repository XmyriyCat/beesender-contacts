namespace Contacts.Contracts.Responses.Contact;

public class ContactResponse
{
    public required Guid Id { get; set; }
    
    public required string Name { get; set; }

    public required string PhoneNumber { get; set; }

    public required string JobTitle { get; set; }

    public required DateOnly BirthDate { get; set; }
}