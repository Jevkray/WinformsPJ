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
    }
}
