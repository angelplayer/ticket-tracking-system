

using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
  public class TicketService : ITicketService
  {

    public async Task<List<Ticket>> GetTicketListAsync()
    {
      return await context.Tickets.ToListAsync();
    }

    public async Task<Ticket> GetTicketById(string ticketId)
    {

      var ticket = await this.context.Tickets
      .SingleOrDefaultAsync(ticket => ticket.Id.ToString().SequenceEqual(ticketId));

      return ticket ?? throw new DomainException("ticket_not_found", DomainExceptionCode.NotFound);
    }

    public async Task<Ticket> ResolveTicket(string ticketId, bool resolve)
    {
      var ticket = await GetTicketById(ticketId);
      ticket.Resolved = resolve;
      await context.SaveChangesAsync();

      return ticket;
    }

    public async Task DeleteTicket(string ticketId)
    {
      var ticket = await GetTicketById(ticketId);
      context.Tickets.Remove(ticket);
      await context.SaveChangesAsync();

    }

    public async Task<Ticket> UpdateTicket(string ticketId, string summery, string description)
    {
			var ticket = await GetTicketById(ticketId);
			ticket.Summery = summery;
			ticket.Description = description;
			context.Update(ticket);
			await context.SaveChangesAsync();

			return ticket;
    }

    public async Task<Ticket> createTicket(string summery, string description, TicketType type)
    {
      var ticket = new Ticket()
      {
        Description = description,
        Resolved = false,
        Summery = summery,
        TicketType = type
      };

      await context.Tickets.AddAsync(ticket);
      await context.SaveChangesAsync();

      return await GetTicketById(ticket.Id.ToString());
    }

    public TicketService(TTSContext context)
    {
      this.context = context;
    }

    private readonly TTSContext context;
  }
}