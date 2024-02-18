namespace Clarity.Web.Service.Helpers
{ 
        public class HttpRequestExceptionMessage : Exception
        {
            public int StatusCode { get; set; }
            public string Content { get; set; }

            public HttpRequestExceptionMessage(string message,
                int statusCode,
                string content = null) : base(message)
            {
                StatusCode = statusCode;
                Content = content;
            }
        }
    
}
