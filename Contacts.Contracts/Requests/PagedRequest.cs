using Contacts.Contracts.Variables;

namespace Contacts.Contracts.Requests;

public class PagedRequest
{
    public int Page { get; set; } = PagedOptions.DefaultPage;

    public int PageSize { get; set; } = PagedOptions.DefaultPageSize;
}