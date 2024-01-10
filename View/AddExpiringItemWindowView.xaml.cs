using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmileLineDentalClinic.View
{
    /// <summary>
    /// Interaction logic for AddProductWindowView.xaml
    /// </summary>
    public partial class AddExpiringItemWindowView : Window
    {
        public AddExpiringItemWindowView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^\d{1,4}([.]\d{1,2})?$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^\d{1,4}([.]\d{1,2})?$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); 
            e.Handled = regex.IsMatch(e.Text);

        }

        private void TextBox_PreviewTextInput_3(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); 
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_4(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); 
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput_5(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
