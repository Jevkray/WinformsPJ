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
using System.Data.SqlClient;

//using Word = Microsoft.Office.Interop.Word;
//using Excel = Microsoft.Office.Interop.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Praktika
{
    public partial class Form10 : Form
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
        public Form10()
        {
            InitializeComponent();
            Initialization();
        }
        public void Initialization()
        {
            _mycon = GetDBConnection();
            _table = new DataTable();
            TMCInfo();
            Adapter(comboBox1, "SELECT ID,NameOfStorageLocation FROM StorageLocations", "NameOfStorageLocation", "id");
            Adapter(comboBox2, "SELECT ID,Counterpartie FROM Counterparties", "Counterpartie", "id");
            Adapter(comboBox3, "SELECT ID,FIO FROM employees", "FIO", "id");
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
            TMCInfo();
        }
        private void Form10_Load(object sender, EventArgs e)
        {
            _access = Access.Accesses;
            Buttons(Access.Accesses);
        }
        private void TMCInfo()
        {
            string script = "Select tmcinfo.id, tmcinfo.Period as Период, tmcinfo.StorageLocation as Склад_Хранения, tmcinfo.MOL as МОЛ, tmcinfo.Counterparties as Контрагенты, tmcinfo.Employee as Работник from tmcinfo join StorageLocations on storagelocations.id = tmcinfo.StorageLocations_id join Counterparties on counterparties.id = tmcinfo.Counterparties_id join Employees on employees.id = tmcinfo.Employees_id";
            MSDataFill(script, _connectData, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string script = $"insert into tmcinfo(Period,StorageLocation,MOL,Counterparties,Employee,StorageLocations_id,Counterparties_id,Employees_id) value ('{dateTimePicker1.Value.ToString("yyyy.MM.dd").Substring(0, 10)}','{comboBox1.Text}','{textBox2.Text}','{comboBox2.Text}','{comboBox3.Text}','{comboBox4.Text}','{comboBox5.Text}','{comboBox6.Text}')";
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
                    DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить отчет?", "Удаление", MessageBoxButtons.YesNo);
                    if (zz == DialogResult.Yes)
                    {


                        string script = ($"DELETE FROM tmcinfo WHERE ID = {value2} ");
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
            string script = ($"UPDATE tmcinfo SET Period = '{dateTimePicker1.Value.ToString("yyyy.MM.dd").Substring(0, 10)}', StorageLocation = '{comboBox1.Text}', MOL = '{textBox2.Text}', Counterparties = '{comboBox2.Text}', Employee = '{comboBox3.Text}', StorageLocations_id = '{comboBox4.Text}', Counterparties_id = '{comboBox5.Text}', Employees_id = '{comboBox6.Text}' WHERE ID = {value2}");
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM StorageLocations WHERE NameOfStorageLocation='{comboBox1.Text}'", comboBox4, "tmcinfo.id", "id");
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM counterparties WHERE Counterpartie='{comboBox2.Text}'", comboBox5, "tmcinfo.id", "id");
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSAdapter($"SELECT ID  FROM employees WHERE FIO='{comboBox3.Text}'", comboBox6, "tmcinfo.id", "id");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Учет ТМЦ.pdf";
                bool ErrorMessage = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Невозможно записать на диск: " + ex.Message);
                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            var exePath = AppDomain.CurrentDomain.BaseDirectory;
                            var path = System.IO.Path.Combine(exePath, "..\\..\\Fonts\\couriercyrps_bold.ttf");

                            BaseFont bf = BaseFont.CreateFont(path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 3;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            pTable.DefaultCell.BorderWidth = 1;

                            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
                            //Добавим заголовки

                            //Кастыль
                            String[] Header = new string[6];
                            Header[0] = "";
                            Header[1] = "Период";
                            Header[2] = "Склад хранения";
                            Header[3] = "Мол";
                            Header[4] = "Контрагент";
                            Header[5] = "Работник";

                            for (int i = 0; i < 6; i++)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(Header[i], text));
                                pCell.BackgroundColor = new iTextSharp.text.BaseColor(250, 250, 250);
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow viewRow in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell dcell in viewRow.Cells)
                                {
                                    if (dcell.OwningColumn != dataGridView1.Columns[0])
                                    {
                                        pTable.AddCell(new Phrase(dcell.Value.ToString(), text));
                                    }
                                    else
                                    {
                                        pTable.AddCell(new Phrase("", text));
                                    }
                                }
                            }
                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                document.Add(pTable);
                                String line = "______________________________________________________________________________________";
                                document.Add(new Paragraph(line));
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Сохранение завершено успешно", "Уведомление");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при сохранении" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Записей не найдено", "Ошибка");
            }
        }
    }
}
