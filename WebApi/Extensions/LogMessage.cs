using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions
{
    public static class LogMessage
    {
        public static string GetLogInfo(this object type, [CallerMemberName] string method = "")
        {
            return $"{type}.{method}";
        }
    }
}