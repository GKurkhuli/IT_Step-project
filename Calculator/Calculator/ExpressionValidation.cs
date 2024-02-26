namespace Calculator
{
    internal class ExpressionValidation
    {
        private static readonly char[] previousOperators = ['+', '-', '*', '/', '('];
        protected static bool IsAllowedKey(char key, string expression)
        {
            if (char.IsDigit(key))
                return true;

            if (key == '+' || key == '-' || key == '*' || key == '/')
                return IsOperationAllowed(key, expression);

            if (key == '(')
                return IsOpenBracketAllowed(expression);

            if (key == ')' && expression.Length > 0)
                return IsClosingBracketAllowed(expression);

            if (key == '.')
                return IsDecimalPointAllowed(expression);

            return false;
        }
       
        private static bool IsClosingBracketAllowed(string expression)
        {
            if (!expression.Contains('('))
                return false;
            int[] bracketsCunt = OpenAndCloseingBracketsCount(expression);

            return !previousOperators.Any(pre => expression.EndsWith(pre)) && bracketsCunt[0] > bracketsCunt[1];
        }
        private static bool IsDecimalPointAllowed(string expression)
        {
            char[] splitWith = ['+', '-', '*', '/', '(', ')'];
            string[] separateNumbers = expression.Split(splitWith);

            return !separateNumbers.Last().Contains('.');
        }
        private static bool IsOpenBracketAllowed(string expression)
        {
            return expression.Length == 0 || previousOperators.Any(pre => expression.EndsWith(pre));
        }
        private static bool IsOperationAllowed(char key, string expression)
        {
            if (expression.Length == 0 || expression.EndsWith("("))
                return key == '+' || key == '-';

            char lastChar = expression[expression.Length - 1];
            return lastChar != '+' && lastChar != '-' && lastChar != '*' && lastChar != '/';
        }
        private static int[] OpenAndCloseingBracketsCount(string expression)
        {
            int oBC = 0;
            int cBC = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    oBC++;
                else if (expression[i] == ')')
                    cBC++;
            }
            return [oBC, cBC];
        }
        protected static string FinaliseExpression(string expression)
        {
            expression = RemoveUnfinishedOperations(expression);

            if (!expression.Contains('('))
                return expression;

            return FillMissingClosingBrackets(expression);
        }
        private static string FillMissingClosingBrackets(string expression)
        {
            int[] bracketsCunt = OpenAndCloseingBracketsCount(expression);
            while (bracketsCunt[0] > bracketsCunt[1])
            {
                expression = expression + ")";
                bracketsCunt[1]++;
            }

            return expression;
        }        

        private static string RemoveUnfinishedOperations(string expression)
        {
            while (previousOperators.Any(op => expression.EndsWith(op)))
                expression = expression.Remove(expression.Length - 1);

            return expression;
        }
    }
}