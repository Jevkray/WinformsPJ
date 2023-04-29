﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Praktika
{
    public partial class Form3 : Form
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
        public Form3()
        {
            InitializeComponent();
            Initialization();
        }
        public void Initialization()
        {
            _mycon = GetDBConnection();
            _table = new DataTable();
            Storagelocations();
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
            Storagelocations();
        }
        private void Storagelocations()
        {
            string script = "Select id,NameOfStorageLocation as Название_Склада, FinanciallyResponsiblePerson as ФОЛ, WarehouseAddress as Адрес, ProhibitShipmentsFromTheWarehouse as Запретить_Поставку_Со_Склада, ForGoodsAtFixedPricesComingInWholesaleAndSoldThroughRetail as Фиксированная_Стоимость, WarehouseType as Тип_Склада from storagelocations";
            MSDataFill(script, _connectData, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            _access = Access.Accesses;
            Buttons(Access.Accesses);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string script = $"insert into storagelocations(NameOfStorageLocation,FinanciallyResponsiblePerson,WarehouseAddress,ProhibitShipmentsFromTheWarehouse,ForGoodsAtFixedPricesComingInWholesaleAndSoldThroughRetail,WarehouseType) value ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}','{textBox6.Text}')";
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

        private void button3_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить склад хранения?", "Удаление", MessageBoxButtons.YesNo);
                    if (zz == DialogResult.Yes)
                    {


                        string script = ($"DELETE FROM storagelocations WHERE ID = {value2} ");
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
            string script = ($"UPDATE storagelocations SET NameOfStorageLocation = '{textBox1.Text}',FinanciallyResponsiblePerson = '{textBox2.Text}', WarehouseAddress = '{textBox3.Text}', ProhibitShipmentsFromTheWarehouse = '{textBox4.Text}', ForGoodsAtFixedPricesComingInWholesaleAndSoldThroughRetail = '{textBox5.Text}', WarehouseType = '{textBox6.Text}' WHERE ID = {value2}");
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string result = "";
            string item = comboBox1.Text;
            switch (item)
            {
                case "Название склада":
                    result = "NameOfStorageLocation";
                    break;
                case "ФОЛ":
                    result = "FinanciallyResponsiblePerson";
                    break;
                case "Адрес":
                    result = "WarehouseAddress";
                    break;
                case "Запреты":
                    result = "ProhibitShipmentsFromTheWarehouse";
                    break;
                case "Фиксированная стоимость":
                    result = "ForGoodsAtFixedPricesComingInWholesaleAndSoldThroughRetail";
                    break;
                case "Тип склада":
                    result = "WarehouseType";
                    break;
            }
            string script = $"Select id,NameOfStorageLocation as Название_Склада, FinanciallyResponsiblePerson as ФОЛ, WarehouseAddress as Адрес, ProhibitShipmentsFromTheWarehouse as Запретить_Поставку_Со_Склада, ForGoodsAtFixedPricesComingInWholesaleAndSoldThroughRetail as Фиксированная_Стоимость, WarehouseType as Тип_Склада from storagelocations  WHERE ((({result})Like \"%" + textBox7.Text + "%\"));";
            MSDataFill(script, _connectData, dataGridView1);
        }
    }
}
