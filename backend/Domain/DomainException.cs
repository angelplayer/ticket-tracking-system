// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Backend.Domain
{
  public enum DomainExceptionCode : int
  {
    NotFound = 404, // Follow HTTP code
  }

  public class DomainException : Exception
  {
    public int Code { get; private set; }

    public DomainException(string error, DomainExceptionCode code) : base(error)
    {
      Code = (int)code;
    }
  }

}
