using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace PdfsReader
{
    class Program
    {
        static void Main()
        {
            bool continueSearch = true;

            while (continueSearch)
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
                var matchedFiles = new List<string>();

                foreach (var file in pdfFiles)
                {
                    using (PdfDocument document = PdfDocument.Open(file))
                    {
                        var text = "";
                        foreach (Page page in document.GetPages())
                        {
                            text += page.Text;
                        }

                        if (text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            matchedFiles.Add(Path.GetFileName(file));
                        }
                    }
                }
                
                if (matchedFiles.Count > 0)
                {
                    Console.WriteLine("Files containing the keyword:");
                    foreach (var filename in matchedFiles)
                    {
                        Console.WriteLine($"- {filename}");
                    }
                }
                else
                {
                    Console.WriteLine("No files found with the given keyword.");
                }

                Console.WriteLine("\nSearch complete.");
                
                Console.Write("\nDo you want to perform another search? (yes/no): ");
                var answer = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(answer) || !answer.Trim().ToLower().StartsWith("y"))
                {
                    continueSearch = false;
                }

                Console.WriteLine();
            }

            Console.WriteLine("Goodbye!");
        }
    }
}
