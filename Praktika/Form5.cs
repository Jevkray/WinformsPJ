using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Praktika
{
    public partial class Form5 : Form
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
        public Form5()
        {
            InitializeComponent();
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Initialization();
        }
        public void Initialization()
        {
            _mycon = GetDBConnection();
            _table = new DataTable();
            Materials();
            Adapter(comboBox2, "SELECT ID,PackerName  FROM packers", "PackerName", "id");
            Adapter(comboBox3, "SELECT ID,NameOfStorageLocation FROM StorageLocations", "NameOfStorageLocation", "id");
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
            Materials();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            _access = Access.Accesses;
            Buttons(Access.Accesses);
        }
        private void Materials()
        {
            string script = "Select materials.id,materials.Name as Название, materials.Price as Цена, materials.UnitOfMeasurement as Единица_Измерения, materials.DiscountPrice as Цена_Со_Скидкой, StorageLocations.NameOfStorageLocation as Склад, Packers.PackerName as Заготовитель from materials join packers on packers.id = materials.Packers_id join StorageLocations on storagelocations.id = materials.StorageLocations_id";
            MSDataFill(script, _connectData, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string script = $"insert into materials(Name,Price,UnitOfMeasurement,DiscountPrice,StorageLocations_id,Packers_id) value ('{textBox1.Text}',{textBox2.Text},'{comboBox1.Text}',{textBox4.Text},{comboBox4.Text},{comboBox5.Text})";
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

        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM packers WHERE PackerName='{comboBox2.Text}'", comboBox4, "id", "id");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM StorageLocations WHERE NameOfStorageLocation='{comboBox3.Text}'", comboBox5, "id", "id");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить материал?", "Удаление", MessageBoxButtons.YesNo);
                    if (zz == DialogResult.Yes)
                    {


                        string script = ($"DELETE FROM materials WHERE ID = {value2} ");
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
            string script = ($"UPDATE materials SET Name = '{textBox1.Text}',Price = '{textBox2.Text}', UnitOfMeasurement = '{comboBox1.Text}', DiscountPrice = '{textBox4.Text}' ,StorageLocations_id = '{comboBox5.Text}', Packers_id = '{comboBox4.Text}' WHERE ID = {value2}");
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string result = "";
            string item = comboBox6.Text;
            switch (item)
            {
                case "Название":
                    result = "materials.Name";
                    break;
                case "Цена":
                    result = "materials.Price";
                    break;
                case "Единица Измерения":
                    result = "materials.UnitOfMeasurement";
                    break;
                case "Цена Со Скидкой":
                    result = "materials.DiscountPrice";
                    break;
                case "Склад":
                    result = "StorageLocations.NameOfStorageLocation";
                    break;
                case "Заготовитель":
                    result = "Packers.PackerName";
                    break;
            }
            string script = $"Select materials.id,materials.Name as Название, materials.Price as Цена, materials.UnitOfMeasurement as Единица_Измерения, materials.DiscountPrice as Цена_Со_Скидкой, StorageLocations.NameOfStorageLocation as Склад, Packers.PackerName as Заготовитель from materials join packers on packers.id = materials.Packers_id join StorageLocations on storagelocations.id = materials.StorageLocations_id  WHERE ((({result})Like \"%" + textBox3.Text + "%\"));";
            MSDataFill(script, _connectData, dataGridView1);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

