namespace Clarity.Web.Service.Helpers
{
        public class HttpRequestNotExceptionMessage : HttpRequestExceptionMessage
        {
            public HttpRequestNotExceptionMessage(string message) : base(message, 404) { }
        }
    
}
