using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Extensions
{
    public static class RequestDebug
    {
        public static async Task<string> GetRawBodyAsync(this HttpRequest request, bool stripWhitespace = true, Encoding encoding = null)
        {
            if (!request.Body.CanSeek)
                request.EnableBuffering();

            request.Body.Position = 0;
            var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);
            var body = await reader.ReadToEndAsync().ConfigureAwait(false);
            request.Body.Position = 0;
            if (stripWhitespace)
                return System.Text.RegularExpressions.Regex.Replace(body, @"\s", string.Empty);
            else
                return body;
        }
    }
}