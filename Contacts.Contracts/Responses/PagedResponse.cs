namespace Contacts.Contracts.Responses;

public class PagedResponse<TResponse>
{
    public required IEnumerable<TResponse> Items { get; set; }

    public required int Page { get; set; }

    public required int PageSize { get; set; }

    public required int TotalItems { get; set; }

    public bool HasNextPage => TotalItems > Page * PageSize;
}