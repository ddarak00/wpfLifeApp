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
    /// Interaction logic for editWindow.xaml
    /// </summary>
    public partial class editWindow : Window
    {
        public editWindow(Contact selectedObject)
        {
            InitializeComponent();
            editName.Text = selectedObject.name;
            editNumber.Text = selectedObject.number;
            originalName = selectedObject.name;
            originalNumber = selectedObject.number;
        }

        private void editContactButton_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand editcmd = new SqlCommand();
            editcmd.Connection = dbConnection.cnn;
            editcmd.CommandText = "Update phoneBook SET number =@number, name=@name WHERE number=@orignumber AND name=@origname";
            editcmd.Parameters.AddWithValue("@name", editName.Text);
            editcmd.Parameters.AddWithValue("@number", editNumber.Text);
            editcmd.Parameters.AddWithValue("@origname", originalName);
            editcmd.Parameters.AddWithValue("@orignumber", originalNumber);

            if (editcmd.ExecuteNonQuery() == 1)
            {
                //newName = editName.Text;
                //newNumber = editNumber.Text;
                PhoneSearchWindow fm = Application.Current.Windows.OfType<PhoneSearchWindow>().Take(1).SingleOrDefault();
                if (fm != null)
                {
                    Console.WriteLine("Detecting a PhoneSearchWindow");
                    fm.RefreshList();
                }
                else
                {
                    Console.WriteLine("Did Not find a PhoneSearchWindow!");
                }
                Console.WriteLine("Updated.");
            }
            else
            {
                Console.WriteLine("Not Updated!");
            }
        }

        private void cancelEditWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        string originalName;
        string originalNumber;
        string newName;
        string newNumber;
    }
}
