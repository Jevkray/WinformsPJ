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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace Praktika
{
    public partial class Form9 : Form
    {
        private DataTable _table;
        public MySqlConnection _mycon;
        public MySqlCommand _mycom;
        public string _access;
        private string _connectData = "Server=localhost;Database=mydb;Uid=root;pwd=12345;charset=utf8";
        public string value2;
        public string value2_1;

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
        public Form9()
        {
            InitializeComponent();
            InitializationTrans();
            InitializationArr();
        }
        public void InitializationArr()
        {
            _mycon = GetDBConnection();
            _table = new DataTable();
            ArrTMC();
            Adapter(comboBox3, "SELECT ID, Counterpartie FROM Counterparties", "Counterpartie", "id");
            Adapter(comboBox4, "SELECT ID, NameOfStorageLocation FROM StorageLocations", "NameOfStorageLocation", "id");
            Adapter(comboBox6, "SELECT ID, Name FROM Materials", "Name", "id");
        }

        public void InitializationTrans()
        {
            _mycon = GetDBConnection();
            _table = new DataTable();
            TransTMC();
            Adapter(comboBox3_1, "SELECT ID, Counterpartie FROM Counterparties", "Counterpartie", "id");
            Adapter(comboBox4_1, "SELECT ID, NameOfStorageLocation FROM StorageLocations", "NameOfStorageLocation", "id");
            Adapter(comboBox6_1, "SELECT ID, Name FROM Materials", "Name", "id");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Show();
        }

        private void dataGridView1_1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_1.Show();
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
        private void button1_Click(object sender, EventArgs e)
        {
            ArrTMC();
        }
        private void button1_1_Click(object sender, EventArgs e)
        {
            TransTMC();
        }
        private void Form9_Load(object sender, EventArgs e)
        {
            _access = Access.Accesses;
            Buttons(Access.Accesses);
        }
        private void ArrTMC()
        {
            string script = "Select arrivaltmc.id, arrivaltmc.Date as Дата, arrivaltmc.Number as Номер, arrivaltmc.Material as Материал, arrivaltmc.Count as Количество, arrivaltmc.EdIzmer as 'Единица измерения', arrivaltmc.Price as 'Цена за единицу', arrivaltmc.Counterparties as Контрагенты, arrivaltmc.StorageLocation as 'Место хранения', arrivaltmc.Sum as Сумма from arrivaltmc join Materials on materials.id = arrivaltmc.Material_id join Counterparties on counterparties.id = arrivaltmc.Counterparties_id join StorageLocations on storagelocations.id = arrivaltmc.StorageLocations_id";
            MSDataFill(script, _connectData, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void TransTMC()
        {
            string script = "Select transfertmc.id, transfertmc.Date as Дата, transfertmc.Number as Номер, transfertmc.Material as Материал, transfertmc.Count as Количество, transfertmc.EdIzmer as 'Единица измерения', transfertmc.Price as 'Цена за единицу', transfertmc.Counterparties as Контрагенты, transfertmc.StorageLocation as 'Место хранения', transfertmc.Sum as Сумма from transfertmc join Materials on materials.id = transfertmc.Material_id join Counterparties on counterparties.id = transfertmc.Counterparties_id join StorageLocations on storagelocations.id = transfertmc.StorageLocations_id";
            MSDataFill(script, _connectData, dataGridView1_1);
            dataGridView1_1.Columns[0].Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string script = $"insert into arrivaltmc(Date,Number,Material,Count,EdIzmer,Price,Counterparties,StorageLocation,Sum,Counterparties_id,StorageLocations_id,Material_id) value ('{dateTimePicker1.Value.ToString("yyyy.MM.dd").Substring(0, 10)}','{textBox2.Text}','{comboBox6.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}','{comboBox3.Text}','{comboBox4.Text}','{Convert.ToSingle(textBox3.Text) * Convert.ToSingle(textBox5.Text)}','{comboBox1.Text}','{comboBox2.Text}','{comboBox5.Text}')";
            MSDataFill(script, _connectData, dataGridView1);
            InitializationArr();

        }
        private void button2_1_Click(object sender, EventArgs e)
        {
            string script = $"insert into transfertmc(Date,Number,Material,Count,EdIzmer,Price,Counterparties,StorageLocation,Sum,Counterparties_id,StorageLocations_id,Material_id) value ('{dateTimePicker1_1.Value.ToString("yyyy.MM.dd").Substring(0, 10)}','{textBox2_1.Text}','{comboBox6_1.Text}','{textBox3_1.Text}','{textBox4_1.Text}','{textBox5_1.Text}','{comboBox3_1.Text}','{comboBox4_1.Text}','{Convert.ToSingle(textBox3_1.Text) * Convert.ToSingle(textBox5_1.Text)}','{comboBox1_1.Text}','{comboBox2_1.Text}','{comboBox5_1.Text}')";
            MSDataFill(script, _connectData, dataGridView1_1);
            InitializationTrans();

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

        private void button3_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить отчет?", "Удаление", MessageBoxButtons.YesNo);
                    if (zz == DialogResult.Yes)
                    {


                        string script = ($"DELETE FROM arrivaltmc WHERE ID = {value2} ");
                        MSDataFill(script, _connectData, dataGridView1);
                        InitializationArr();
                    }
                }
                catch { MessageBox.Show("Неверно введены данные"); }
            }
        }

        private void button3_1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить отчет?", "Удаление", MessageBoxButtons.YesNo);
                    if (zz == DialogResult.Yes)
                    {


                        string script = ($"DELETE FROM transfertmc WHERE ID = {value2_1} ");
                        MSDataFill(script, _connectData, dataGridView1_1);
                        InitializationTrans();
                    }
                }
                catch { MessageBox.Show("Неверно введены данные"); }
            }
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            value2 = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGridView1_1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            value2 = dataGridView1_1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string script = ($"UPDATE arrivaltmc SET Date = '{dateTimePicker1.Value.ToString("yyyy.MM.dd").Substring(0, 10)}', Number = '{textBox2.Text}', Material = '{comboBox6.Text}', Count = '{textBox3.Text}', EdIzmer = '{textBox4.Text}', Price = '{textBox5.Text}', Counterparties = '{comboBox3.Text}', StorageLocation = '{comboBox4.Text}', Sum = '{Convert.ToSingle(textBox3.Text) * Convert.ToSingle(textBox5.Text)}', Counterparties_id = '{comboBox1.Text}', StorageLocations_id = '{comboBox2.Text}', Material_id = '{comboBox5.Text}' WHERE ID = {value2}");
            MSDataFill(script, _connectData, dataGridView1);
            InitializationArr();
        }

        private void button4_1_Click(object sender, EventArgs e)
        {
            string script = ($"UPDATE transfertmc SET Date = '{dateTimePicker1_1.Value.ToString("yyyy.MM.dd").Substring(0, 10)}', Number = '{textBox2_1.Text}', Material = '{comboBox6_1.Text}', Count = '{textBox3_1.Text}', EdIzmer = '{textBox4_1.Text}', Price = '{textBox5_1.Text}', Counterparties = '{comboBox3_1.Text}', StorageLocation = '{comboBox4_1.Text}', Sum = '{Convert.ToSingle(textBox3_1.Text) * Convert.ToSingle(textBox5_1.Text)}', Counterparties_id = '{comboBox1_1.Text}', StorageLocations_id = '{comboBox2_1.Text}', Material_id = '{comboBox5_1.Text}' WHERE ID = {value2_1}");
            MSDataFill(script, _connectData, dataGridView1_1);
            InitializationTrans();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM counterparties WHERE Counterpartie='{comboBox3.Text}'", comboBox1, "arrivaltmc.id", "id");
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM StorageLocations WHERE NameOfStorageLocation='{comboBox4.Text}'", comboBox2, "arrivaltmc.id", "id");
        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM Materials WHERE Name='{comboBox6.Text}'", comboBox5, "arrivaltmc.id", "id");
        }
        private void comboBox3_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM counterparties WHERE Counterpartie='{comboBox3_1.Text}'", comboBox1_1, "transfertmc.id", "id");
        }
        private void comboBox4_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM StorageLocations WHERE NameOfStorageLocation='{comboBox4_1.Text}'", comboBox2_1, "transfertmc.id", "id");
        }
        private void comboBox6_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM Materials WHERE Name='{comboBox6_1.Text}'", comboBox5_1, "transfertmc.id", "id");
        }
    }
}

//ЦЕНА ЗА ЕДИНИЦУ ТОВАРА
