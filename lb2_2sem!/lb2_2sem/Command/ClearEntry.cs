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


namespace lb2_2sem.Command
{
    public class ClearEntry : ICommand
    {
        private TextBox _textBox;
        private List<string[]> _previousAction;

        public ClearEntry(TextBox textBox, List<string[]> previousAction)
        {
            _textBox = textBox;
            _previousAction = previousAction;
        }

        public void Execute()
        {
            if (_previousAction.Count > 0)
            {
                _textBox.Text = _previousAction[^1][0];
                _previousAction.RemoveAt(_previousAction.Count - 1);
            }
            else
            {
                _textBox.Clear();
            }
        }
    }
}
