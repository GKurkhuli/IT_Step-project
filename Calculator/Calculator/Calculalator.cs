using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Calculalator : ExpressionValidation
    {
        public static void Calculate()
        {
            Greatings();
            Calculations();
            Thanks();
        }
        
        private static void Calculations()
        {
            while (true)
            {
                DisplayIntroducyion();
                ForCalculation expression = GetExpression();

                if (expression.Key.Key == ConsoleKey.Escape)
                    return;

                DisplayResult(FinaliseExpression(expression.Expression));
            }
        }
        
        private static ForCalculation GetExpression()
        {
            ConsoleKeyInfo keyInfo;
            string expression = "";
            do
            {
                keyInfo = Console.ReadKey(intercept: true);

                if (IsAllowedKey(keyInfo.KeyChar, expression))
                {
                    Console.Write(keyInfo.KeyChar);
                    expression += keyInfo.KeyChar;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && expression.Length > 0)
                {
                    Console.Write("\b \b");
                    expression = expression.Substring(0, expression.Length - 1);
                }
            } while (!(keyInfo.Key == ConsoleKey.Enter && expression.Length > 0) && keyInfo.Key != ConsoleKey.Escape);

            return new ForCalculation(expression, keyInfo);
        }
        private static void DisplayResult(string expression)
        {
            Console.WriteLine();
            Console.WriteLine($"Expression: {expression}");
            EvaluateExpression(expression);
        }

        private static void EvaluateExpression(string expression)
        {
            DataTable dt = new();
            try
            {
                object result = dt.Compute(expression, "");
                Console.WriteLine($"Result: {Convert.ToDouble(result)}") ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void Greatings()
        {
            Console.WriteLine("This is simple calculator");
            Console.WriteLine("To calculate expressions press any key");
            Console.WriteLine("You can always stop calculations if you pres Esc key");
        }
        private static void Thanks()
        {
            Console.WriteLine();
            Console.WriteLine("Thank you for your hard work!");
            Console.WriteLine("I hope you enjoyed it!");
        }
        private static void DisplayIntroducyion()
        {
            Console.WriteLine();
            Console.WriteLine("Simple Calculator");
            Console.WriteLine("Enter an arithmetic expression using numbers and basic operators (+, -, *, /,(,)):");
        }
    }
}
