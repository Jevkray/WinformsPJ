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
using ComboBox = System.Windows.Forms.ComboBox;

namespace Praktika
{
    public partial class Form4 : Form
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
        public Form4()
        {
            InitializeComponent();
            Initialization();
        }
        public void Initialization()
        {
            _mycon = GetDBConnection();
            _table = new DataTable();
            Counterparties();
            Adapter(comboBox2, "SELECT ID,FIO  FROM employees", "FIO", "id");
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
            Counterparties();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            _access = Access.Accesses;
            Buttons(Access.Accesses);
        }
        private void Counterparties()
        {
            string script = "Select counterparties.id,counterparties.Counterpartie as Контрагент, counterparties.FullName as Наименование, counterparties.Adress as Адрес, counterparties.Telephone as Телефон, counterparties.MailAdress as Почтовый_адрес, counterparties.YNP as УНП, counterparties.OKULP as ОКЮЛП, counterparties.OKPO as ОКПО, counterparties.OKED as ОКЭД, counterparties.Direсtor as Руководитель, Employees.FIO as Сотрудник from counterparties join employees on employees.id = counterparties.Employees_id";
            MSDataFill(script, _connectData, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string script = $"insert into counterparties(Counterpartie,FullName,Adress,Telephone,MailAdress,YNP,OKULP,OKPO,OKED,Director,Employees_id) value ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}',{textBox6.Text},{textBox7.Text},{textBox8.Text},{textBox9.Text},'{textBox10.Text}',{comboBox1.Text})";
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }
        public static void MSAdapter(string script, ComboBox comboBox, string member, string value)
        {
            string connStr1 = "Server=localhost;Database=mydb;Uid=root;pwd=12345;charset=utf8";
            DataTable patientTable = new DataTable();
            MySqlConnection myConnection = new MySqlConnection(connStr1);
            {
                MySqlCommand command = new MySqlCommand(script, myConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(patientTable);
            }
            comboBox.DataSource = patientTable;
            comboBox.DisplayMember = member;
            comboBox.ValueMember = value;
        }
        public static void Adapter(ComboBox comboBox, string script, string member, string value)
        {
            string connStr1 = "Server=localhost;Database=mydb;Uid=root;pwd=12345;charset=utf8";
            DataTable patientTable = new DataTable();
            MySqlConnection myConnection = new MySqlConnection(connStr1);
            {
                MySqlCommand command = new MySqlCommand(script, myConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(patientTable);
            }
            comboBox.DataSource = patientTable;
            comboBox.DisplayMember = member;
            comboBox.ValueMember = value;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM employees WHERE FIO='{comboBox2.Text}'", comboBox1, "id", "id");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить контрагента?", "Удаление", MessageBoxButtons.YesNo);
                    if (zz == DialogResult.Yes)
                    {


                        string script = ($"DELETE FROM counterparties WHERE ID = {value2} ");
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
            string script = ($"UPDATE counterparties SET Counterpartie = '{textBox1.Text}',FullName = '{textBox2.Text}',Adress = '{textBox3.Text}', Telephone = '{textBox4.Text}, MailAdress = '{textBox5.Text}', YNP = {textBox6.Text}, OKULP = {textBox7.Text}, OKPO = {textBox8.Text}, OKED = {textBox9.Text}, Director = '{textBox10.Text}','Employees_id = '{comboBox1.Text}' WHERE ID = {value2}");
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }
    }
}

