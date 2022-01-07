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
using System.Windows.Shapes;

using System.Data;
using System.Data.SqlClient;

namespace WpfBasics
{
    /// <summary>
    /// Interaction logic for Quotes.xaml
    /// </summary>
    public partial class Quotes : Window
    {
        public Quotes()
        {
            InitializeComponent();
            dbConnection db = new dbConnection();

            SqlCommand listcmd = new SqlCommand();
            listcmd.Connection = dbConnection.cnn;

            listcmd.CommandText = "Select quote from quotes";
            SqlDataReader reader = listcmd.ExecuteReader();

            while (reader.Read())
            {
                listWithColumns.Items.Add(new Quote { quote = reader["quote"].ToString()});
            }
            reader.Close();
        }

        private void addQuoteButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(editQuoteTextBox.Text);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection.cnn;

            cmd.CommandText = "INSERT INTO quotes(quote) VALUES(@param1)";
            cmd.Parameters.AddWithValue("@param1", editQuoteTextBox.Text);

            if (String.IsNullOrEmpty(editQuoteTextBox.Text))
            {
                Console.WriteLine("Invalid Input!");
                return;
            }
            if (cmd.ExecuteNonQuery() == 1)
            {
                Console.WriteLine("Successfully Inserted into donations database!");
                listWithColumns.Items.Add(new Quote { quote = editQuoteTextBox.Text });
                editQuoteTextBox.Clear();
                //listSelect.Items.Add(nameInput.Text + "\t\t\t" + numberInput.Text);
            }
            else
                Console.WriteLine("Something went wrong!");
        }

        private void listWithColumns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public class Quote
        {
            public string quote { get; set; }
        }

        private void deleteQuoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (listWithColumns.SelectedItems.Count == 0)
            {
                Console.WriteLine("No Quotes Selected to Delete!");
                return;
            }

            var selectedContactObject = listWithColumns.SelectedItems[0] as Quote;

            SqlCommand deletecmd = new SqlCommand();
            deletecmd.Connection = dbConnection.cnn;
            deletecmd.CommandText = "Delete FROM quotes WHERE quote =@quote";
            deletecmd.Parameters.AddWithValue("@quote", selectedContactObject.quote.ToString());

            if (deletecmd.ExecuteNonQuery() == 1)
            {
                listWithColumns.Items.Remove(listWithColumns.SelectedItem);
                Console.WriteLine("Quote Deleted.");
            }
            else
            {
                //searchResults.Clear();
                Console.WriteLine("Quote to delete not found!");
            }
        }
    }
}
