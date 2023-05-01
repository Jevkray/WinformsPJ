using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf.parser;
using MySqlX.XDevAPI;

namespace Praktika
{
    public partial class Form11 : Form
    {
        private DataTable _table;
        public MySqlConnection _mycon;
        public MySqlCommand _mycom;
        public string _access;
        private string _connectData = "Server=localhost;Database=mydb;Uid=root;pwd=12345;charset=utf8";
        public string value2;

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
        public Form11()
        {
            InitializeComponent();
            Initialization();
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
            Ychet();
        }
        private void Form11_Load(object sender, EventArgs e)
        {
            _access = Access.Accesses;
            Buttons(Access.Accesses);
        }
        private void Ychet()
        {
            string script = "Select remainingstock.id, remainingstock.Code as Код, remainingstock.Name as Наименование, remainingstock.EdIzmer as Единица_Измерения, remainingstock.Cost as Цена, remainingstock.RemainEndPer as Остаток, remainingstock.Amount as Количество, remainingstock.Sum as Сумма from remainingstock";
            MSDataFill(script, _connectData, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string script = $"insert into remainingstock(Code,Name,EdIzmer,Cost,RemainEndPer,Amount,Sum) value ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}','{textBox6.Text}','{textBox7.Text}')";
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DialogResult zz = MessageBox.Show("Вы уверены что хотите удалить этот элемент?", "Удаление", MessageBoxButtons.YesNo);
                    if (zz == DialogResult.Yes)
                    {


                        string script = ($"DELETE FROM remainingstock WHERE ID = {value2} ");
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
            string script = ($"UPDATE remainingstock SET Code = '{textBox1.Text}', Name = '{textBox2.Text}', EdIzmer = '{textBox3.Text}', Cost = '{textBox4.Text}', RemainEndPer = '{textBox5.Text}', Amount = '{textBox6.Text}', Sum = '{textBox7.Text}' WHERE ID = {value2}");
            MSDataFill(script, _connectData, dataGridView1);
            Initialization();
        }
        //Print to PDF
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Отчет ТМЦ.pdf";
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
                            String[] Header = new string[8];
                            Header[0] = "";
                            Header[1] = "Код";
                            Header[2] = "Наименование";
                            Header[3] = "Единица измерения";
                            Header[4] = "Цена";
                            Header[5] = "Остаток";
                            Header[6] = "Количество";
                            Header[7] = "Сумма";

                            for ( int  i = 0; i < 8; i++ )
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
