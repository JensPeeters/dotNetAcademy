using Business_layer.DTO;
using Data_layer.Model;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.IO;

namespace dotNETAcademyServer.Services
{
    public class PDFGenerator
    {
        private Document document;
        private TextFrame generalFrame;
        private Table table;
        PdfDocumentRenderer renderer;

        private Color TableBorder = Colors.Black;
        private Color TableGray = Colors.LightGray;
        private BestellingDTO bestelling;
        private MemoryStream stream;

        public void GeneratePDF(BestellingDTO bestelling)
        {
            this.bestelling = bestelling;
            // Create a new MigraDoc document
            CreateDocument();
            DefineStyles();
            CreatePage();
            FillContent();
            // Render MigraDoc document
            this.renderer = new PdfDocumentRenderer();
            renderer.Document = document;
            renderer.RenderDocument();
            // Create memorystream
            stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false);

            ///Wanner je de factuur ergens fysiek wilt opslaan
            //renderer.PdfDocument.Save("./Bestellingen/factuur" + bestelling.Id + ".pdf");
        }

        public MemoryStream GetStream()
        {
            return stream;
        }

        private void CreateDocument()
        {
            this.document = new Document();
            this.document.Info.Title = "Bestelling";
            this.document.Info.Subject = "Alle informatie van de bestelling";
            this.document.Info.Author = "dotNetAcadamy";
        }
        private void DefineStyles()
        {
            //Normal style
            Style style = this.document.Styles["Normal"];
            style.Font.Name = "Verdana";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            //Create new style called Table
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            //Create new style called Reference
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }
        private void CreatePage()
        {
            //Each MigraDoc document needs at least one section.
            Section section = this.document.AddSection();

            // Put a logo in the header
            Image image = section.Headers.Primary.AddImage("./Images/Logo.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Left;
            image.WrapFormat.Style = WrapStyle.Through;

            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("© dotnetAcademy Inc");
            paragraph.Format.Font.Size = 9; 
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the general information
            this.generalFrame = section.AddTextFrame();
            this.generalFrame.Height = "3.0cm";
            this.generalFrame.Width = "5.0cm";
            this.generalFrame.Left = ShapePosition.Right;
            this.generalFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.generalFrame.Top = "1.75cm";
            this.generalFrame.RelativeVertical = RelativeVertical.Page;

            // Add the title on top of the table
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "3cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Bestelling", TextFormat.Bold);

            // Create the bestelling table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            Column column = this.table.AddColumn("7cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            Column column1 = this.table.AddColumn("1.5cm");
            column1.Format.Alignment = ParagraphAlignment.Left;

            Column column2 = this.table.AddColumn("2.5cm");
            column2.Format.Alignment = ParagraphAlignment.Left;

            Column column3 = this.table.AddColumn("1.5cm");
            column3.Format.Alignment = ParagraphAlignment.Left;

            Column column4 = this.table.AddColumn("2.5cm");
            column4.Format.Alignment = ParagraphAlignment.Left;

            // Create head of table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = false;
            row.Shading.Color = TableGray;
            row.TopPadding = 3;
            row.BottomPadding = 3;

            row.Cells[0].AddParagraph("Omschrijving");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("Aantal");
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph("Prijs/stuk");
            row.Cells[2].Format.Font.Bold = true;
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph("Btw %");
            row.Cells[3].Format.Font.Bold = true;
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph("Bedrag");
            row.Cells[4].Format.Font.Bold = true;
            row.Cells[4].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;

            //this.table.SetEdge(0, 0, 5, 2, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }

        private void FillContent()
        {
            // Fill general info in the general info text frame
            Paragraph paragraph = this.generalFrame.AddParagraph();
            //paragraph.AddText("Naam: " + "Iemand");
            //paragraph.AddLineBreak();
            paragraph.AddText("Factuurdatum: ");
            paragraph.AddDateField("dd.MM.yyyy");
            paragraph.AddLineBreak();
            paragraph.AddText("Factuurnummer: " + this.bestelling.Id); //this.fileName.Substring(7,this.fileName.Length-11)

            // Iterate the invoice items
            foreach (BestellingItem product in bestelling.Producten)
            {
                Row row1 = this.table.AddRow();
                row1.TopPadding = 3;
                row1.Cells[0].AddParagraph(product.Product.Titel);
                row1.Cells[1].AddParagraph(product.Aantal.ToString());
                row1.Cells[2].AddParagraph("€ " + product.Product.Prijs.ToString("0.00"));
                row1.Cells[3].AddParagraph("21%");
                row1.Cells[4].AddParagraph("€ " + (product.Aantal * product.Product.Prijs).ToString("0.00"));
                row1.BottomPadding = 3;

                //this.table.SetEdge(0, this.table.Rows.Count - 2, 6, 2, Edge.Box, BorderStyle.Single, 0.75);
            }

            // Add an invisible row as a space line to the table
            Row row = this.table.AddRow();
            row.Borders.Visible = false;


            // Add basisbedrag row
            double btw = bestelling.TotaalPrijs / 121 * 21;
            row = this.table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Basisbedrag:");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 3;
            row.Cells[4].AddParagraph("€ " + (bestelling.TotaalPrijs - btw).ToString("0.00"));
            row.TopPadding = 3;
            row.BottomPadding = 3;

            // Add btwbedag row
            row = this.table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("21% btw:");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 3;
            row.Cells[4].AddParagraph("€ " + btw.ToString("0.00"));
            row.TopPadding = 3;
            row.BottomPadding = 3;

            // Add the total price row
            row = this.table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Totaalbedrag:");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[0].MergeRight = 3;
            row.Cells[4].AddParagraph("€ " + bestelling.TotaalPrijs.ToString("0.00"));
            row.Cells[4].Format.Font.Bold = true;
            row.TopPadding = 3;
            row.BottomPadding = 3;



            // Set the borders of the specified cell range
            //this.table.SetEdge(5, this.table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);
        }
    }
}
