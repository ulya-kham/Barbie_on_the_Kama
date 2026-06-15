using Barbie_on_the_Kama.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barbie_on_the_Kama
{
    public partial class Form2 : Form
    {
        string connectStr = @"Data Source=barbie.db;Version=3;";

        public Form2()
        {
            
            InitializeComponent();
            DoubleBuffered = true;

            listBox_location.DrawMode = DrawMode.OwnerDrawFixed;
            listBox_location.ItemHeight = 33;

        }
        public int level { get; set; }
        public double points { get; set; }
        public double money { get; set; }
        public string player { get; set; }

        int min_lvl = 0;
        string location;

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
            Form1 newForm = new Form1();
            newForm.Show();
            this.Visible = false;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e) // отображение игрового прогресса
        {
            listBox_location.SetSelected(0, true); // выбор первого элемента списка

            this.ActiveControl = button1;

            label_lvl.Text = level.ToString();
            label_progress.Text = points.ToString() + "/300";
            label_money.Text = money.ToString();
        }

        private bool IsLocationOpen()
        {
            if (level >= min_lvl)
                return true;
            else
                return false;
        }

        private void SetLocationImage(System.Drawing.Bitmap openImage)
        {
            if (IsLocationOpen())
                reka_pb.BackgroundImage = openImage;
            else
                reka_pb.BackgroundImage = Resources.zakryto;
        }

        private void listBox_location_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedindex = listBox_location.SelectedIndex;
            location = listBox_location.SelectedItem.ToString();
            label_reka.Text = location;

            switch (selectedindex)
            {
                case 0:
                    min_lvl = 0;
                    SetLocationImage(Resources.paya);
                    break;
                case 1:
                    min_lvl = 1;
                    SetLocationImage(Resources.kosva);
                    break;
                case 2:
                    min_lvl = 2;
                    SetLocationImage(Resources.suzva);
                    break;
                case 3:
                    min_lvl = 4;
                    SetLocationImage(Resources.sylva);
                    break;
                case 4:
                    min_lvl = 6;
                    SetLocationImage(Resources.obva);
                    break;
                case 5:
                    min_lvl = 9;
                    SetLocationImage(Resources.iren);
                    break;
                case 6:
                    min_lvl = 12;
                    SetLocationImage(Resources.chusova);
                    break;
                case 7:
                    min_lvl = 15;
                    SetLocationImage(Resources.vishera);
                    break;
            }

            min_lvl_label.Text = "Необходимый уровень: " + min_lvl.ToString();
            UpdateLocationDescription();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsLocationOpen())
            {
                MessageBox.Show("Необходим " + min_lvl + " уровень!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form3 newForm = new Form3();
            newForm.level = level;
            newForm.points = points;
            newForm.location = location;
            newForm.player = player;
            newForm.money = money;
            newForm.Show();
            this.Visible = false;
        }

        private void records_btn_Click(object sender, EventArgs e)
        {
            Form4 newForm = new Form4();

            newForm.ParentForm2 = this;

            newForm.level = level;
            newForm.points = points;
            newForm.player = player;
            newForm.money = money;

            newForm.Show();

            this.Hide();
        }

        private void listBox_location_DoubleClick(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 newForm = new Form5();
            newForm.level = level;
            newForm.points = points;
            newForm.player = player;
            newForm.Show();
            this.Visible = false;
        }

        private void UpdateLocationDescription()
        {
            switch (location)
            {
                case "Пая":
                    label14.Text =
                        "Пая — небольшая лесная река на севере Пермского края.\n" +
                        "Её тихие берега скрыты хвойными лесами, а вода остаётся прохладной даже летом. " +
                        "Местные рыбаки ценят Паю за спокойствие и возможность поймать окуня, плотву и щуку вдали от популярных маршрутов.";
                    break;

                case "Косьва":
                    label14.Text =
                        "Косьва берёт начало на западных склонах Урала и протекает через Губахинский округ.\n" +
                        "Река славится чистой водой, живописными перекатами и суровой северной природой. " +
                        "Когда-то по Косьве сплавляли лес, а сегодня сюда приезжают за красивыми видами и хорошей рыбалкой.";
                    break;

                case "Сюзьва":
                    label14.Text =
                        "Сюзьва течёт по территории Добрянского округа среди смешанных лесов Прикамья.\n" +
                        "Небольшая и извилистая река известна своими тихими заводями, где любят держаться карась, окунь и язь. " +
                        "Идеальное место для неторопливой рыбалки на поплавочную удочку.";
                    break;

                case "Сылва":
                    label14.Text =
                        "Сылва — одна из крупнейших рек Пермского края, протекающая через Кунгурский и Пермский районы.\n" +
                        "Она знаменита своими известняковыми скалами, карстовыми пещерами и красивыми речными долинами. " +
                        "На Сылве можно встретить практически все популярные виды рыб Среднего Урала.";
                    break;

                case "Обва":
                    label14.Text =
                        "Обва является крупным левым притоком Камы и протекает через Ильинский и Карагайский районы.\n" +
                        "Название реки известно ещё с древних времён, а её воды веками кормили местных жителей. " +
                        "Широкие плёсы и глубокие ямы делают Обву отличным местом для ловли крупной рыбы.";
                    break;

                case "Ирень":
                    label14.Text =
                        "Ирень протекает по югу Пермского края через Ординский и Кунгурский районы.\n" +
                        "Река известна своими карстовыми ландшафтами и многочисленными подземными источниками. " +
                        "Рыболовы ценят Ирень за разнообразие рыб и красивые сельские пейзажи.";
                    break;

                case "Чусовая":
                    label14.Text =
                        "Чусовая — легендарная уральская река, пересекающая границу Европы и Азии.\n" +
                        "Именно по ней в XVIII веке шли барки с железом демидовских заводов. " +
                        "Величественные скалы-бойцы, быстрые перекаты и богатая история сделали Чусовую символом всего Урала.";
                    break;

                case "Вишера":
                    label14.Text =
                        "Вишера считается жемчужиной северного Прикамья и протекает через Красновишерский район.\n" +
                        "Река берёт начало в горах Северного Урала и проходит через заповедные таёжные территории. " +
                        "Кристально чистая вода, дикая природа и шанс встретить настоящий трофей делают Вишеру мечтой любого рыболова.";
                    break;

                default:
                    label14.Text = "";
                    break;
            }
        }

        private void listBox_location_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            Color backColor;
            Color textColor;

            if (selected)
            {
                // основной розовый
                backColor = Color.FromArgb(194, 66, 112);
                textColor = Color.White;
            }
            else
            {
                backColor = Color.FromArgb(254, 230, 234);
                textColor = Color.FromArgb(194, 66, 112);
            }

            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;      // по горизонтали
            sf.LineAlignment = StringAlignment.Center;  // по вертикали

            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(
                    listBox_location.Items[e.Index].ToString(),
                    e.Font,
                    textBrush,
                    e.Bounds,
                    sf);
            }

            e.DrawFocusRectangle();
        }
    }
}