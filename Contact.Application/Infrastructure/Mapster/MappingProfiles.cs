using Contacts.Contracts.Requests.Contact;
using Contacts.Contracts.Responses.Contact;
using Mapster;

namespace Contact.Application.Infrastructure.Mapster;

public static class MappingProfiles
{
    public static void Configure()
    {
        TypeAdapterConfig<CreateContactRequest, Data.Models.Contact>.NewConfig();

        TypeAdapterConfig<UpdateContactRequest, Data.Models.Contact>.NewConfig();

        TypeAdapterConfig<Data.Models.Contact, ContactResponse>.NewConfig();

        TypeAdapterConfig<IEnumerable<Data.Models.Contact>, ContactsResponse>.NewConfig()
            .Map(dest => dest.Items,
                src => src.Select(x => x.Adapt<Data.Models.Contact, ContactResponse>()));
    }
}