using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;

namespace Osoby
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=localhost;port=3306;username=root;password=;");

        private void zaloguj_Click(object sender, EventArgs e)
        {
            try
            {
                if (login.Text != "" && haslo.Text != "")
                {

                    con.Open();
                    MySqlDataReader row;
                    MySqlCommand command = con.CreateCommand();
                    con.ChangeDatabase("pracownicy");
                    command.CommandText = "select login,has³o from haselka WHERE Login ='" + login.Text + "' AND Has³o ='" + haslo.Text + "'";
                    row = command.ExecuteReader(); ;
                    if (row.HasRows)
                    {
                        Form2 f = new Form2();
                        f.ShowDialog();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Data not found", "Information");
                    }
                }
                else
                {
                    MessageBox.Show("Username or Password is empty", "Information");
                }
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Potrzebujesz pomocy? Dzwoñ po IT i tak nie pomo¿e :) ", "Information");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pracownicy
            con.Open();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "CREATE DATABASE IF NOT EXISTS `pracownicy`";
            int resultp = command.ExecuteNonQuery();
            if (resultp > 0)
            {
                con.ChangeDatabase("pracownicy");
                command.CommandText = "CREATE TABLE `haselka` (`Login` TEXT NOT NULL , `Has³o` TEXT NOT NULL ) ENGINE = InnoDB; INSERT INTO `haselka`(`Login`, `Has³o`) VALUES ('admin','admin1')";
                command.ExecuteNonQuery();
            }
            con.Close();
        }
    }
}   