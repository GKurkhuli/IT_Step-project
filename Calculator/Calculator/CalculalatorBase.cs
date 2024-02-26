namespace Calculator
{
    internal class CalculalatorBase
    {

        private static string CheckAndAddMissingClosingBrackets(string expression)
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
            while (oBC > cBC)
            {
                expression = expression + ")";
                cBC++;
            }

            return expression;
        }

        private static string FillMissingClosingBrackets(string expression)
        {
            if (expression.Contains('('))
                return CheckAndAddMissingClosingBrackets(expression);

            return expression;
        }

        private static bool IsAllowedKey(char key, string expression)
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

            int oBC = 0;
            int cBC = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    oBC++;
                else if (expression[i] == ')')
                    cBC++;
            }

            return !expression.EndsWith('(') && oBC > cBC;
        }
        private static bool IsDecimalPointAllowed(string expression)
        {
            char[] splitWith = ['+', '-', '*', '/', '(', ')'];
            string[] separateNumbers = expression.Split(splitWith);

            return !separateNumbers.Last().Contains('.');
        }
        private static bool IsOpenBracketAllowed(string expression)
        {
            char[] allowedPrevious = ['+', '-', '*', '/', '('];

            return expression.Length == 0 || allowedPrevious.Any(pre => expression.EndsWith(pre));
        }
        private static bool IsOperationAllowed(char key, string expression)
        {
            if (expression.Length == 0 || expression.EndsWith("("))
                return key == '+' || key == '-';

            char lastChar = expression[expression.Length - 1];
            return lastChar != '+' && lastChar != '-' && lastChar != '*' && lastChar != '/';
        }

        private static string RemoveUnfinishedOperations(string expression)
        {
            char[] notAllowedOperations = ['+', '-', '*', '/', '('];

            while (notAllowedOperations.Any(op => expression.EndsWith(op)))
                expression = expression.Remove(expression.Length - 1);

            return expression;
        }
    }
}