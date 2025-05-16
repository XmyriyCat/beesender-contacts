using System.Linq.Expressions;
using Contact.Application.Exceptions;
using Contact.Application.Services.Contracts;
using Contact.Data.UnitOfWork;
using Contacts.Contracts.Requests.Contact;
using Contacts.Contracts.Responses.Contact;
using FluentValidation;
using MapsterMapper;

namespace Contact.Application.Services.Implementations;

public class ContactService : IContactService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateContactRequest> _createValidator;
    private readonly IValidator<UpdateContactRequest> _updateValidator;

    public ContactService(IRepositoryWrapper repositoryWrapper, IMapper mapper,
        IValidator<CreateContactRequest> createValidator, IValidator<UpdateContactRequest> updateValidator)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<ContactResponse> CreateAsync(CreateContactRequest request, CancellationToken token = default)
    {
        await _createValidator.ValidateAndThrowAsync(request, token);

        var contact = _mapper.Map<Data.Models.Contact>(request);

        var createdContact = await _repositoryWrapper.Contacts.AddAsync(contact, token);

        await _repositoryWrapper.SaveChangesAsync(token);

        return _mapper.Map<ContactResponse>(createdContact);
    }

    public async Task<ContactResponse?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var contact = await _repositoryWrapper.Contacts.GetByIdAsync(id, token);

        if (contact is null)
        {
            return null;
        }

        return _mapper.Map<ContactResponse>(contact);
    }

    public async Task<ContactsResponse> GetAllAsync(ContactsRequest request, CancellationToken token = default)
    {
        var contacts = await _repositoryWrapper.Contacts.GetAllAsync(request.Page, request.PageSize);

        var totalCount = await _repositoryWrapper.Contacts.CountAsync(token: token);

        var response = new ContactsResponse
        {
            Page = request.Page,
            PageSize = request.PageSize,
            Items = _mapper.Map<List<ContactResponse>>(contacts),
            TotalItems = totalCount
        };

        return response;
    }

    public async Task<ContactResponse> UpdateAsync(Guid id, UpdateContactRequest request,
        CancellationToken token = default)
    {
        await _updateValidator.ValidateAndThrowAsync(request, token);

        var contact = await _repositoryWrapper.Contacts.GetByIdAsync(id, token);

        if (contact is null)
        {
            throw new ContactNotFoundException($"Contact with ID {id} is not found.");
        }

        _mapper.Map(request, contact);

        var updatedContact = _repositoryWrapper.Contacts.Update(contact);
        
        await _repositoryWrapper.SaveChangesAsync(token);

        var response = _mapper.Map<ContactResponse>(updatedContact);

        return response;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        var contact = await _repositoryWrapper.Contacts.GetByIdAsync(id, token);

        if (contact is null)
        {
            throw new ContactNotFoundException($"Contact with ID {id} is not found.");
        }

        var deletionResult = _repositoryWrapper.Contacts.Delete(contact);
        
        await _repositoryWrapper.SaveChangesAsync(token);

        return deletionResult;
    }

    public async Task<int> CountAsync(CancellationToken token = default)
    {
        return await _repositoryWrapper.Contacts.CountAsync(token: token);
    }

    public async Task<bool> AnyAsync(Expression<Func<Data.Models.Contact, bool>> predicate, CancellationToken token = default)
    {
        return await _repositoryWrapper.Contacts.AnyAsync(predicate, token);
    }
}