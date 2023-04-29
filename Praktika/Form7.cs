using MySql.Data.MySqlClient;
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
    public partial class Form7 : Form
    {
        private const string CMD_TEXT = "SELECT * FROM user WHERE Login = @uL AND Password = @uP";
        private Autorizator _autoriazator;
        private DataTable _table;
        private MySqlDataAdapter _adapter;
        private string _login;
        static private string _password;
        static private string _access;
        static public string Ac { set; get; }
        public Form7()
        {
            InitializeComponent();
            Initialization();
        }
        private void Initialization()
        {
            _autoriazator = new Autorizator();
            _table = new DataTable();
            _adapter = new MySqlDataAdapter();
        }
        private void Verification()
        {

            _login = comboBox1.Text;
            _password = textBox1.Text;

            MySqlCommand command = new MySqlCommand(CMD_TEXT, _autoriazator.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = _login;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = _password;
            _adapter.SelectCommand = command;
            _adapter.Fill(_table);
            MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=mydb;Uid=root;pwd=12345;charset=utf8");
            _connection.Open();

            MySqlCommand command1 = new MySqlCommand($"SELECT access FROM user WHERE Login =  '{comboBox1.Text}' AND Password = '{textBox1.Text}' ", _connection);

            MySqlDataReader reader = command1.ExecuteReader();
            while (reader.Read())
            {
                _access = reader.GetValue(0).ToString();

            }
            Access.Accesses = comboBox1.Text;
            Access.Password = _password;
            _connection.Close();
            if (_table.Rows.Count > 0)
            {

                OpenForm(new Form1());
            }
            else
                MessageBox.Show("Ошибка! Не верно введен пароль или логин!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _access = comboBox1.Text;
            Verification();
        }
        private void OpenForm(Form form)
        {
            Hide();
            form.Show();
        }
        class Autorizator
        {
            private MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=mydb;Uid=root;pwd=12345;charset=utf8");
            public MySqlConnection GetConnection() => _connection;

            private void OpenConnection()
            {
                if (_connection.State == System.Data.ConnectionState.Closed)
                    _connection.Open();
            }
            private void CloseConnection()
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
