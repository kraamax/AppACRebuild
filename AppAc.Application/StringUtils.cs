using System;
using System.Collections.Generic;

namespace AppAC.Application
{
    public static class StringUtils
    {
         public static string ToString(List<string> errors)
        {
            var result = String.Join(", ", errors.ToArray());
            return result;
        }
    }
}