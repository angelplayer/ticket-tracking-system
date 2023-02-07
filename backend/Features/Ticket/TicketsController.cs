

using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.Ticket
{

  [Route("api/[Controller]")]
  [ApiController]
  [Authorize]
  public class TicketsController : ControllerBase
  {

    // GET: /api/tickets
    [HttpGet]
    public async Task<IActionResult> ListTicket()
    {

      return Ok(await ticketService.GetTicketListAsync());
    }

    // GET: /api/tickets/id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById([FromRoute] string id)
    {
      return Ok(await ticketService.GetTicketById(id));
    }

    // POST: /api/tickets
    [HttpPost]
    [Authorize(Policy = "CreateTicketPolicy")]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest request)
    {
      // validate request body requirement			
      if (!request.validate())
      {
        return BadRequest();
      }

      return Ok(await ticketService.createTicket(
        request.Summery,
        request.Description,
        request.Type
      ));
    }

    // PUT: /api/ticket/1
    [HttpPut("{id}")]
    [Authorize(Policy = "ModifyTicketPolicy")]
    public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketRequest request, [FromRoute] string id)
    {
      var ticket = await ticketService.GetTicketById(id);

      // validate request body requirement			
      if (!request.validate())
      {
        return BadRequest();
      }

      return Ok(await ticketService.UpdateTicket(ticket.Id.ToString(), request.Summery, request.Description));
    }

    // PATCH: /api/ticket/1/resolve
    [HttpPatch("{id}/resolve")]
    [Authorize(Policy = "ResolveTicketPolicy")]
    public async Task<IActionResult> ResolveTicket([FromRoute] string id)
    {

      return Ok(await ticketService.ResolveTicket(id, true));
    }

    // DELTE: /api/ticket/1
    [HttpDelete("{id}")]
    [Authorize(Policy = "DeleteTicketPolicy")]
    public async Task<IActionResult> DeleteTicke([FromRoute] string id)
    {
      var ticket = await ticketService.GetTicketById(id);

      await ticketService.DeleteTicket(ticket.Id.ToString());
      return Ok();
    }


    public TicketsController(ITicketService ticketService, IAuthorizationService authorization)
    {
      this.ticketService = ticketService;
      this.authorization = authorization;
    }

    private readonly ITicketService ticketService;
    private readonly IAuthorizationService authorization;

  }

}
