using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika
{
    public partial class Form11 : Form
    {
        private DataTable _table;
        public MySqlConnection _mycon;
        public MySqlCommand _mycom;
        public string _access;
        private string _connectData = "Server=localhost;Database=mydb;Uid=root;pwd=12345;charset=utf8";

        public DataSet ds;
        private void Buttons(string _access)
        {
            _access = this._access;
            switch (_access)
            {
                case "Администратор":
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;

                    break;
                case "Директор":
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    break;
                case "Главный бухгалтер":
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = true;
                    break;
                case "Оператор ЭВМ":
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    break;
                case "Кладовщик":
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    break;
            }
        }
        public Form11()
            { 
                InitializeComponent();
                Initialization();
            }
        public void Initialization()
            {
                _mycon = GetDBConnection();
                _table = new DataTable();
                Ychet();
            }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                dataGridView1.Show();
            }
        public static MySqlConnection GetDBConnection()
            {

                string host = "localhost";
                int port = 3306;
                string database = "mydb";
                string username = "root";
                string password = "12345";
                try
                {
                    return GetDBConnection(host, port, database, username, password);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                return null;
            }
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
            {
                String connString = "Server=" + host + ";Database=" + database
                    + ";port=" + port + ";User Id=" + username + ";password=" + password;

                MySqlConnection SqlConnection = new MySqlConnection(connString);

                return SqlConnection;
            }
         public void MSDataFill(string script, string connect, DataGridView dataGridView)
            {
                try
                {
                    _table = new DataTable();
                    MySqlDataAdapter ms_data = new MySqlDataAdapter(script, _mycon);
                    ms_data.Fill(_table);
                    dataGridView.DataSource = _table;
                    _mycon.Close();
                    //_table.Clear();
                }
                catch (Exception exeption)
                {
                    MessageBox.Show("" + exeption);
                }
            }

            private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ychet();
        }
        private void Ychet()
        {
            string script = "Select id,Code as Код,Name as Наименование,EdIzmer as Единица_Измерения,Cost as Цена, RemainEndPer as Остаток, Amount as Количество, Sum as Сумма from RemainingStock";
            MSDataFill(script, _connectData, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
