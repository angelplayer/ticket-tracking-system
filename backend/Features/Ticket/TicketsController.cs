
using Backend.Domain;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.Ticket
{

	public record CreateTicketRequest(string summery, string description, TicketType type);
	public record UpdateTicketRequest(string summery, string description);


	[Route("api/[Controller]")]
	[ApiController]
	public class TicketsController : ControllerBase
	{

		// GET: /api/tickets
		[HttpGet]
		public  async Task<IActionResult> ListTicket() {

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
		public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest request)
		{
			return Ok(await ticketService.createTicket(
				request.summery,
				request.description,
				request.type
			));
		}

		// PUT: /api/ticket/1
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketRequest request, [FromRoute] string id)
		{
			return Ok(await ticketService.UpdateTicket(id, request.summery, request.description));
		}

		// PATCH: /api/ticket/1/resolve
		[HttpPatch("{id}/resolve")]
		public async Task<IActionResult> ResolveTicket([FromRoute] string id) {

			return Ok(await ticketService.ResolveTicket(id, true));
		}

		// DELTE: /api/ticket/1
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTicke([FromRoute] string id) {
			await ticketService.DeleteTicket(id);
			return Ok();
		}


		public TicketsController(ITicketService ticketService)
		{
			this.ticketService = ticketService;
		}

		readonly ITicketService ticketService;

	}

}