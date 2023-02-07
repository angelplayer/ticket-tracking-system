// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using Backend.Domain;

namespace Backend.Services
{
  public interface IUserService
  {
    Task<User> GetUserById(string userid);
  }
}
