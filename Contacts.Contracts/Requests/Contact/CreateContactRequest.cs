namespace Contacts.Contracts.Requests.Contact;

public class CreateContactRequest
{
    public required string Name { get; set; }

    public required string PhoneNumber { get; set; }

    public required string JobTitle { get; set; }

    public required DateOnly BirthDate { get; set; }
}