namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IDocumentService
    {
        byte[] GeneratePdfFromString();

        byte[] GeneratePdfFromRazorView<TModel>(string viewPath, TModel model);
    }
}
