using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class ForCalculation
    {
        public string Expression { get; }
        public ConsoleKeyInfo Key { get; }

        public ForCalculation(string expression, ConsoleKeyInfo key)
        {
            Expression = expression;
            Key = key;
        }

    }
}
