using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace dotNETAcademyServer.Services
{
    public class PDFGenerator
    {
        public void GeneratePDF(string fileName)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
              new XRect(0, 0, page.Width, page.Height),
              XStringFormats.Center);

            // Save the document...
            document.Save("./Bestellingen/" + fileName);
            document.Close();
        }
    }
}
