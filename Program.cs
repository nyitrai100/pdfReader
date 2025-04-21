using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

class Program
{
    static void Main()
    {
        Console.Write("Enter folder path: ");
        string? folderPath = Console.ReadLine();

        Console.Write("Enter keyword to search: ");
        string? keyword = Console.ReadLine();

        if (!string.IsNullOrEmpty(folderPath) && !string.IsNullOrEmpty(keyword))
        {
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
        }
        else
        {
            Console.WriteLine("Folder Path not valid!");
        }

        Console.WriteLine("Search complete.");
    }
}