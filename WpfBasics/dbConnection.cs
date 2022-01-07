using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace WpfBasics
{
    public class dbConnection
    {
        public dbConnection()  // constructor Function
        {
            string connectionString = "Data Source=DANIEL-PC\\SQLEXPRESS;Initial Catalog=lifeApp;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            if (cnn.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection OPEN!");
                //cnn.Close();
                //Console.WriteLine("Connection Closed!");
            }
            else
            {
                Console.WriteLine("Connection FAILED!");
            }
        }

        public static SqlConnection cnn;
    }
}
