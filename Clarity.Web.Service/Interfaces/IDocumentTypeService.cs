using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IDocumentTypeService
    {
        Task<bool> InsertOrUpdateDocumentType(DocumentType documentType);
        Task<List<DocumentType>> FetchAllDocumentTypes();
    }
}
