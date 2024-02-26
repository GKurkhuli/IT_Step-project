using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using static System.Reflection.Metadata.BlobBuilder;

namespace Bookmanagement
{
    internal class BookManager : BookManagerBase
    {
        public static void Manage()
        {
            DisplayInstruction();
            StartManaging();
            Thanks();
        }
       
        private static void StartManaging()
        {
            char choise;
            const string filePath = "Books.txt";
            List<Book> books = LoadBooksFormFile(filePath); 
            do
            {
                choise = ChooseManaginOption();
                if (choise == 'n')
                    AddNewBook(books, filePath);

                if (choise == 'd')
                    DisplayAvailableBooks(books);

                if (choise == 's')
                    DisplayAvailableBooks(SearchByName(books));

            } while (choise == 'n' || choise == 'd' || choise == 's');
        }
        private static char ChooseManaginOption()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Press n, d, s for action");
            Console.WriteLine("Press any other key to close the program");
            return Console.ReadLine().ToLower().FirstOrDefault();
        }

        private static void AddNewBook(List<Book> books, string filePath)
        {
            Book b = new Book();

            if (BookExists(books, b))
                return;
            if (NameAndAuthorExist(books,b))
            {
                AddOrUpdateListAndFile(books, filePath, b);
                return;
            }
            AddBookToListAndFile(books, filePath, b);
        }
        

        private static bool BookExists(List<Book> books, Book b)
        {
            if (books.Any(book => book.Equals(b)))
            {
                Console.WriteLine("This book was already included in the system");
                return true;
            }
            return false;
        }
        private static bool NameAndAuthorExist(List<Book> books, Book b)
        {
            if(books.Any(book => book.PartiallyEquals(b)))
                return true;
            
            return false;
        }

        private static List<Book> SearchByName(List<Book> books)
        {
            List < Book> b = new List<Book>();
            char[] neglect = [' ', '.', ',', '!', '?', ';'];
            string searchTitle = GetSearchTitle(neglect);

            foreach (Book book in books)
            {
                string title = new string(book.Title.ToLower().Where(c => !neglect.Contains(c)).ToArray());
                if(title.Contains(searchTitle))
                    b.Add(book);
            }
            return b;
        }

        private static string GetSearchTitle(char[] neglect)
        {
            Console.WriteLine("Enter the title or part of it of the book you want to find");
            return new string(Console.ReadLine().Trim().ToLower().Where(c => !neglect.Contains(c)).ToArray());
        }

        private static void DisplayAvailableBooks(List<Book> books)
        {
            Console.WriteLine($"There are {books.Count} books in total");
            foreach (Book book in books)
                book.Display();
        }
        
        private static void DisplayInstruction()
        {
            Console.WriteLine("       You are using book manager");
            Console.WriteLine("     In order to add new bokk, press n");
            Console.WriteLine("For diplaying all the available books, press d");
            Console.WriteLine("    For searching book by title, press s");
            Console.WriteLine("  To close the program, press any other key");
        }
        private static void Thanks()
        {
            Console.WriteLine();
            Console.WriteLine("Thank You For Using Our Book Management System");
            Console.WriteLine("            Hope You Enjoied");
        }
    }
}
