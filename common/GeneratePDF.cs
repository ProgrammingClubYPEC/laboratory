using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.common
{
    public class GeneratePDF
    {
        private PdfDocument document;
        private List<PdfPage> pages;
        private List<PdfGraphics> graphics;
        private PdfFont font;

        private PdfPage currentPage;
        private PdfGraphics currentGraphics;

        private int position = 0;

        public GeneratePDF()
        {
            document = new PdfDocument();
            pages = new List<PdfPage>();
            graphics = new List<PdfGraphics>();

            pages.Add(document.Pages.Add());
            graphics.Add(pages.First().Graphics);

            currentPage = pages.First();
            currentGraphics = graphics.First();

            font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
        }

        public void WriteTextLine(string text)
        {
            if (document == null)
                return;

            currentGraphics.DrawString(text, font, PdfBrushes.Black, new System.Drawing.PointF(0, position));
            position += 30;
        }

        public void AddNewPage()
        {
            if (document == null)
                return;

            pages.Add(document.Pages.Add());
            currentPage = pages.Last();
            graphics.Add(currentPage.Graphics);
            currentGraphics = graphics.Last();
        }

        public int CountPage()
        {
            return pages.Count;
        }

        public void ChangePage(int index)
        {
            if (index >= 0 && index < pages.Count)
            {
                currentPage = pages[index];
                currentGraphics = currentPage.Graphics;
            }
        }

        public void InsertImage(string path)
        {
            if (document == null || String.IsNullOrEmpty(path) || !File.Exists(path))
                return;

            PdfBitmap bitmap = new PdfBitmap(path);
            currentGraphics.DrawImage(bitmap, 0, position);
            position += bitmap.Height + 10;
        }

        public void Save(string name)
        {
            document.Save($"../../Resources/pdf/{name}.pdf");
            document.Close();
        }

        public static void SaveBarcodePdf(string name, string path)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(path) || !File.Exists(path))
                return;

            PdfDocument pdfDocument = new PdfDocument();
            PdfPage page = pdfDocument.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfBitmap barcode = new PdfBitmap(path);
            graphics.DrawImage(barcode, 0, 0);
            pdfDocument.Save($"../../Resources/pdf/barcode/barcode-{name}.pdf");
            pdfDocument.Close();
        }
    }
}
