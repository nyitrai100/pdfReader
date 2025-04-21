using System;
using System.IO;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

class Program
{
    static void Main()
    {
        Console.Write("Enter folder path: ");
        string folderPath = Console.ReadLine();

        Console.Write("Enter keyword to search: ");
        string keyword = Console.ReadLine();

        var pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

        foreach (var file in pdfFiles)
        {
            using (PdfDocument document = PdfDocument.Open(file))
            {
                string text = "";
                foreach (Page page in document.GetPages())
                {
                    text += page.Text;
                }

                if (text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }
            }
        }

        Console.WriteLine("Search complete.");
    }
}