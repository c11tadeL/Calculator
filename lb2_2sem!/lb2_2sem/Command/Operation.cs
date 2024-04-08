using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Diagnostics.Tracing;


namespace lb2_2sem.Command
{
    public class Operation : ICommand
    {
        private readonly TextBox _textBox;
        private readonly string? _operation;
        private readonly char[]? _operators;
        private readonly string? _operationPattern;
        private readonly List<string[]> _previousAction;

        public Operation(TextBox textBox, string? operation, char[]? operators, string? operationPattern, List<string[]> previousAction)
        {
            _textBox = textBox;
            _operation = operation;
            _operators = operators;
            _operationPattern = operationPattern;
            _previousAction = previousAction;
        }

        public void Execute()
        {
            string expression = _textBox.Text;

            if (_operation == ".")
            {
                if (expression.Length > 0 && char.IsDigit(expression[^1]))
                {
                    if (!expression.Split(_operators).Last().Contains('.'))
                    {
                        _textBox.Text += _operation;
                    }
                }
                return;
            }

            if (_operation == "+" || _operation == "-" || _operation == "×" || _operation == "÷")
            {
                if (expression.Length > 0)
                {
                    char lastChar = expression[^1];

                    if (lastChar == '.') return;

                    if (lastChar == '+' || lastChar == '-' || lastChar == '×' || lastChar == '÷')
                    {
                        _textBox.Text = expression.Substring(0, expression.Length - 1);
                    }
                }
            }

            _textBox.Text += _operation;

            if (!string.IsNullOrEmpty(_operationPattern) && Regex.IsMatch(_textBox.Text, _operationPattern))
            {
                _textBox.Text = _textBox.Text.Substring(0, _textBox.Text.Length - 1);

                new Calculation(_textBox, _previousAction).Execute();

                _textBox.Text += _operation;
            }
        }
    }
}
