// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Backend.Domain
{

  public enum TicketType
  {
    Bug = 0, FeatureRequest, TestCase
  }

  public class Ticket
  {
    public Guid Id { get; set; }
    public string Summery { get; set; }
    public string Description { get; set; }
    public TicketType TicketType { get; set; }
    public bool Resolved { get; set; }
  }
}
