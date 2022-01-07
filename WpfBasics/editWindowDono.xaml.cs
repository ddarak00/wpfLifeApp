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
    /// Interaction logic for editWindowDono.xaml
    /// </summary>
    public partial class editWindowDono : Window
    {
        public editWindowDono(Donation selectedObject)
        {
            InitializeComponent();
            editNameDono.Text = selectedObject.name;
            editAmount.Text = selectedObject.amount.ToString();
            deductibleInput.IsChecked = selectedObject.isDeductible;
            dateInputDono.Text = selectedObject.date.ToString();

            originalName = selectedObject.name;
            originalAmount = selectedObject.amount;
            origIsDeductible = selectedObject.isDeductible;
            origDate = selectedObject.date;

    }

        private void editDonationButton_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand editcmd = new SqlCommand();
            editcmd.Connection = dbConnection.cnn;
            editcmd.CommandText = "Update donations SET name=@name, amount=@amount, deductible=@deductible, date=@date WHERE date=@origDate AND name=@origname AND amount=@origAmount AND deductible=@origDeductible";
            editcmd.Parameters.AddWithValue("@name", editNameDono.Text);
            editcmd.Parameters.AddWithValue("@amount", editAmount.Text);
            editcmd.Parameters.AddWithValue("@deductible", deductibleInput.IsChecked);
            editcmd.Parameters.AddWithValue("@date", dateInputDono.Text);

            editcmd.Parameters.AddWithValue("@origname", originalName);
            editcmd.Parameters.AddWithValue("@origAmount", originalAmount);
            editcmd.Parameters.AddWithValue("@origDeductible", origIsDeductible);
            editcmd.Parameters.AddWithValue("@origDate", origDate);

            if (editcmd.ExecuteNonQuery() == 1)
            {
                Donations fm = Application.Current.Windows.OfType<Donations>().Take(1).SingleOrDefault();
                if (fm != null)
                {
                    Console.WriteLine("Detecting a Donations Window");
                    fm.RefreshList();
                }
                else
                {
                    Console.WriteLine("Did Not find a Donations Window!");
                }
                Console.WriteLine("Updated.");
                Close();
            }
            else
            {
                Console.WriteLine("Donations Not Updated!");
            }
        }

        /*public string name { get; set; }
        public decimal amount { get; set; }
        public bool isDeductible { get; set; }
        public DateTime date { get; set; }*/

        //private void cancelEditWindowButton_Click(object sender, RoutedEventArgs e)
        /// {
        //    Close();
        // }

        private void DonoCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DonoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void cancelEditButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dateBoxDono_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        string originalName;
        decimal originalAmount;
        bool origIsDeductible;
        public DateTime origDate;
    }
}
