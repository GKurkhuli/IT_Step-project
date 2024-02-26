using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmanagement
{
    internal class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string PublishYear { get; set; }

        public Book(string title, string author, string publishYear)
        {
            Title = title;
            Author = author;
            PublishYear = publishYear;
        }
        public Book()
        {
            Title = GetBookTitle();
            Author = GetAuthorName();
            PublishYear = GetPublishYear();
        }

        private static string GetBookTitle()
        {
            Console.WriteLine("Enter the title of the book you want to add:");
            return CapitaliseString(GetString("Title"));
        }
        private static string GetAuthorName()
        {
            Console.WriteLine("Enter the author of this book");
            return CapitaliseString(GetString("Author"));
        }
        private static string GetString(string type)
        {
            while (true)
            {
                string s = Console.ReadLine().Trim();
                if (s.Length > 0) return s;

                Console.WriteLine($"{type} field cannot be empty!");
                Console.WriteLine($"Please enter the {type.ToLower()}");
            }
        }
        private static string CapitaliseString(string str)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(str);
        }        
        private static string GetValidYear()
        {
            DateTime currentDate = DateTime.Now;
            while (true)
            {
                int year = GetInt();
                if (year <= currentDate.Year && year >= -2600)
                    return Math.Abs(year).ToString() + (year < 0 ? "BC" : ""); ;
                Console.WriteLine("Invalid year. Enter again");
            }
        }
        private static string GetPublishYear()
        {
            Console.WriteLine("Enter the year the book was published");
            return GetValidYear();
        }
        private static int GetInt()
        {
            int n;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Please enter iteger values only");
            }
            return n;
        }
        public void Display()
        {
            Console.WriteLine(Title);
            Console.WriteLine($"\tby {Author}");
            Console.WriteLine($"\tpublished in {PublishYear}");
        }
        public bool Equals(Book b)
        {
            return this.Title == b.Title
                && this.Author == b.Author
                && this.PublishYear == b.PublishYear;
        }
        public bool PartiallyEquals(Book b)
        {
            return this.Title == b.Title
                && this.Author == b.Author;
        }
    }
}
