using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace pujcovnaHudebnichNastroju
{
    /// <summary>
    /// Interakční logika pro login.xaml
    /// </summary>
    public partial class login : Window
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wetter.Vitek\source\repos\pujcovnaHudebnichNastroju\pujcovnaHudebnichNastroju\pujcovnaDB.mdf;Integrated Security=True");
        public login()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            PujcovnaNastroju pn = new PujcovnaNastroju();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE jmeno=@j AND heslo=@h";
            cmd.Parameters.AddWithValue("@j", logJmeno.Text);
            cmd.Parameters.AddWithValue("@h", logHeslo.Password);
            SqlDataReader r = cmd.ExecuteReader();
            if (r.Read())
            {
                pn.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Nemáte oprávnění vstoupit.");
            }
            conn.Close();
        }
    }
}
