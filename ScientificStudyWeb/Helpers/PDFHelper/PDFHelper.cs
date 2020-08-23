using System.IO;
using DinkToPdf;

namespace ScientificStudyWeb.Helpers.PDFHelper
{
    public static class PDFHelper
    {
        public static GlobalSettings SetGlobalSettings(string documentTitle)
        {
            return new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = documentTitle
            };
        }

        public static ObjectSettings SetObjectSettings()
        {
            return new ObjectSettings
            {
                PagesCount = true,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "PDFHelper", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9 },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Page [page] of [toPage]" }
            };
        }
    }
}