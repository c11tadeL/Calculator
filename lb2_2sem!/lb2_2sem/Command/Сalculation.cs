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
using System.Data;


namespace lb2_2sem.Command
{
    public class Calculation : ICommand
    {
        private readonly TextBox _textBox;
        private readonly List<string[]> _previousAction;

        public Calculation(TextBox textBox, List<string[]> previousAction)
        {
            _textBox = textBox;
            _previousAction = previousAction;
        }

        public void Execute()
        {
            try
            {
                if (!string.IsNullOrEmpty(_textBox.Text))
                {
                    string expression = _textBox.Text.Replace('×', '*').Replace('÷', '/');
                    object result = new DataTable().Compute(expression, null);
                    _previousAction.Add(new string[] { _textBox.Text });
                    _textBox.Text = result?.ToString() ?? string.Empty;
                }
            }
            catch (DivideByZeroException)
            {
                _textBox.Text = "Error: Division by zero";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
