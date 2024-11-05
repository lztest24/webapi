using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Extensions
{
    public static class LogMessage
    {
        public static string GetLogInfo(this object type, object data = null, [CallerMemberName] string method = "")
        {
            if (data == null)
                return $"{type}.{method}";

            if (data is ModelStateDictionary)
            {
                var mstate = data as ModelStateDictionary;
                return $"{type}.{method}({{{string.Join(',', mstate!.Keys.Select(k => $"{k}:{mstate[k]!.RawValue}"))}}})";
            }

            if (data is string)
            {
                return $"{type}.{method}({{{data}}})";
            }

            return string.Empty;
        }
    }
}