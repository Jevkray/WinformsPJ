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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DataTable _table;
        public MySqlConnection _mycon;
        public MySqlCommand _mycom;
        private string _connectData = "Server=localhost;Database=ychet;Uid=root;pwd=12345;charset=utf8";
        public static void Adapter(ComboBox comboBox, string script, string member, string value)
        {
            string connStr1 = "Server=localhost;Database=YCHET;Uid=root;pwd=12345;charset=utf8";
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
        private void справочникиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void местаХраненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
        }

        private void контрагентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
        }

        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.Show();
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void заготовительToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 f = new Form8();
            f.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void тТНToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void учетТМЦToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form9 f = new Form9();
            f.Show();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void складскойУчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            f.Show();
        }

        private void отчетПоТМЦToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form11 f = new Form11();
            f.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Form11 f = new Form11();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
