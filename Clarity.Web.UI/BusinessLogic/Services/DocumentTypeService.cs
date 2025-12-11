using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using Clarity.Web.UI.Utility;
using Newtonsoft.Json;
using System.Text;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly HttpClient _httpClient;

        public DocumentTypeService(HttpClientService httpClientService)
        {
            _httpClient = httpClientService.GetHttpClient();
        }
        public async Task<List<DocumentType>> FetchAllDocumentTypes()
        {
            var responce = await _httpClient.GetAsync("DocumentType/FetchAllDocumentTypes");

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var contentReqponce = JsonConvert.DeserializeObject<List<DocumentType>>(content);
                return contentReqponce != null ? contentReqponce : new List<DocumentType>();
            }
            return new List<DocumentType>();
        }

        public async Task<bool> InsertOrUpdateDocumentType(DocumentType documentType)
        {
            var inputContent = JsonConvert.SerializeObject(documentType);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("DocumentType/InsertOrUpdateDocumentType", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<bool>(content);
                return responceContent ? responceContent : false;
            }
            return false;
        }
    }
}
