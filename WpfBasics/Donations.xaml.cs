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
    /// Interaction logic for Donations.xaml
    /// </summary>
    public partial class Donations : Window
    {
        public Donations()
        {
            InitializeComponent();
            dbConnection dbDon = new dbConnection();

            SqlCommand listcmd = new SqlCommand();
            listcmd.Connection = dbConnection.cnn;
            listcmd.CommandText = "Select name, amount, deductible, date from donations";
            SqlDataReader reader = listcmd.ExecuteReader();

            while (reader.Read())
            {
                listWithColumnsDon.Items.Add(new Donation { name = reader["name"].ToString(), amount = (decimal)reader["amount"], isDeductible = (Boolean)reader["deductible"], date = (DateTime)reader["date"] });
            }
            reader.Close();
        }

        private void listWithColumns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void submitContactButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("SUBMIT CLICKED!");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection.cnn;

            cmd.CommandText = "INSERT INTO donations(name, amount, deductible, date) VALUES(@param1,@param2,@param3,@param4)";

            if (String.IsNullOrEmpty(nameInput.Text))
            {
                Console.WriteLine("Invalid Input!");
                return;
            }

            cmd.Parameters.AddWithValue("@param1", nameInput.Text);
            cmd.Parameters.AddWithValue("@param2", System.Convert.ToDecimal(amountInput.Text));
            cmd.Parameters.AddWithValue("@param3", (bool)deductibleInput.IsChecked);
            cmd.Parameters.AddWithValue("@param4", DateTime.Parse(dateInput.Text));

            decimal donationAmount;
            bool parseOK = decimal.TryParse(amountInput.Text, out donationAmount);

            if (cmd.ExecuteNonQuery() == 1)
            {
                Console.WriteLine("Successfully Inserted into donations database!");
                listWithColumnsDon.Items.Add(new Donation { name = nameInput.Text, amount = donationAmount, isDeductible = (bool)deductibleInput.IsChecked, date = DateTime.Parse(dateInput.Text) });
                nameInput.Clear(); amountInput.Clear(); deductibleInput.IsChecked = false; dateInput.Clear();
                //listSelect.Items.Add(nameInput.Text + "\t\t\t" + numberInput.Text);
            }
            else
                Console.WriteLine("Something went wrong!");
        }

        private void editContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (listWithColumnsDon.SelectedItems.Count == 0)
            {
                Console.WriteLine("Nothing Selected to Edit!");
                return;
            }

            var selectedContactDonation = listWithColumnsDon.SelectedItems[0] as Donation;


            editWindowDono editSubWindowDono = new editWindowDono(selectedContactDonation);
            editSubWindowDono.Show();
        }

        private void deleteContactButton_Click(object sender, RoutedEventArgs e)
        {
            if (listWithColumnsDon.SelectedItems.Count == 0)
            {
                Console.WriteLine("Nothing Selected to Delete!");
                return;
            }

            var selectedContactObject = listWithColumnsDon.SelectedItems[0] as Donation;

            SqlCommand deletecmd = new SqlCommand();
            deletecmd.Connection = dbConnection.cnn;
            deletecmd.CommandText = "Delete FROM donations WHERE name =@name AND amount =@amount AND date =@date";
            deletecmd.Parameters.AddWithValue("@name", selectedContactObject.name.ToString());
            deletecmd.Parameters.AddWithValue("@amount", selectedContactObject.amount.ToString());
            deletecmd.Parameters.AddWithValue("@date", selectedContactObject.date.ToString());


            if (deletecmd.ExecuteNonQuery() == 1)
            {
                listWithColumnsDon.Items.Remove(listWithColumnsDon.SelectedItem);
                Console.WriteLine(" Deleted.");
            }
            else
            {
                //searchResults.Clear();
                Console.WriteLine(" not found!");
            }
        }

        public void RefreshList()
        {
            Console.WriteLine("Donations list updated!");
            SqlCommand listcmd = new SqlCommand();
            listcmd.Connection = dbConnection.cnn;
            listcmd.CommandText = "Select name, amount, deductible, date from donations";
            SqlDataReader reader = listcmd.ExecuteReader();

            listWithColumnsDon.Items.Clear();
            while (reader.Read())
            {
                listWithColumnsDon.Items.Add(new Donation { name = reader["name"].ToString(), amount = (decimal)reader["amount"],
                    isDeductible = (bool)reader["deductible"], date = (DateTime)reader["date"]});
                //Console.WriteLine(reader["name"].ToString());
            }
            reader.Close();
            Console.WriteLine("RefreshList Donation");
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void amountBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void McCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void McCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void dateBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class Donation
    {
        public string name { get; set; }
        public decimal amount { get; set; }
        public bool isDeductible { get; set; }
        public DateTime date { get; set; }
    }
}
