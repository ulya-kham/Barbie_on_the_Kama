using Barbie_on_the_Kama.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mysqlx.Notice.Warning.Types;

namespace Barbie_on_the_Kama
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            dataGridView1.CellMouseEnter += DataGridView1_CellMouseEnter;
            dataGridView1.CellMouseLeave += DataGridView1_CellMouseLeave;

            dataGridView1.ColumnHeaderMouseClick +=
                dataGridView1_ColumnHeaderMouseClick;
        }
        public int level { get; set; }
        public double points { get; set; }
        public double money { get; set; }
        public string player { get; set; }
        public Form2 ParentForm2;
        private void StyleGrid()
        {
            Color pink = Color.FromArgb(194, 66, 112);
            Color lightPink = Color.FromArgb(254, 230, 234);

            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.MultiSelect = false;

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;

            dataGridView1.BackgroundColor = lightPink;
            dataGridView1.BorderStyle = BorderStyle.None;

            dataGridView1.GridColor =
                Color.FromArgb(230, 190, 200);

            dataGridView1.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            // ===== Заголовки =====

            dataGridView1.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor =
     Color.FromArgb(194, 66, 112);

            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            // Цвет заголовка при сортировке
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(225, 120, 160);

            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.White;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font("Century Gothic", 18, FontStyle.Bold);

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.ColumnHeadersHeight = 45;

            // ===== Строки =====

            dataGridView1.DefaultCellStyle.BackColor = Color.White;

            dataGridView1.DefaultCellStyle.ForeColor =
                Color.FromArgb(70, 70, 70);

            dataGridView1.DefaultCellStyle.Font =
                new Font("Century Gothic", 16);

            dataGridView1.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(255, 245, 247);

            // Светло-розовое выделение
            dataGridView1.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(255, 220, 228);

            dataGridView1.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(70, 70, 70);

            dataGridView1.RowTemplate.Height = 35;

            // Сортировку ВКЛЮЧАЕМ
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode =
                    DataGridViewColumnSortMode.Automatic;
            }
        }
        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
           // Application.Exit();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "🌸 Уже уходишь?\n\nМы будем тебя ждать\n\nВыйти из Barbie on the Kama?",
        "До встречи на рыбалке ♡",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
       
        private void back_btn_Click(object sender, EventArgs e)
        {
            if (ParentForm2 != null)
            {
                ParentForm2.Show();
            }

            this.Close();

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string connectStr = @"Data Source=barbie.db;Version=3;";
            string query = "SELECT fish AS 'Рыба', weight AS 'Вес', name AS 'Игрок', catch_date AS 'Дата', location AS 'Локация' FROM records";

            using (SQLiteConnection connectSQL = new SQLiteConnection(connectStr))
            {
                using (SQLiteCommand commandSQL = new SQLiteCommand(query, connectSQL))
                {
                    connectSQL.Open();

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(commandSQL);

                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["Дата"].Value != null)
                        {
                            string value =
                                row.Cells["Дата"].Value.ToString();

                            DateTime dt;

                            if (DateTime.TryParse(value, out dt))
                            {
                                row.Cells["Дата"].Value =
                                    dt.ToString("dd.MM.yy");
                            }
                        }
                    }
                }

            }

            StyleGrid();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
            }

            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns["Рыба"].FillWeight = 35;
            dataGridView1.Columns["Вес"].FillWeight = 10;
            dataGridView1.Columns["Игрок"].FillWeight = 15;
            dataGridView1.Columns["Дата"].FillWeight = 20;
            dataGridView1.Columns["Локация"].FillWeight = 20;
            label7.BringToFront();
            label8.BringToFront();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            panel2.Visible = true;

            string fish =
                dataGridView1.SelectedRows[0].Cells[0].Value?.ToString() ?? "";

            string s =
                dataGridView1.SelectedRows[0].Cells[1].Value?.ToString() ?? "0";

            label7.Text = fish;

            // ===== Вес =====

            double weight = 0;
            double.TryParse(s, out weight);

            if (weight <= 0)
            {
                label8.Text = "Нет рекорда";
            }
            else
            {
                if (weight < 1000)
                {
                    label8.Text = weight + " грамм";
                }
                else
                {
                    label8.Text =
                        (weight / 1000.0).ToString("0.###") + " кг";
                }
            }

            // ===== Рыба =====

            if (fish == "Гольян")
            {
                pictureBox4.BackgroundImage = Resources.golyan;
                label2.Text = "Гольян – это представитель семейства карповых, отличается небольшими размерами. Предпочитают водоемы со стремительным течением и чистой водой. Питается в основном личинками насекомых, вырастает до 8-11 см.";
            }
            else if (fish == "Голавль")
            {
                pictureBox4.BackgroundImage = Resources.golavl;
                label2.Text = "Голавль — широко распространенная рыба средних размеров из семейства карповых. Голавль предпочитает глубокие омута рядом с перекатами, питается с поверхности насекомыми и молодь рыб. Встречаются особи размером более 2-х килограмм.";
            }
            else if (fish == "Плотва")
            {
                pictureBox4.BackgroundImage = Resources.plotva;
                label2.Text = "Плотва – это стайная рыба из семейства карповых, обитающая в реках на участках с умеренным течением. Плотва интересна тем, что ведет активный образ жизни в любое время года. Вырастает до 1 килограмма.";
            }
            else if (fish == "Подуст")
            {
                pictureBox4.BackgroundImage = Resources.podust;
                label2.Text = "Подуст — рыба из семейства карповых. В среднем вырастает до 30 сантиметров, вес — 500 грамм. Подуст очень восприимчив к условиям обитания и находится в Красной книге России. Обитает в реках с песчаным или каменистым дном, часто любит находиться у затопленных коряг.";
            }
            else if (fish == "Хариус")
            {
                pictureBox4.BackgroundImage = Resources.khar;
                label2.Text = "Хариус — обитает в быстрых реках, в чистой воде. Ценится за красоту, сопротивление при вываживании и вкусное мясо. Эта рыба стала символом чистых водоёмов — там, где она исчезает, природа теряет равновесие. Для рыбаков хариус — трофей, а для гурманов — деликатес.";
            }
            else if (fish == "Язь")
            {
                pictureBox4.BackgroundImage = Resources.yaz;
                label2.Text = "Язь - представитель семейства карповых. Питается насекомыми, червями и молодью других рыб. Молодые особи предпочитают вести стайный образ жизни. Чем старше язь, тем более малочисленная группа, в которой он находится.";
            }
            else if (fish == "Щука")
            {
                pictureBox4.BackgroundImage = Resources.schuka;
                label2.Text = "Щука – хищная рыба, способная вырастать в длину до полутора метров и весить при этом порядка 15 кг. Щука отличается прогонистой формой, сравнительно большой головой и пастью. Охотится преимущественно из засады.";
            }
            else if (fish == "Окунь")
            {
                pictureBox4.BackgroundImage = Resources.okun;
                label2.Text = "Окунь – хищная рыба, характерной особенностью этого вида рыб является строение и форма спинного плавника, а также наличие черных полос по бокам туловища. Окунь питается червями и мальком, достигает массы 2 кг.";
            }
            else if (fish == "Елец")
            {
                pictureBox4.BackgroundImage = Resources.elec;
                label2.Text = "Елец – имеет сравнительно большие плавники и очень мощное тело, из-за чего является желанным трофеем у рыболовов. Елец достигает массы 500 грамм. Питается насекомыми и их личинками.";
            }
            else if (fish == "Пескарь")
            {
                pictureBox4.BackgroundImage = Resources.peskar;
                label2.Text = "Пескарь – это пресноводная рыба небольших размеров. Пескарь водится практически во всех реках с быстрым течением. Ценится у рыболовов за свои вкусовые качества. Как правило, попадаются особи величиной до 17 см.";
            }
            else if (fish == "Таймень")
            {
                pictureBox4.BackgroundImage = Resources.tai;
                label2.Text = "Таймень - крупнейший представитель лососевых, может достигать 2 метров в длину и веса до 80 кг. Обитает в быстрых реках Сибири и Дальнего Востока, занесен в Красную книгу. Хищник, питается рыбой, может нападать на водоплавающих птиц и даже мелких грызунов.";
            }
            else if (fish == "Ручьевая форель")
            {
                pictureBox4.BackgroundImage = Resources.trout;
                label2.Text = "Ручьевая форель - пресноводная рыба семейства лососевых, обитает в холодных чистых ручьях и реках с быстрым течением. Имеет яркую окраску с красными и черными пятнами. Чувствительна к чистоте воды и содержанию кислорода.";
            }
            else if (fish == "Подкаменщик")
            {
                pictureBox4.BackgroundImage = Resources.pdkm;
                label2.Text = "Подкаменщик - мелкая донная рыба семейства рогатковых, обитает в холодных чистых реках с каменистым дном. Ведет скрытный образ жизни, питается мелкими беспозвоночными. Длина обычно не превышает 10-15 см. Занесен в Красную книгу во многих регионах России.";
            }
            else if (fish == "Жерех")
            {
                pictureBox4.BackgroundImage = Resources.zherix;
                label2.Text = "Жерех - хищная рыба семейства карповых, обитает в быстрых реках. Отличается мощным телом и нижней челюстью, выступающей вперед. Охотится на мелкую рыбу у поверхности воды, часто выпрыгивая за добычей.";
            }

            pictureBox4.Location = new Point(
                (panel2.ClientSize.Width - pictureBox4.Width) / 2,
                40);

            label7.AutoSize = true;
            label8.AutoSize = true;

            label7.Location = new Point(
                (panel2.ClientSize.Width - label7.Width) / 2,
                pictureBox4.Bottom + 20);

            label8.Location = new Point(
                (panel2.ClientSize.Width - label8.Width) / 2,
                label7.Bottom + 10);

        }

        private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    Color.FromArgb(255, 235, 240);
            }
        }

        private void DataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    Color.White;
            }
        }
        private void dataGridView1_ColumnHeaderMouseClick(
    object sender,
    DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.ClearSelection();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
            }
        }

    }
}