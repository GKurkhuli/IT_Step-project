using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    internal class Word
    {
        public string Category { get; }
        public string WordText { get; }
        public string Hint { get; }

        public Word(string category, string word, string hint) {
            Category = category;
            WordText = word;
            Hint = hint;
        }
    }
}
