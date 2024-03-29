﻿using Components.Services;
using DataAccess.Data;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BoyutBackend.Middleware
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private ApiLogService _apiLogService;

        public ApiLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ApiLogService apiLogService)
        {
            try
            {
                _apiLogService = apiLogService;

                var request = httpContext.Request;
                if (request.Path.StartsWithSegments(new PathString("/api")))
                {
                    var stopWatch = Stopwatch.StartNew();
                    var requestTime = DateTime.UtcNow;
                    var requestBodyContent = await ReadRequestBody(request);
                    var originalBodyStream = httpContext.Response.Body;
                    using var responseBody = new MemoryStream();
                    var response = httpContext.Response;
                    response.Body = responseBody;
                    await _next(httpContext);
                    stopWatch.Stop();

                    string responseBodyContent = null;
                    responseBodyContent = await ReadResponseBody(response);
                    await responseBody.CopyToAsync(originalBodyStream);

                    await SafeLog(requestTime,
                        stopWatch.ElapsedMilliseconds,
                        response.StatusCode,
                        ReadUserId(request.Headers["Authorization"]),
                        request.Method,
                        request.Path,
                        request.QueryString.ToString(),
                        request.Headers["User-Agent"],
                        requestBodyContent,
                        responseBodyContent);
                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (Exception)
            {
                await _next(httpContext);
            }
        }

        private static Guid ReadUserId(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                return Guid.Empty;
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(token.Split().Last());
                var userId = tokenS.Claims.First(claim => claim.Type == "userID").Value;

                return Guid.Parse(userId);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        private static async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private async Task SafeLog(DateTime requestTime,
                            long responseMillis,
                            int statusCode,
                            Guid userID,
                            string method,
                            string path,
                            string queryString,
                            string userAgent,
                            string requestBody,
                            string responseBody)
        {
            if (path.ToLower().StartsWith("/api/authentication/login"))
            {
                requestBody = "(Request logging disabled for /api/authentication/login)";
                responseBody = "(Response logging disabled for /api/authentication/login)";
            }

            if (method == "GET")
            {
                requestBody = "";
            }

            await _apiLogService.Log(new SysApiLogging
            {
                RequestTime = requestTime,
                ResponseMillis = responseMillis,
                StatusCode = statusCode,
                UserId = userID,
                Method = method,
                Path = path,
                QueryString = queryString,
                UserAgent = userAgent,
                RequestBody = requestBody,
                ResponseBody = responseBody
            });
        }
    }
}

