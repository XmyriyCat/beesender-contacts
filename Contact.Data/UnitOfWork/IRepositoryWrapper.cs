using Contact.Data.Repository.Contracts;

namespace Contact.Data.UnitOfWork;

public interface IRepositoryWrapper
{
    IContactRepository Contacts { get; }

    Task SaveChangesAsync(CancellationToken token = default);
}