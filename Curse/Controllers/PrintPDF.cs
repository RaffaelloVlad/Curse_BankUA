using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;

namespace Curse.Controllers
{
    public static class PrintPDF
    {
        public static void GeneratePdf(DataTable table, string fileName)
        {

            // Создаем документ
            Document document = new Document();

            // Создаем объект PdfWriter для записи в файл
            PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

            // Открываем документ
            document.Open();

            // Создаем таблицу для вывода данных
            PdfPTable pdfTable = new PdfPTable(table.Columns.Count);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            // Устанавливаем шрифт
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new Font(baseFont, 10, Font.NORMAL, BaseColor.BLACK);

            // Добавляем заголовки столбцов
            foreach (DataColumn column in table.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, font));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            // Добавляем данные
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    pdfTable.AddCell(new Phrase(row[column].ToString(), font));
                }
            }

            // Добавляем таблицу в документ
            document.Add(pdfTable);

            // Закрываем документ
            document.Close();

        }
    }
}
