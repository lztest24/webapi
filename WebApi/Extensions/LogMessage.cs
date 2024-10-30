using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions
{
    public static class LogMessage
    {
        public static string GetLogInfo(this object controller, [CallerMemberName] string method = "")
        {
            return $"{controller}.{method}";
        }
    }
}