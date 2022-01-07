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

//using System.Data;
//using System.Data.SqlClient;

namespace WpfBasics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //string connectionString;

            InitializeComponent();

            /*SqlConnection cnn;
            connectionString = "Data Source=DANIEL-PC\\SQLEXPRESS;Initial Catalog=lifeApp;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            if (cnn.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection OPEN!");
                //cnn.Close();
            }
            else
            {
                Console.WriteLine("Connection FAILED!");
            }*/
        }

        private void PhoneBookButton_Click(object sender, RoutedEventArgs e)
        {
            PhoneSearchWindow subWindow = new PhoneSearchWindow();
            subWindow.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DonationButton_Click(object sender, RoutedEventArgs e)
        {
            Donations donationSubWindow = new Donations();
            donationSubWindow.Show();
        }

        private void QuoteButton_Click(object sender, RoutedEventArgs e)
        {
            Quotes quotesSubWindow = new Quotes();
            quotesSubWindow.Show();
        }
    }
}
