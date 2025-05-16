using Contacts.Contracts.Requests.Contact;
using Contacts.Contracts.Responses.Contact;
using System.Linq.Expressions;

namespace Contact.Application.Services.Contracts;

public interface IContactService
{
    Task<ContactResponse> CreateAsync(CreateContactRequest request, CancellationToken token = default);

    Task<ContactResponse?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<ContactsResponse> GetAllAsync(ContactsRequest request, CancellationToken token = default);

    Task<ContactResponse> UpdateAsync(Guid id, UpdateContactRequest request, CancellationToken token = default);

    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);

    Task<int> CountAsync(CancellationToken token = default);

    Task<bool> AnyAsync(Expression<Func<Data.Models.Contact, bool>> predicate, CancellationToken token = default);
}