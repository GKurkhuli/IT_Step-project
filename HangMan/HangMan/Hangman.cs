using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    internal class Hangman
    {        
        public static void Play()
        {
            Console.WriteLine("Welcome to Hangman!");

            List<Word> wordList = LoadWordsFromFile("words.txt");

            while (wordList.Any())
            {

                GamePlay(wordList);

                Console.Write("Do you want to play again? (Y/N): ");
                string playAgain = Console.ReadLine().Trim().ToUpper();

                if (playAgain != "Y")
                    return;
            }
            Console.WriteLine("All words have been used. Game over!");
        }
        private static List<Word> LoadWordsFromFile(string filePath)
        {
            List<Word> words = new List<Word>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] components = line.Split('|');
                    if (components.Length == 3)
                    {
                        words.Add(new Word(components[0].Trim(), components[1].Trim(), components[2].Trim()));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: file couldn't be found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return words;
        }
        public static void GamePlay(List<Word> wordList)
        {
            Word selectedWord = GetRandomWordUnderChosenCategory(wordList);
            wordList.Remove(selectedWord);

            HangmanGame game = new HangmanGame(selectedWord.WordText, selectedWord.Hint);
            game.Play();           
        }
        private static Word GetRandomWordUnderChosenCategory(List<Word> wordList)
        {
            while (true)
            {
                DisplayAvaliableCategories(wordList);

                Console.Write("Enter the category: ");
                string selectedCategory = Console.ReadLine();

                var wordsInCategory = wordList
                    .Where(word => word.Category.Equals(selectedCategory))
                    .ToList();

                if (wordsInCategory.Count == 0)
                {
                    Console.WriteLine("There are no words available in this category. Please select another category.");
                    continue;
                }

                Random random = new Random();
                return wordsInCategory[random.Next(wordsInCategory.Count)]; ;
            }
        }
        private static void DisplayAvaliableCategories(List<Word> wordList)
        {
            var availableCategories = wordList.Select(word => word.Category).Distinct();

            Console.WriteLine("Select a category:");
            foreach (var category in availableCategories)
            {
                Console.WriteLine($"- {category}");
            }
        }
    }
}
