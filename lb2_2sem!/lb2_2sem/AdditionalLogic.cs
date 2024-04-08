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

namespace lb2_2sem
{
    public static class AdditionalLogic
    {
        public static bool CanAddOperator(string inputText, string newOperator)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return false;
            }
            //if (newOperator == "-")
            //{
            //    return !Regex.IsMatch(inputText, @"-{1,}");
            //}

            return true;
        }
        public static string NormalizeOperators(string inputText)
        {

            string pattern = @"([+\-×\÷])\1+";
            string normalizedText = Regex.Replace(inputText, pattern, "$1");
            normalizedText = Regex.Replace(normalizedText, @"[+\-×\÷/]+", match =>
            {
                return match.Value[0].ToString();
            });

            return normalizedText;

        }
        public static bool CanAddZero(string inputText)
        {
            return string.IsNullOrEmpty(inputText);
        }

        public static string NormalizeZero(string inputText)
        {
            return CanAddZero(inputText) ? "0" : inputText;
        }
        public static bool CanAddZeroAfterDecimalPoint(string inputText)
        {
            return inputText.Contains(".");
        }
        public static bool CanAddDecimalPoint(string inputText)
        {
            return !string.IsNullOrEmpty(inputText);
        }

        public static string NormalizeDots(string inputText)
        {
            return Regex.Replace(inputText, @"(?<=\d)\.+([0-9])", ".$1");
        }
        public static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
        public static bool IsAllowedInput(string input)
        {
           
            return char.IsDigit(input[0]) || IsOperator(input[0]) || input == ".";
        }
        public static void MoveCursorToEnd(TextBox textBox)
        {
            textBox.Select(textBox.Text.Length, 0);
        }
        public static void AdjustFontSize(TextBox textBox)
        {
            if (textBox.Text.Length > 30)
            {
                textBox.FontSize = 14; 
            }
            else if (textBox.Text.Length > 15)
            {
                textBox.FontSize = 18; 
            }
            else
            {
                textBox.FontSize = 22; 
            }
        }

    }

}
