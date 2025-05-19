using Contact.Application.Services.Contracts;
using Contacts.Contracts.Requests.Contact;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Api.Controllers;

[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet(ApiEndpoints.Contact.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] ContactsRequest request, CancellationToken token)
    {
        var response = await _contactService.GetAllAsync(request, token);

        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Contact.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
    {
        var response = await _contactService.GetByIdAsync(id, token);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost(ApiEndpoints.Contact.Create)]
    public async Task<IActionResult> Create([FromBody] CreateContactRequest request, CancellationToken token)
    {
        var response = await _contactService.CreateAsync(request, token);

        return CreatedAtAction(nameof(Get), new { Id = response.Id }, response);
    }

    [HttpPut(ApiEndpoints.Contact.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateContactRequest request,
        CancellationToken token)
    {
        var response = await _contactService.UpdateAsync(id, request, token);

        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Contact.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
    {
        await _contactService.DeleteByIdAsync(id, token);

        return Ok();
    }
}