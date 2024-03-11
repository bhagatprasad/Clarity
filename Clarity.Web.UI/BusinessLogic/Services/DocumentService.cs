using Clarity.Web.UI.BusinessLogic.Interfaces;
using Clarity.Web.UI.Models;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Clarity.Web.UI.BusinessLogic.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IConverter _converter;
        private readonly IRazorRendererHelper _razorRendererHelper;
        public DocumentService(
                  IConverter converter,
                  IRazorRendererHelper razorRendererHelper)
        {
            _converter = converter;
            _razorRendererHelper = razorRendererHelper;
        }
        public byte[] GeneratePdfFromRazorView<TModel>(string viewPath, TModel model)
        {
            var htmlContent = _razorRendererHelper.RenderPartialToString(viewPath, model);
            return GeneratePdf(htmlContent);
        }

        public byte[] GeneratePdfFromString()
        {
            var htmlContent = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <style>
                p{{
                    width: 80%;
                }}
                </style>
            </head>
            <body>
                <h1>Some heading</h1>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            </body>
            </html>
            ";

            return GeneratePdf(htmlContent);
        }
        private byte[] GeneratePdf(string htmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 18, Bottom = 18 },
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" }
            };

            var htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return _converter.Convert(htmlToPdfDocument);
        }
        private InvoiceVM GetInvoiceModel()
        {
            var invoiceViewModel = new InvoiceVM
            {
                OrderDate = DateTime.Now,
                OrderId = 1234567890,
                DeliveryDate = DateTime.Now.AddDays(10),
                Products = new List<Product>()
                {
                    new Product
                    {
                        ItemName = "Hosting (12 months)",
                        Price = 200
                    },
                    new Product
                    {
                        ItemName = "Domain name (1 year)",
                        Price = 12
                    },
                    new Product
                    {
                        ItemName = "Website design",
                        Price = 1000

                    },
                    new Product
                    {
                        ItemName = "Maintenance",
                        Price = 300
                    },
                    new Product
                    {
                        ItemName = "Customization",
                        Price = 400
                    },
                }
            };

            invoiceViewModel.TotalAmount = invoiceViewModel.Products.Sum(p => p.Price);

            return invoiceViewModel;
        }

    }
}
