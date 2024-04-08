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
    class ClearAll : ICommand
    {
        private TextBox _textBox;
        public ClearAll(TextBox textBox)
        {
            _textBox = textBox;
        }
        public void Execute()
        {
            _textBox.Text = string.Empty;
        }
    }
}
