using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly ApplicationDBContext context;

        public DocumentTypeService(ApplicationDBContext dbcontext)
        {
            this.context = dbcontext;
        }
        public async Task<List<DocumentType>> FetchAllDocumentTypes()
        {
            return await context.documentTypes.ToListAsync();
        }

        public async Task<bool> InsertOrUpdateDocumentType(DocumentType documentType)
        {
            if (documentType.Id > 0)
            {
                var document = await context.documentTypes.FindAsync(documentType.Id);
                if (document != null)
                {
                    document.Name = documentType.Name;
                    document.ModifiedOn = documentType.ModifiedOn;
                    document.ModifiedBy = documentType.ModifiedBy;
                }
            }
            else
            {
                await context.documentTypes.AddAsync(documentType);
            }
            var responce = await context.SaveChangesAsync();

            return responce == 1 ? true : false;
        }
    }
}