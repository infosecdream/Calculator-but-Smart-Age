using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartAgeCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Operator _currectOps;
        private bool _periodExist = false;
        public MainWindow()
        {
            InitializeComponent();
            foreach (Button button in ButtonGrid.Children)
            {
                if (button is null) continue;
                
                bool isNumeric = IsNumericRegex().IsMatch((string)button.Content);
                if (isNumeric)
                {
                    button.Click += NumberButton_Click;
                }
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            inputField.Text += (sender as Button)?.Content;
        }

        [GeneratedRegex(@"^\d$")]
        private static partial Regex IsNumericRegex();

        private void PeriodButton_Click(object? sender, RoutedEventArgs? e)
        {
            if (_periodExist) return;
            _periodExist = true;
            inputField.Text += '.';
        }

        private static readonly HashSet<Key> allowedInputKeys = [Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9];
        private void inputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemPeriod)
            {
                if (_periodExist)
                    e.Handled = true;
                PeriodButton_Click(null, null);
                return;
            }
            if (allowedInputKeys.Contains(e.Key))
            {
                inputField.Text += (e.Key - Key.D0).ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            inputField.Text = string.Empty;
            _periodExist = false;
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = inputField.Text;
            if (inputText.Length == 0) return;
            if (inputText[^1] == '.') _periodExist = false;
            inputField.Text = inputText.Remove(inputField.Text.Length - 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}