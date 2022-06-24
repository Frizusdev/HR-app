using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Osoby
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        void pobierzdane()
        {
            string connection = "server=localhost;port=3306;username=root;password=;database=pracownicy";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM osoby ";

            try
            {
                con.Open();
                command.ExecuteNonQuery();
                MySqlDataAdapter adap = new MySqlDataAdapter(command);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataView.DataSource = ds.Tables[0].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        void dodajpracownika()
        {
            if (name.Text != "" && surname.Text != "" && stage.Text != "" && tel.Text != "" && dniur.Text != "" && salary.Text != "")
            {
                string connection = "server=localhost;port=3306;username=root;password=;database=pracownicy";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlCommand command = con.CreateCommand();
                command.CommandText = "INSERT INTO `osoby`" +
                    "(`Imię`, `Nazwisko`, `Stanowisko`, `Data_Urodzenia`, `Data_Zatrudnienia`, `Telefon`, `Data_Rozpoczęcia_Urlopu`, `Data_Zakończenia_Urlopu`, `Dni_Urlopu`, `Stawka`) " +
                    "VALUES ('" + name.Text + "','" + surname.Text + "','" + stage.Text + "','" + dataur.Value.ToString() + "','" + datazat.Value.ToString() + "','" + tel.Text + "','" + dataurstart.Value.ToString() + "','" + dataurend.Value.ToString() + "','" + dniur.Text + "','" + salary.Text + "');";

                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void usunpracownika()
        {
            string connection = "server=localhost;port=3306;username=root;password=;database=pracownicy";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "DELETE FROM `osoby` WHERE ID = "+id.Text+"";

            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void zmianapracownik()
        {
            string connection = "server=localhost;port=3306;username=root;password=;database=pracownicy";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "UPDATE `osoby` SET `Imię`='" + namenew.Text + "',`Nazwisko`='" + surnamenew.Text + "',`Stanowisko`='" + stagenew.Text + "',`Data_Urodzenia`='" + dataurnew.Value.ToString() + "',`Data_Zatrudnienia`='" + datazatnew.Value.ToString() + "',`Telefon`='" + telnew.Text + "',`Data_Rozpoczęcia_Urlopu`='" + dataurstartnew.Value.ToString() + "',`Data_Zakończenia_Urlopu`='" + dataurendnew.Value.ToString() + "',`Dni_Urlopu`='" + dniurnew.Text + "',`Stawka`='" + salarynew.Text + "' WHERE ID=" + id.Text + "";

            try
            {
                con.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void szukaj()
        {
            string connection = "server=localhost;port=3306;username=root;password=;database=pracownicy";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM `osoby` where id=" + id.Text;
            con.Open();
            MySqlDataReader r = command.ExecuteReader();

            try
            {
                while(r.Read())
                {
                    string date;
                    namenew.Text = r["Imię"].ToString();
                    surnamenew.Text = r["Nazwisko"].ToString();
                    stagenew.Text = r["Stanowisko"].ToString();
                    date = r["Data_Urodzenia"].ToString();
                    dataurnew.Value = Convert.ToDateTime(date);
                    //dataurnew.Value = (DateTime)r["Data_Urodzenia"];
                    date = r["Data_Zatrudnienia"].ToString();
                    datazatnew.Value = Convert.ToDateTime(date);
                    //datazatnew.Value = (DateTime)r["Data_Zatrudnienia"];
                    telnew.Text = r["Telefon"].ToString();
                    //dataurstartnew.Value = (DateTime)r["Data_Rozpoczęcia_Urlopu"];
                    date = r["Data_Rozpoczęcia_Urlopu"].ToString();
                    dataurstartnew.Value = Convert.ToDateTime(date);
                    //dataurendnew.Value = (DateTime)r["Data_Zakonczenia_Urlopu"];
                    date = r["Data_Zakończenia_Urlopu"].ToString();
                    dataurendnew.Value = Convert.ToDateTime(date);
                    dniurnew.Text = r["Dni_Urlopu"].ToString();
                    salarynew.Text = r["Stawka"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            r.Close();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pobierzdane();
        }

        private void add_Click(object sender, EventArgs e)
        {
            dodajpracownika();
            pobierzdane();
        }

        private void del_Click(object sender, EventArgs e)
        {
            usunpracownika();
            pobierzdane();
        }

        private void zmien_Click(object sender, EventArgs e)
        {
            zmianapracownik();
            pobierzdane();
        }

        private void search_Click(object sender, EventArgs e)
        {
            szukaj();
        }
    }
}
