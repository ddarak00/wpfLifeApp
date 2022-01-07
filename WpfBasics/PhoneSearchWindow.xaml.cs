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
    /// Interaction logic for PhoneSearchWindow.xaml
    /// </summary>
    public sealed partial class PhoneSearchWindow : Window
    {
        /*private static PhoneSearchWindow instance = null;  Singleton stuff
        private static readonly object padlock = new object();*/

        public PhoneSearchWindow()
        {
            InitializeComponent();
            dbConnection db = new dbConnection();

            SqlCommand listcmd = new SqlCommand();
            listcmd.Connection = dbConnection.cnn;
            listcmd.CommandText = "Select name, number from phoneBook";
            SqlDataReader reader = listcmd.ExecuteReader();

            while (reader.Read())
            {
                listWithColumns.Items.Add(new Contact { name = reader["name"].ToString(), number = reader["number"].ToString() });
            }
            reader.Close();
        }

        public void RefreshList()
        {

            SqlCommand listcmd = new SqlCommand();
            listcmd.Connection = dbConnection.cnn;
            listcmd.CommandText = "Select name, number from phoneBook";
            SqlDataReader reader = listcmd.ExecuteReader();

            listWithColumns.Items.Clear();
            while (reader.Read())
            {
                listWithColumns.Items.Add(new Contact { name = reader["name"].ToString(), number = reader["number"].ToString() });
                Console.WriteLine(reader["number"].ToString());
            }
            reader.Close();
            Console.WriteLine("RefreshList2");
        }

        /*public static PhoneSearchWindow Instance
        { //Singleton Stuff
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PhoneSearchWindow();
                    }
                    return instance;
                }
            }
        }*/

        private void searchResults_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void submitContactButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("SUBMIT CLICKED!");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection.cnn;

            cmd.CommandText = "INSERT INTO phoneBook(name, number) VALUES(@param1,@param2)";

            cmd.Parameters.AddWithValue("@param1", nameInput.Text);
            cmd.Parameters.AddWithValue("@param2", numberInput.Text);

            if (cmd.ExecuteNonQuery() == 1)
            {
                Console.WriteLine("Successfully Inserted into database!");
                listWithColumns.Items.Add(new Contact { name = nameInput.Text, number = numberInput.Text });
                nameInput.Clear(); numberInput.Clear();
                //listSelect.Items.Add(nameInput.Text + "\t\t\t" + numberInput.Text);
            }
            else
                Console.WriteLine("Something went wrong!");
        }

        private void nameBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void numberBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void closePhoneBookButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            /*SqlCommand listcmd = new SqlCommand();
            listcmd.Connection = dbConnection.cnn;
            listcmd.CommandText = "Select number from phoneBook";
            SqlDataReader reader = listcmd.ExecuteReader();
            searchResults.Clear();

            while (reader.Read())
            {
                searchResults.AppendText(reader["number"].ToString() + '\n');
            }
            reader.Close();*/
        }

        private void deleteContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (listWithColumns.SelectedItems.Count == 0)
            {
                Console.WriteLine("Nothing Selected to Delete!");
                return;
            }

            var selectedContactObject = listWithColumns.SelectedItems[0] as Contact;

            //Console.WriteLine(selectedContactObject.name.ToString() + " " + selectedContactObject.number.ToString());
            ///////////////////////
            /*SqlCommand listcmd = new SqlCommand();
            listcmd.Connection = dbConnection.cnn;
            listcmd.CommandText = "Select name, number from phoneBook";
            SqlDataReader reader = listcmd.ExecuteReader();

            while (reader.Read())
            {
                listSelect.Items.Add(reader["name"].ToString() + "\t\t\t" + reader["number"].ToString());
            }
            reader.Close();*/
            //////////////////////////////////////////
            SqlCommand deletecmd = new SqlCommand();
            deletecmd.Connection = dbConnection.cnn;
            deletecmd.CommandText = "Delete FROM phoneBook WHERE name =@name AND number =@number";
            deletecmd.Parameters.AddWithValue("@name", selectedContactObject.name.ToString());
            deletecmd.Parameters.AddWithValue("@number", selectedContactObject.number.ToString());


            if (deletecmd.ExecuteNonQuery() == 1)
            {
                listWithColumns.Items.Remove(listWithColumns.SelectedItem);
                Console.WriteLine(" Deleted.");
            }
            else
            {
                //searchResults.Clear();
                Console.WriteLine(" not found!");
            }
        }

        private void listWithColumns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void editContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (listWithColumns.SelectedItems.Count == 0)
            {
                Console.WriteLine("Nothing Selected to Edit!");
                return;
            }

            var selectedContactObject = listWithColumns.SelectedItems[0] as Contact;


            editWindow editSubWindow = new editWindow(selectedContactObject);
            editSubWindow.Show();
        }

        public void printTest()
        {
            Console.WriteLine("printTest");
            listWithColumns.Items.Remove(listWithColumns.SelectedItem);
        }
    }

    public class Contact
    {
        public string name { get; set; }
        public string number { get; set; }
    }
}
