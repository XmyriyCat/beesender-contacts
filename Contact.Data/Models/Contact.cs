namespace Contact.Data.Models;

public class Contact
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string JobTitle { get; set; }

    public DateOnly BirthDate { get; set; }
}