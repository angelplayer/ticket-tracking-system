// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace Backend.Features
{
  public record Replay(bool success, object data = null, string[] error = null);

  public static class ReplyBuilder
  {
    public static Replay Error(string error)
    {
      return new Replay(false, null, new string[] { error });
    }
  }
}
