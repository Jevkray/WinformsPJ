using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Windows.Forms;
//using Word = Microsoft.Office.Interop.Word;
//using Excel = Microsoft.Office.Interop.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Praktika
{
    public partial class Form2 : Form
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
        public Form2()
        {
            InitializeComponent();
            Initialization();
        }
        public void Initialization()
        {
            _mycon = GetDBConnection();
            _table = new DataTable();
            Employees();
            Adapter(comboBox3, "SELECT ID,PackerName  FROM packers", "PackerName", "id");
            Adapter(comboBox4, "SELECT ID,NameOfStorageLocation FROM StorageLocations", "NameOfStorageLocation", "id");
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
            Employees();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            _access = Access.Accesses;
            Buttons(Access.Accesses);
        }
        private void Employees()
        {
            string script = "Select employees.id,employees.FIO as ФИО, employees.Position as Должность, employees.Phone as Телефон, Packers.PackerName as Заготовитель, StorageLocations.NameOfStorageLocation as Название_Склада from employees join packers on packers.id = employees.Packers_id join StorageLocations on storagelocations.id = employees.StorageLocations_id";
            MSDataFill(script, _connectData, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string script = $"insert into employees(FIO,Position,Phone,Packers_id,StorageLocations_id) value ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}',{comboBox1.Text},{comboBox2.Text})";
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
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM packers WHERE PackerName='{comboBox3.Text}'", comboBox1, "id", "id");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM StorageLocations WHERE NameOfStorageLocation='{comboBox4.Text}'", comboBox2, "id", "id");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить сотрудника?", "Удаление", MessageBoxButtons.YesNo);
                    if (zz == DialogResult.Yes)
                    {


                        string script = ($"DELETE FROM employees WHERE ID = {value2} ");
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
            string script = ($"UPDATE employees SET FIO = '{textBox1.Text}',Position = '{textBox2.Text}', Phone = '{textBox3.Text}',Packers_id = '{comboBox1.Text}',StorageLocations_id = '{comboBox2.Text}' WHERE ID = {value2}");
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }
        private void MSDataAdapterFill(string cmdText, ComboBox comboBox = null, DataTable dataTable = null, string displayNubmer = null, string valueNumber = null)
        {
            try
            {
                MySqlConnection myConnection = GetDBConnection();
                {
                    _table = new DataTable();
                    MySqlCommand command = new MySqlCommand(cmdText, _mycon);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(_table);
                    //_table.Clear();
                }
                if (comboBox != null)
                {
                    comboBox.DataSource = dataTable;
                    comboBox.DisplayMember = displayNubmer;
                    comboBox.ValueMember = valueNumber;
                }
            }
            catch (Exception exeption)
            {
                MessageBox.Show("" + exeption);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string result = "";
            string item = comboBox5.Text;
            switch (item)
            {
                case "ФИО":
                    result = "employees.FIO";
                    break;
                case "Должность":
                    result = "employees.Position";
                    break;
                case "Телефон":
                    result = "employees.Phone";
                    break;
                case "Заготовитель":
                    result = "Packers.PackerName";
                    break;
                case "Склад":
                    result = "StorageLocations.NameOfStorageLocation";
                    break;
            }
            string script = $"Select employees.id,employees.FIO as ФИО, employees.Position as Должность, employees.Phone as Телефон, Packers.PackerName as Заготовитель, StorageLocations.NameOfStorageLocation as Название_Склада from employees join packers on packers.id = employees.Packers_id join StorageLocations on storagelocations.id = employees.StorageLocations_id  WHERE ((({result})Like \"%" + textBox4.Text + "%\"));";
            MSDataFill(script, _connectData, dataGridView1);
        }
    }
}
