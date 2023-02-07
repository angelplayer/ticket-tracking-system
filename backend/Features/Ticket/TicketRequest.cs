

using Backend.Domain;

namespace Backend.Features.Ticket
{
  public class CreateTicketRequest : IRequestValidator
  {

    public CreateTicketRequest(string summery, string description, TicketType type)
    {
      Summery = summery;
      Description = description;
      Type = type;
    }

    public string Summery { get; set; }
    public string Description { get; set; }
    public TicketType Type { get; set; }

    public bool validate()
    {
      // create request non-blank required summery and description
      return !string.IsNullOrWhiteSpace(Summery) && !string.IsNullOrWhiteSpace(Description);
    }
  }

  public class UpdateTicketRequest : IRequestValidator
  {
    public UpdateTicketRequest(string summery, string description)
    {
      Summery = summery;
      Description = description;
    }

    public string Summery { get; set; }
    public string Description { get; set; }

    public bool validate()
    {
      return !string.IsNullOrWhiteSpace(Summery) && !string.IsNullOrWhiteSpace(Description);
    }
  }

}
