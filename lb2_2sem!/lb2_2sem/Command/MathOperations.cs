using System;
using System.Windows.Controls;
using System.Collections.Generic;

namespace lb2_2sem.Command
{
    public class MathOperations : ICommand
    {
        private readonly TextBox _textBox;
        private readonly char[] _operators;
        private readonly Dictionary<string, Func<double, double>> _mathOperators = new()
        {
            { "sqrt", Math.Sqrt },
            { "Pi", number => number * Math.PI },
            { "exp", Math.Exp },
            { "n^2", number => Math.Pow(number, 2) },
            { "log", Math.Log }
        };

        public MathOperations(TextBox textBox, char[] operators)
        {
            _textBox = textBox;
            _operators = operators;
        }

        public void Execute()
        {
            string expression = _textBox.Text.Trim();

            foreach (var kvp in _mathOperators)
            {
                string mathOperator = kvp.Key;
                if (expression.EndsWith(mathOperator))
                {
                    MathOperation(mathOperator);
                    return;
                }
            }
        }

        private void MathOperation(string mathOperator)
        {
            string expression = _textBox.Text;

            if (expression.EndsWith(mathOperator) && expression.Length > 0)
            {
                expression = expression.Remove(expression.Length - mathOperator.Length);

                string[] parts = expression.Split(_operators, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 0)
                {
                    string lastNumberString = parts[^1].Trim();

                    if (double.TryParse(lastNumberString, out double number))
                    {
                        if (_mathOperators.TryGetValue(mathOperator, out Func<double, double> operation))
                        {
                            double result = operation.Invoke(number);
                            _textBox.Text = expression.Substring(0, expression.Length - lastNumberString.Length) + result;
                        }
                    }
                }
            }
        }

    }
}
