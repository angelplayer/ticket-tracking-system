// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Backend.Domain
{
  public enum UserType { ADMIN = 0, PM, RD, QA };

  public class User
  {
    public Guid Id { get; set; }
    public string Username { get; set; }
    public UserType UserType { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] Salt { get; set; }
  }
}
