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
using lb2_2sem.Command;
using System.Globalization;


namespace lb2_2sem
{
   
    public partial class MainWindow : Window
    {

        private readonly List<string[]> _previousActions = new();
        private MathOperations _mathOperations;
        public MainWindow()
        {
            InitializeComponent();
            _mathOperations = new MathOperations(txtInput, new char[] { '+', '-', '*', '/' });
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            txtInput.PreviewKeyDown += txtInput_PreviewKeyDown;
            txtInput.PreviewTextInput += txtInput_PreviewTextInput;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            GridLength si = new GridLength(1, GridUnitType.Star);
            if (additionalOptions.Width == si)
            {
                GridLength zero = new GridLength(0, GridUnitType.Star);
                additionalOptions.Width = zero;
            }
            else
            {
                additionalOptions.Width = si;
            }

        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = string.Empty;
        }
        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            ClearEntry clearEntryCommand = new ClearEntry(txtInput, _previousActions);
            clearEntryCommand.Execute();
        }
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Content.ToString();
            string currentText = txtInput.Text;
            switch (buttonText)
            {
                case "00":
                    if (AdditionalLogic.CanAddZero(currentText) || AdditionalLogic.CanAddZeroAfterDecimalPoint(currentText))
                    {
                        txtInput.Text = buttonText;
                       txtInput.Text = AdditionalLogic.NormalizeZero(currentText + "0");
                    }
                    break;
                case "0":
                    if (AdditionalLogic.CanAddZero(currentText) || AdditionalLogic.CanAddZeroAfterDecimalPoint(currentText))
                    {
                        txtInput.Text = buttonText;
                        txtInput.Text = AdditionalLogic.NormalizeZero(currentText + buttonText);
                    }
                    break;
                case ".":
                    if (AdditionalLogic.CanAddDecimalPoint(currentText))
                    {
                        txtInput.Text = currentText + buttonText;
                    }
                    break;
                default:
                    txtInput.Text = currentText + buttonText;
                    break;
            }
            txtInput.Text = AdditionalLogic.NormalizeDots(txtInput.Text);
        }




        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operatorText = button.Content.ToString();
            if (AdditionalLogic.CanAddOperator(txtInput.Text, operatorText))
            {
                txtInput.Text += operatorText;
                txtInput.Text = AdditionalLogic.NormalizeOperators(txtInput.Text);
                _mathOperations.Execute();
            }
        }


        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            ICommand calculation = new Calculation(txtInput, _previousActions);
            calculation.Execute();
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            AdditionalLogic.AdjustFontSize(txtInput);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            RemoveLast removeLastOperation = new RemoveLast(txtInput);
            removeLastOperation.Execute();
        }
        private void txtInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EqualsButton_Click(sender, e);
                e.Handled = true;
                AdditionalLogic.MoveCursorToEnd(txtInput);
            }
        }
        private void txtInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!AdditionalLogic.IsAllowedInput(e.Text))
            {
                e.Handled = true; 
            }
        }
    }
}
