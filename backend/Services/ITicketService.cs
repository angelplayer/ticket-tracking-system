// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using Backend.Domain;

namespace Backend.Services
{
  public interface ITicketService
  {
    Task<List<Ticket>> GetTicketListAsync();
    Task<Ticket> createTicket(string summery, string description, TicketType type);
    Task<Ticket> GetTicketById(string ticketId);
    Task<Ticket> ResolveTicket(string ticketId, bool resolve);
    Task DeleteTicket(string ticketId);
    Task<Ticket> UpdateTicket(string ticketId, string summery, string description);
  }
}
