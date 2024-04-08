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
    public class RemoveLast : ICommand
    {
        private readonly TextBox _textBox;
        private string _removedCharacter;

        public RemoveLast(TextBox textBox)
        {
            _textBox = textBox;
        }

        public void Execute()
        {
            if (!string.IsNullOrEmpty(_textBox.Text))
            {
                _removedCharacter = _textBox.Text.Substring(_textBox.Text.Length - 1);
                _textBox.Text = _textBox.Text.Remove(_textBox.Text.Length - 1);
            }
        }
    }
}
