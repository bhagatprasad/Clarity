using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Helpers;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class EmployeeDocumentService : IEmployeeDocumentService
    {
        private readonly ApplicationDBContext context;

        public EmployeeDocumentService(ApplicationDBContext dbcontext)
        {
            this.context = dbcontext;
        }
        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync()
        {
            return await fetchEmployeeDocumentsAsync(EmployeeDocumentEnum.All);
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long Id)
        {
            return await fetchEmployeeDocumentsAsync(EmployeeDocumentEnum.DocumentId, 0, 0, 0, Id, false, "", "");
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long employeeId, long documentType)
        {
            return await fetchEmployeeDocumentsAsync(EmployeeDocumentEnum.EmployeeLongDocumentTypeLong, 0, employeeId, documentType, 0, false, "", "");
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long employeeId, string documentType)
        {
            return await fetchEmployeeDocumentsAsync(EmployeeDocumentEnum.EmployeeIdLongDocumentString, 0, employeeId, 0, 0, false, "", documentType);
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(string documentType, long userId)
        {
            return await fetchEmployeeDocumentsAsync(EmployeeDocumentEnum.DocumentTypeStringUserIdLong, userId, 0, 0, 0, false, "", documentType);
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(string documentType, string email)
        {
            return await fetchEmployeeDocumentsAsync(EmployeeDocumentEnum.DocumentTypeStringEmial, 0, 0, 0, 0, false, email, documentType);
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(string documentType)
        {
            return await fetchEmployeeDocumentsAsync(EmployeeDocumentEnum.DocumentTypeString, 0, 0, 0, 0, false, "", documentType);
        }

        public async Task<List<EmployeeDocument>> FetchEmployeeDocumentsAsync(long documentType, bool isActive)
        {
            return await fetchEmployeeDocumentsAsync(EmployeeDocumentEnum.ActiveDocumentType, 0, 0, documentType, 0, true, "", "");
        }

        public async Task<bool> InsertOrUpdateEmployeeDocument(EmployeeDocument employeeDocument)
        {
            if (employeeDocument.Id > 0)
            {
                var document = await context.employeeDocuments.FindAsync(employeeDocument.Id);
                if (document != null)
                {
                    document.DocumentName = employeeDocument.DocumentName;
                    document.DocumentPath = employeeDocument.DocumentPath;
                    document.DocumentExtension = employeeDocument.DocumentExtension;
                    document.DocumentTypeId = employeeDocument.DocumentTypeId;
                    document.ModifiedBy = employeeDocument.ModifiedBy;
                    document.ModifiedOn = employeeDocument.ModifiedOn;
                }

            }
            else
            {
                await context.employeeDocuments.AddAsync(employeeDocument);
            }

            var responce = await context.SaveChangesAsync();

            return responce == 1 ? true : false;
        }

        public async Task<bool> RemoveEmployeeDocument(long employeeDocumentId)
        {
            var document = await context.employeeDocuments.FindAsync(employeeDocumentId);

            if (document != null)
            {
                context.employeeDocuments.Remove(document);
            }
            var responce = await context.SaveChangesAsync();

            return responce == 1 ? true : false;
        }


        private async Task<List<EmployeeDocument>> fetchEmployeeDocumentsAsync(EmployeeDocumentEnum employeeDocument, long userId = 0, long employeeId = 0, long documentTypeId = 0, long documentId = 0, bool isactive = false, string email = "", string documentType = "")
        {

            var employeedocuments = await context.employeeDocuments.ToListAsync();

            switch (employeeDocument)
            {
                case EmployeeDocumentEnum.ActiveDocumentType:
                    var _documenttype = await context.documentTypes.Where(x => x.Name.ToLower().Trim() == documentType.ToLower().Trim()).FirstOrDefaultAsync();
                    employeedocuments = employeedocuments.Where(x => x.IsActive == true && x.DocumentTypeId == _documenttype?.Id).ToList();
                    break;

                case EmployeeDocumentEnum.DocumentTypeString:
                    employeedocuments = employeedocuments.Where(x => x.DocumentTypeId == documentTypeId).ToList();
                    break;

                case EmployeeDocumentEnum.DocumentId:
                    employeedocuments = employeedocuments.Where(x => x.Id == documentId).ToList();
                    break;

                case EmployeeDocumentEnum.DocumentTypeStringUserIdLong:
                    var documenttype = await context.documentTypes.Where(x => x.Name.ToLower().Trim() == documentType.ToLower().Trim()).FirstOrDefaultAsync();
                    var user = await context.users.FirstOrDefaultAsync(x => x.Id == userId);
                    employeedocuments = employeedocuments.Where(x => x.DocumentTypeId == documenttype?.Id && x.EmployeeId == user?.EmployeeId).ToList();
                    break;

                case EmployeeDocumentEnum.DocumentTypeStringEmial:
                    var docType = await context.documentTypes.Where(x => x.Name.ToLower().Trim() == documentType.ToLower().Trim()).FirstOrDefaultAsync();
                    var _user = await context.users.FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == email.ToLower().Trim());
                    employeedocuments = employeedocuments.Where(x => x.DocumentTypeId == docType?.Id && x.EmployeeId == _user?.EmployeeId).ToList();
                    break;

                case EmployeeDocumentEnum.EmployeeIdLongDocumentString:
                    var _docType = await context.documentTypes.Where(x => x.Name.ToLower().Trim() == documentType.ToLower().Trim()).FirstOrDefaultAsync();
                    employeedocuments = employeedocuments.Where(x => x.DocumentTypeId == _docType?.Id && x.EmployeeId == employeeId).ToList();
                    break;
                case EmployeeDocumentEnum.EmployeeLongDocumentTypeLong:
                    employeedocuments = employeedocuments.Where(x => x.DocumentTypeId == documentTypeId && x.EmployeeId == employeeId).ToList();
                    break;
            }

            return employeedocuments;
        }
    }
}
