namespace OpenCloud.Extensions
{
    internal static class HttpRequestExceptionExtensions
    {
        public static bool CouldBeOAuthFail(this HttpRequestException ex)
            => ex.StatusCode == System.Net.HttpStatusCode.BadRequest || ex.StatusCode == System.Net.HttpStatusCode.Unauthorized;
    }
}
