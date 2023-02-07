// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Backend.Configuration
{
  public interface IPasswordHasher
  {
    Task<byte[]> Hash(string passwordHashed, byte[] salt);
    void Dispose();
  }
}
