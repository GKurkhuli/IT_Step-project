using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    internal class HangmanGame
    {
        private string word;
        private string hint;
        private StringBuilder guessed;
        private int attemptsLeft;
        private char[] maskedWord;
        public HangmanGame(string word, string hint)
        {
            this.word = word.ToUpper();
            this.hint = hint;
            maskedWord = new char[word.Length];
            Array.Fill(maskedWord, '_');
            attemptsLeft = 6;
            guessed = new StringBuilder();
        }
        public void Play()
        {
            Console.WriteLine($"Hints: {hint}");
            Console.WriteLine($"Word to Guess: {string.Join(" ", maskedWord)}");
            while (attemptsLeft > 0)
            {
                Console.WriteLine($"Attempts left: {attemptsLeft}");
                Console.WriteLine($"Guessed Letters: {guessed}");

                 CheckCharacterInWord(CharacterValidation());

                Console.WriteLine("Word to guess: " + string.Join(" ", maskedWord));

                if (maskedWord.SequenceEqual(word))
                {
                    Console.WriteLine("Congratulations! You guessed the word.");
                    return;
                }
            }
            Console.WriteLine("Sorry, you lost. The word was: " + word);
        }
        private char CharacterValidation()
        {
            while (true)
            {
                Console.Write("Guess letter: ");
                char guess = Console.ReadLine().ToUpper().FirstOrDefault();
                if (!char.IsLetter(guess))
                {
                    Console.WriteLine("Invalid input. Please enter letters only");
                    continue;
                }
                if (guessed.ToString().Contains(guess))
                {
                    Console.WriteLine("Lettar was already entered before");
                    continue;
                }
                guessed.Append(guess);
                return guess;
            }
        }
        private void CheckCharacterInWord(char guess)
        {
            if (word.Contains(guess))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == guess)
                    {
                        maskedWord[i] = guess;
                    }
                }
            }
            else
            {
                attemptsLeft--;
            }
        }
    }
}
