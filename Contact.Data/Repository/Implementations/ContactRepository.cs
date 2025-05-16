using Contact.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Contact.Data.Repository.Implementations;

public class ContactRepository : GenericRepository<Models.Contact>, IContactRepository
{
    public ContactRepository(ContactDataContext context) : base(context)
    {
    }
}