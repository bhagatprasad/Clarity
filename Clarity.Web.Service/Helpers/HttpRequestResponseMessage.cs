namespace Clarity.Web.Service.Helpers
{
        public class HttpRequestResponseMessage<T>

        {
            public bool Success { get; set; }
            public T Data { get; set; }
            public string ErrorMessage { get; set; }

            public HttpRequestResponseMessage(T data,
                bool success = true,
                string errorMessage = null)
            {
                Success = success;
                Data = data;
                ErrorMessage = errorMessage;
            }
        }
    
}
