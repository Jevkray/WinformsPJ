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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Praktika
{
    public partial class Form6 : Form
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
        public Form6()
        {
            InitializeComponent();
            Initialization();
        }
        public void Initialization()
        {
            _mycon = GetDBConnection();
            _table = new DataTable();
            Packers();
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

        public string value2;
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
        private void button1_Click(object sender, EventArgs e)
        {
            Packers();
        }
        private void Packers()
        {
            string script = "Select id,PackerName as Заготовитель, Address as Адрес, Phone as Телефон from packers";
            MSDataFill(script, _connectData, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string script = $"insert into packers(PackerName,Address,Phone) value ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}')";
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить заготовителя?", "Удаление", MessageBoxButtons.YesNo);
                    if (zz == DialogResult.Yes)
                    {


                        string script = ($"DELETE FROM packers WHERE ID = {value2} ");
                        MSDataFill(script, _connectData, dataGridView1);
                        Initialization();
                    }
                }
                catch { MessageBox.Show("Неверно введены данные"); }
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            value2 = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string script = ($"UPDATE packers SET PackerName = '{textBox1.Text}',Address = '{textBox2.Text}', Phone = '{textBox3.Text}' WHERE ID = {value2}");
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            _access = Access.Accesses;
            Buttons(Access.Accesses);
        }
    }
}