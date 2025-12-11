using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IDocumentTypeService
    {
        Task<bool> InsertOrUpdateDocumentType(DocumentType documentType);
        Task<List<DocumentType>> FetchAllDocumentTypes();
    }
}
