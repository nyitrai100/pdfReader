using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace PdfsReader
{
    class Program
    {
        static void Main()
        {
            string? folderPath = null;
            string? keyword = null;
            
            while (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                Console.Write("Enter folder path: ");
                folderPath = Console.ReadLine();

                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("Invalid folder path. Try again.");
                    folderPath = null;
                }
            }
            
            while (string.IsNullOrWhiteSpace(keyword))
            {
                Console.Write("Enter keyword to search: ");
                keyword = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    Console.WriteLine("Keyword cannot be empty. Try again.");
                }
            }

            Console.WriteLine($"\nSearching PDFs in: {folderPath}");
            Console.WriteLine($"Looking for keyword: \"{keyword}\"\n");

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

            Console.WriteLine("\nSearch complete.");
        }
    }
}