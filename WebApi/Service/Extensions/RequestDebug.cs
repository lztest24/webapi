using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Nodes;

namespace WebApi.Extensions
{
    public static class RequestDebug
    {
        public static async Task<string> GetRawBodyAsync(this HttpRequest request, bool stripWhitespace = true, Encoding encoding = null)
        {
            if (request?.Body == null)
                return string.Empty;

            if (!request.Body.CanSeek)
                request.EnableBuffering();

            request.Body.Position = 0;
            var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);
            var body = await reader.ReadToEndAsync().ConfigureAwait(false);
            request.Body.Position = 0;
            if (stripWhitespace)
            {
                try
                {
                    return JsonSerializer.Serialize(JsonNode.Parse(body));
                }
                catch
                {
                    return body;
                }
            }
            else
                return body;
        }
    }
}