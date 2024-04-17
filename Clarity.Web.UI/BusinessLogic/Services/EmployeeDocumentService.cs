using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class EmployeeDocumentService : IEmployeeDocumentService
    {
        private readonly HttpClient _httpClient;

        public EmployeeDocumentService(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }
        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync()
        {
            var responce = await _httpClient.GetAsync("EmployeeDocument/FetchEmployeeDocumentsAsync");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeDocument>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeDocument>();
            }
            return new List<EmployeeDocument>();
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long Id)
        {
            var requestuRL = Path.Combine("EmployeeDocument/FetchEmployeeDocumentsAsync", Id.ToString());

            var responce = await _httpClient.GetAsync(requestuRL);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeDocument>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeDocument>();
            }
            return new List<EmployeeDocument>();
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(string documentType)
        {
            var requestuRL = Path.Combine("EmployeeDocument/ByDocumentType", documentType);

            var responce = await _httpClient.GetAsync(requestuRL);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeDocument>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeDocument>();
            }
            return new List<EmployeeDocument>();
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long documentType, bool isActive)
        {
            var requestuRL = Path.Combine("EmployeeDocument/ByDocumentType", documentType.ToString() + "/" + isActive);

            var responce = await _httpClient.GetAsync(requestuRL);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeDocument>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeDocument>();
            }
            return new List<EmployeeDocument>();
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long employeeId, long documentType)
        {
            var requestuRL = Path.Combine("EmployeeDocument/ByEmployeeAndDocumentType", employeeId.ToString() + "/" + documentType.ToString());

            var responce = await _httpClient.GetAsync(requestuRL);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeDocument>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeDocument>();
            }
            return new List<EmployeeDocument>();
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long employeeId, string documentType)
        {
            var requestuRL = Path.Combine("EmployeeDocument/ByEmployeeAndDocumentType", employeeId.ToString() + "/" + documentType);

            var responce = await _httpClient.GetAsync(requestuRL);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeDocument>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeDocument>();
            }
            return new List<EmployeeDocument>();
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(string documentType, long userId)
        {
            var requestuRL = Path.Combine("EmployeeDocument/ByDocumentTypeAndUser", documentType.ToString() + "/" + userId.ToString());

            var responce = await _httpClient.GetAsync(requestuRL);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeDocument>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeDocument>();
            }
            return new List<EmployeeDocument>();
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(string documentType, string email)
        {
            var requestuRL = Path.Combine("EmployeeDocument/ByDocumentTypeAndEmail", documentType.ToString() + "/" + email.ToString());

            var responce = await _httpClient.GetAsync(requestuRL);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<EmployeeDocument>>(content);
                return contentReqponce != null ? contentReqponce : new List<EmployeeDocument>();
            }
            return new List<EmployeeDocument>();
        }

        public async Task<bool> InsertOrUpdateEmployeeDocument(EmployeeDocument employeeDocument)
        {
            var inputContent = JsonConvert.SerializeObject(employeeDocument);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("EmployeeDocument/InsertOrUpdateEmployeeDocument", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }

        public async Task<bool> RemoveEmployeeDocument(long employeeDocumentId)
        {
            var requestuRL = Path.Combine("EmployeeDocument", employeeDocumentId.ToString());

            var responce = await _httpClient.DeleteAsync(requestuRL);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<bool>(content);
                return contentReqponce ? contentReqponce : false;
            }
            return false;
        }
    }
}
