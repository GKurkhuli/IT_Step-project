namespace Bookmanagement
{
    abstract class BookManagerBase
    {
        internal static List<Book> LoadBooksFormFile(string filePath)
        {
            List<Book> books = new List<Book>();
            if (!File.Exists(filePath))
                CreateFile(filePath);

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] desc = line.Split('|');
                    if (desc.Length == 3)
                    {
                        books.Add(new Book(desc[0].Trim(), desc[1].Trim(), desc[2].Trim()));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while reading from file: {e.Message}");
            }

            return books;
        }
        private static void CreateFile(string filePath)
        {
            try
            {
                using FileStream fs = File.Create(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while creating file: {e.Message}");
            }
        }
        internal static void AddOrUpdateListAndFile(List<Book> books, string filePath, Book b)
        {
            Console.WriteLine("There is a book with the same title and author you just entered");
            Console.WriteLine("If you wish to update publish year of the book press u");
            //   Console.WriteLine("Press any other key to discard the new book");
            char choise = Console.ReadLine().ToLower().Trim().FirstOrDefault();
            if (choise == 'u')
                UpdatePublishYearInListAndFile(books, filePath, b);
            /* else if (choise == 'n')
                 AddBookToListAndFile(books, filePath, b);*/
            else
                return;
        }
        internal static void AddBookToListAndFile(List<Book> books, string filePath, Book b)
        {
            books.Add(b);
            AddNewBookToFile(b, filePath);
            Console.WriteLine("Book was successfully added");
        }
        private static void AddNewBookToFile(Book b, string filePath)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine($"{b.Title}|{b.Author}|{b.PublishYear}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while adding new book: {e.Message}");
            }
        }
        
        

        private static void UpdatePublishYearInListAndFile(List<Book> books, string filePath, Book b)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                string titeleAndAuthor = $"{b.Title}|{b.Author}";
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(titeleAndAuthor))
                    {
                        lines[i] = lines[i].Replace(lines[i].Split('|')[2], b.PublishYear);
                    }

                }
                File.WriteAllLines(filePath, lines);

                foreach (Book book in books)
                {
                    if (book.PartiallyEquals(b))
                    {
                        book.PublishYear = b.PublishYear;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while updating file: {e.Message}");
            }
        }
    }
}