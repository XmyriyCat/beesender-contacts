using Contact.Data.Repository.Contracts;

namespace Contact.Data.UnitOfWork;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly ContactDataContext _dbContext;

    public RepositoryWrapper(ContactDataContext dbContext, IContactRepository contacts)
    {
        _dbContext = dbContext;
        Contacts = contacts;
    }

    public IContactRepository Contacts { get; }

    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await _dbContext.SaveChangesAsync(token);
    }
}