using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pujcovnaHudebnichNastroju
{
    /// <summary>
    /// Interakční logika pro PujcovnaNastroju.xaml
    /// </summary>
    public partial class PujcovnaNastroju : Window
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Wetter.Vitek\source\repos\pujcovnaHudebnichNastroju\pujcovnaHudebnichNastroju\pujcovnaDB.mdf;Integrated Security=True");
        public PujcovnaNastroju()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ZobrazData();
            ZobrazVypujcky();
        }

        public void ZobrazVypujcky()
        {
            List<Vypujcky> vypujcky = new List<Vypujcky>();
            conn.Open();
            SqlCommand cmdVypujcky = conn.CreateCommand();
            cmdVypujcky.CommandText = "SELECT idVypujcky, nazev, typ, prijmeni, datumVypujceni, datumVraceni FROM Vypujcky, zakaznici, nastroje WHERE keyNastroje=idNastroje AND keyZakaznici=idZakaznici;";
            SqlDataReader rVypujcky = cmdVypujcky.ExecuteReader();
            while (rVypujcky.Read())
            {
                vypujcky.Add(new Vypujcky(
                    rVypujcky.GetInt32(0),
                    rVypujcky.GetString(1),
                    rVypujcky.GetString(2),
                    rVypujcky.GetString(3),
                    rVypujcky.GetDateTime(4),
                    rVypujcky.GetDateTime(5)));
            }
            lsvVypujcky.ItemsSource = vypujcky;
            conn.Close();

            List<Statistiky> statistiky = new List<Statistiky>();
            conn.Open();
            SqlCommand cmdStatistiky = conn.CreateCommand();
            cmdStatistiky.CommandText = "SELECT typ, COUNT(*) FROM Vypujcky, nastroje WHERE keyNastroje=nastroje.idNastroje GROUP BY nastroje.typ;";
            SqlDataReader rStatistiky = cmdStatistiky.ExecuteReader();
            while (rStatistiky.Read())
            {
                statistiky.Add(new Statistiky(
                    rStatistiky.GetString(0),
                    rStatistiky.GetInt32(1)));
            }
            lsvStatistiky.ItemsSource = statistiky;
            conn.Close();
        }

        public void ZobrazData()
        {
            List<nastroje> nastrojeN = new List<nastroje>();
            conn.Open();
            SqlCommand cmdNastroje = conn.CreateCommand();
            cmdNastroje.CommandText = "SELECT * FROM nastroje;";
            SqlDataReader rNastroje = cmdNastroje.ExecuteReader();
            while (rNastroje.Read())
            {
                nastrojeN.Add(new nastroje(
                    rNastroje.GetInt32(0),
                    rNastroje.GetString(1),
                    rNastroje.GetString(2)));
            }
            lsvNastrojeVypujcky.ItemsSource = nastrojeN;
            lsvNastroje.ItemsSource = nastrojeN;
            conn.Close();

            List<zakaznici> zakazniciZ = new List<zakaznici>();
            conn.Open();
            SqlCommand cmdZakaznici = conn.CreateCommand();
            cmdZakaznici.CommandText = "SELECT * FROM zakaznici;";
            SqlDataReader rZakaznici = cmdZakaznici.ExecuteReader();
            while (rZakaznici.Read()) 
            {
                zakazniciZ.Add(new zakaznici(
                    rZakaznici.GetInt32(0),
                    rZakaznici.GetString(1),
                    rZakaznici.GetString(2),
                    rZakaznici.GetInt32(3)));
            }
            lsvZakazniciVypujcky.ItemsSource = zakazniciZ;
            lsvZakaznici.ItemsSource = zakazniciZ;
            conn.Close();
        }
        private void btnPridat_Click(object sender, RoutedEventArgs e)
        {
            if (pridavaniNazev.Text != "" && pridavaniTyp.Text != "")
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO nastroje (nazev, typ) VALUES (@nazev, @typ);";
                cmd.Parameters.AddWithValue("@nazev", pridavaniNazev.Text);
                cmd.Parameters.AddWithValue("@typ", pridavaniTyp.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                ZobrazData();
            }
        }

        private void btnPridatZakaznik_Click(object sender, RoutedEventArgs e)
        {
            if (pridavaniJmeno.Text != "" && pridavaniPrijmeni.Text != "" && pridavaniTelefon.Text != "")
            {
                int a;
                if(int.TryParse(pridavaniTelefon.Text, out a))
                {
                    if (a.ToString().Length == 9)
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = "INSERT INTO zakaznici (jmeno, prijmeni, telefon) VALUES (@jmeno, @prijmeni, @telefon);";
                        cmd.Parameters.AddWithValue("@jmeno", pridavaniJmeno.Text);
                        cmd.Parameters.AddWithValue("@prijmeni", pridavaniPrijmeni.Text);
                        cmd.Parameters.AddWithValue("@telefon", pridavaniTelefon.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        ZobrazData();
                    }
                    else
                    {
                        MessageBox.Show("Zkontrolujte zda telefonní číslo má správnou délku.");
                    }
                }
                else
                {
                    MessageBox.Show("Zkontrolujte zda telefonní číslo neobsahuje speciální znaky nebo nemá správnou délku.");
                }
            }
        }

        private void btnSmazat_Click(object sender, RoutedEventArgs e)
        {
            if(lsvNastroje.SelectedItems.Count > 0)
            {
                try
                {
                    nastroje Nastroje = (nastroje)lsvNastroje.SelectedItem;
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM nastroje WHERE idNastroje=@id;";
                    cmd.Parameters.AddWithValue("@id", Nastroje.id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    ZobrazData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Tento nástroj je zaregistrovaný ve vypůjčkách, nelze smazat.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnSmazatZakaznik_Click(object sender, RoutedEventArgs e)
        {
            if(lsvZakaznici.SelectedItems.Count > 0)
            {
                try
                {
                    zakaznici Zakaznici = (zakaznici)lsvZakaznici.SelectedItem;
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "DELETE FROM zakaznici WHERE idZakaznici=@id;";
                    cmd.Parameters.AddWithValue("@id", Zakaznici.id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    ZobrazData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Tento zákazník je zaregistrovaný ve vypůjčkách, nelze smazat.");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lsvNastroje.SelectedItems.Count > 0)
            {
                nastroje Nastroje = (nastroje)lsvNastroje.SelectedItem;
                int id = Nastroje.id;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE nastroje SET nazev=@nazev, typ=@typ WHERE idNastroje=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nazev", editaceNazev.Text);
                cmd.Parameters.AddWithValue("@typ", editaceTyp.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                ZobrazData();
                ZobrazVypujcky();
            }
            else 
            {
                MessageBox.Show("Prvně vyberte položku.");
            }
        }

        private void lsvNastroje_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lsvNastroje.SelectedItems.Count > 0) 
            {
                nastroje Nastroje = (nastroje)lsvNastroje.SelectedItem;
                editaceNazev.Text = Nastroje.nazev;
                editaceTyp.Text = Nastroje.typ;
            }
        }

        private void btnEditZakaznik_Click(object sender, RoutedEventArgs e)
        {
            if (lsvZakaznici.SelectedItems.Count > 0)  
            {
                zakaznici Zakaznici = (zakaznici)lsvZakaznici.SelectedItem;
                int id = Zakaznici.id;
                int a;
                if (int.TryParse(editaceTelefon.Text, out a))
                {
                    if (a.ToString().Length == 9)
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = "UPDATE zakaznici SET jmeno=@jmeno, prijmeni=@prijmeni, telefon=@telefon WHERE idZakaznici=@id;";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@jmeno", editaceJmeno.Text);
                        cmd.Parameters.AddWithValue("@prijmeni", editacePrijmeni.Text);
                        cmd.Parameters.AddWithValue("@telefon", editaceTelefon.Text);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        ZobrazData();
                        ZobrazVypujcky();
                    }
                    else
                    {
                        MessageBox.Show("Zkontrolujte zda telefonní číslo má správnou délku.");
                    }
                }
                else
                {
                    MessageBox.Show("Zkontrolujte zda telefonní číslo neobsahuje speciální znaky nebo nemá správnou délku.");
                }
            }
            else
            {
                MessageBox.Show("Prvně vyberte položku.");
            }
        }

        private void lsvZakaznici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvZakaznici.SelectedItems.Count > 0) 
            {
                zakaznici Zakaznici = (zakaznici)lsvZakaznici.SelectedItem;
                editaceJmeno.Text = Zakaznici.jmeno;
                editacePrijmeni.Text = Zakaznici.prijmeni;
                editaceTelefon.Text = Zakaznici.telefon.ToString();
            }
        }

        private void btnPridatVypujcku_Click(object sender, RoutedEventArgs e)
        {
            zakaznici Zakaznici = (zakaznici)lsvZakazniciVypujcky.SelectedItem;
            nastroje Nastroje = (nastroje)lsvNastrojeVypujcky.SelectedItem;
            if (lsvZakazniciVypujcky.SelectedItems.Count > 0 && lsvNastrojeVypujcky.SelectedItems.Count > 0)
            {
                conn.Open();
                SqlCommand cmdVypujcky = conn.CreateCommand();
                cmdVypujcky.CommandText = "INSERT INTO Vypujcky (keyNastroje, keyZakaznici, datumVypujceni, datumVraceni) VALUES (@keyNastroje, @keyZakaznici, @datumVypujceni, @datumVraceni);";
                cmdVypujcky.Parameters.AddWithValue("@keyNastroje", Nastroje.id);
                cmdVypujcky.Parameters.AddWithValue("@keyZakaznici", Zakaznici.id);
                cmdVypujcky.Parameters.AddWithValue("@datumVypujceni", DateTime.Parse(clrZacatekVypujcky.SelectedDate.ToString()));
                cmdVypujcky.Parameters.AddWithValue("@datumVraceni", DateTime.Parse(clrKonecVypujcky.SelectedDate.ToString()));
                cmdVypujcky.ExecuteNonQuery();
                conn.Close();
                ZobrazVypujcky();
            }
        }

        private void btnOdebratVypujcku_Click(object sender, RoutedEventArgs e)
        {
            if (lsvVypujcky.SelectedItems.Count > 0)
            {
                Vypujcky vypujcky = (Vypujcky)lsvVypujcky.SelectedItem;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM Vypujcky WHERE idVypujcky=@id;";
                cmd.Parameters.AddWithValue("@id", vypujcky.id);
                cmd.ExecuteNonQuery();
                conn.Close();
                ZobrazVypujcky();
            }
        }
    }
}
