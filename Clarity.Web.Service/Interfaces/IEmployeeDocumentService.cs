using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IEmployeeDocumentService
    {
        Task<bool> InsertOrUpdateEmployeeDocument(EmployeeDocument employeeDocument);
        Task<bool> RemoveEmployeeDocument(long employeeDocumentId);
        Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync();
        Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long Id);
        Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(string documentType);
        Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long documentType,bool isActive);
        Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long employeeId,long documentType);
        Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long employeeId, string documentType);
        Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(string documentType, long userId);
        Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(string documentType, string  email);
    }
}
