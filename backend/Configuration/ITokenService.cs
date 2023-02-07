// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Backend.Configuration
{
  public interface ITokenService
  {
    string CreateToken(string username, params string[] scopes);
  }
}
