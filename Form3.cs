using Barbie_on_the_Kama.Properties;
using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Barbie_on_the_Kama
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private static Random r = new Random();
        private int tensionStartX;
        private int tensionMaxX;

        public int level { get; set; }
        public double points { get; set; }
        public double money { get; set; }
        public string location { get; set; }
        public string player { get; set; }

        double weight;
        string bait = "Червь";
        double points_for_fish, money_for_fish;
        int flag = 1;
        bool move;
        int record_weight;
        bool reeling = false;
        int x;
        int y;
        int left, right;
        int worm, oparysh, kuznec, rucheinik, zhivets, popl1, popl2, popl3, popl4, popl5, rod1, rod2, rod3, rod4, rod5;

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

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void UpdateFishingButton()
        {
            switch (flag)
            {
                case 0:
                    label9.Text = "ЖДЁМ...";
                    break;

                case 1:
                    label9.Text = "ЗАБРОСИТЬ";
                    break;

                case 2:
                    label9.Text = "ПОДСЕЧЬ";
                    break;

                case 3:
                    label9.Text = "ТЯНИ!";
                    break;
            }
        }
        private void back_btn_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.level = level;
            newForm.points = points;
            newForm.player = player;
            newForm.money = money;
            newForm.Show();
            this.Visible = false;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadAllFromDB();
            LoadRodSkins();
            LoadPoplSkins();

            x = (int)(544f * this.ClientSize.Width / 1181f);
            y = (int)(434f * this.ClientSize.Height / 696f);

            label2.Text = worm + "шт";

            if (location == "Пая")
                this.BackgroundImage = Resources.paya;
            if (location == "Косьва")
                this.BackgroundImage = Resources.kosva;
            if (location == "Сюзьва")
                this.BackgroundImage = Resources.suzva;
            if (location == "Сылва")
                this.BackgroundImage = Resources.sylva;
            if (location == "Обва")
                this.BackgroundImage = Resources.obva;
            if (location == "Ирень")
                this.BackgroundImage = Resources.iren;
            if (location == "Чусовая")
                this.BackgroundImage = Resources.chusova;
            if (location == "Вишера")
                this.BackgroundImage = Resources.vishera;

            label_lvl.Text = level.ToString();
            label_progress.Text = points.ToString() + "/300";
            label_money.Text = money.ToString();
            label_reka.Text = location;
            tensionStartX = (int)(729f * ClientSize.Width / 1181f);
            tensionMaxX = (int)(942f * ClientSize.Width / 1181f);

            pictureBox11.Left = pictureBox10.Left;
            ApplyStyle();
            UpdateFishingButton();
        }
        
        private void ApplyStyle()
        {
            Color pinkMain = Color.FromArgb(194, 66, 112);
            Color pinkLight = Color.FromArgb(250, 236, 242);
            listBox1.ItemHeight = 36;
            listBox2.ItemHeight = 36;
            groupBox1.BackColor = pinkLight;
            groupBox1.ForeColor = pinkMain;
            groupBox1.Font = new Font("Century Gothic", 14F, FontStyle.Bold);

            radioButton1.ForeColor = pinkMain;
            radioButton2.ForeColor = pinkMain;
            radioButton3.ForeColor = pinkMain;
            radioButton4.ForeColor = pinkMain;
            radioButton5.ForeColor = pinkMain;

            radioButton1.BackColor = pinkLight;
            radioButton2.BackColor = pinkLight;
            radioButton3.BackColor = pinkLight;
            radioButton4.BackColor = pinkLight;
            radioButton5.BackColor = pinkLight;

            radioButton1.Font = new Font("Century Gothic", 14F);
            radioButton2.Font = new Font("Century Gothic", 14F);
            radioButton3.Font = new Font("Century Gothic", 14F);
            radioButton4.Font = new Font("Century Gothic", 14F);
            radioButton5.Font = new Font("Century Gothic", 14F);

            listBox1.BackColor = pinkLight;
            listBox1.ForeColor = pinkMain;
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.Font = new Font("Century Gothic", 14F);

            listBox2.BackColor = pinkLight;
            listBox2.ForeColor = pinkMain;
            listBox2.BorderStyle = BorderStyle.None;
            listBox2.Font = new Font("Century Gothic", 14F);
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox2.DrawMode = DrawMode.OwnerDrawFixed;

            listBox1.DrawItem += ListBox_DrawItem;
            listBox2.DrawItem += ListBox_DrawItem;

        }
        
        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            ListBox lb = (ListBox)sender;

            Color pinkMain = Color.FromArgb(194, 66, 112);
            Color pinkLight = Color.FromArgb(250, 236, 242);
            Color pinkHover = Color.FromArgb(235, 180, 200);

            bool selected =
                (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            e.Graphics.FillRectangle(
                new SolidBrush(selected ? pinkHover : pinkLight),
                e.Bounds);

            Rectangle textRect = new Rectangle(
      e.Bounds.X + 10,
      e.Bounds.Y + 4,
      e.Bounds.Width - 10,
      e.Bounds.Height - 4);

            TextRenderer.DrawText(
                e.Graphics,
                lb.Items[e.Index].ToString(),
                lb.Font,
                textRect,
                pinkMain,
                TextFormatFlags.Left |
                TextFormatFlags.VerticalCenter);

            e.DrawFocusRectangle();
        }
        private (Fish selectedFish, int waitTime) GetFish(string location, string bait)
        {
            var fishes = FishData.LocationProbabilities[location]; // рыбы на выбранной локации
            var baits = FishData.BaitPreferences[bait]; // рыбы на выбранную наживку
            var candidates = new List<(Fish fish, int chance)>(); // cписок рыб которые могут клюнуть на выбранных условиях

            int totalPreference = 0;

            foreach (var fishEntry in fishes) // перебираем всех рыб на локации
            {
                Fish fish = fishEntry.Key;
                int population = fishEntry.Value;

                if (baits.TryGetValue(fish, out int preference)) // получаем количество рыбы каждого вида на локации
                {
                    if (preference > 0) // если рыба может клюнуть на выбранную наживку расчитываем вероятность ее поклевки
                    {
                        int fishPreference = preference * population;
                        candidates.Add((fish, fishPreference));
                        totalPreference += fishPreference;
                    }
                }
            }
            if (totalPreference == 0) // ваще не клюет
                return (null, int.MaxValue); // возвращаем бесконченое время поклевки

            int rand = r.Next(1, totalPreference + 1);
            int i = 0;
            Fish selectedFish = null;

            foreach (var candidate in candidates)  // сложный цикл который определяет какой вид рыбы клюнет на основе рассчитанных вероятностей
            {
                i += candidate.chance;
                if (rand <= i)
                {
                    selectedFish = candidate.fish;
                    break;
                }
            }

            int a = (int)(800 * Math.Sqrt(100000.0 / totalPreference));
            int b = (int)(7000 * Math.Sqrt(100000.0 / totalPreference));
            int waitTime = r.Next(a, b);  // подсчет времени поклевки
            return (selectedFish, waitTime);
        }

        public void GetFishWeight(int minWeight, int maxWeight)
        {
            double chancer = r.NextDouble();

            double range = maxWeight - minWeight;

            double a = 0.5555;
            double b = 0.8079;
            double c = 0.8923;
            double d = 0.9595;
            double e = 0.9871; 
            double f = 0.9961;

            if (chancer < a)
                weight = RandomRange(minWeight, minWeight + range * 0.11);
            else if (chancer < b)
                weight = RandomRange(minWeight + range * 0.11, minWeight + range * 0.22);
            else if (chancer < c)
                weight = RandomRange(minWeight + range * 0.22, minWeight + range * 0.39);
            else if (chancer < d)
                weight = RandomRange(minWeight + range * 0.39, minWeight + range * 0.62);
            else if (chancer < e)
                weight = RandomRange(minWeight + range * 0.62, minWeight + range * 0.81);
            else if (chancer < f)
                weight = RandomRange(minWeight + range * 0.81, minWeight + range * 0.91);
            else
                weight = RandomRange(minWeight + range * 0.91, maxWeight);

            weight = Math.Round(weight);
        }

        private static double RandomRange(double min, double max)
        {
            return min + (r.NextDouble() * (max - min));
        }

        private void play_btn_Click(object sender, EventArgs e)
        {
            label6.Visible = false;

            if (flag == 3)
            {
                return;
            }

            pictureBox5.Location = new Point(
    x - (int)(4f * ClientSize.Width / 1181f),
    y - (int)(33f * ClientSize.Height / 696f));

            if (flag == 0) // удочка в воде
            {
                panel1.Visible = false;
                pictureBox5.Visible = false;

                flag = 1;
                UpdateFishingButton();

                timer1.Enabled = false;
                timer2.Enabled = false;
                timer3.Enabled = false;

                return;
            }

            if (flag == 1) // удочка не в воде
            { 
                if (bait == "Червь" && worm == 0)
                {
                    label6.Visible = true;
                    return;
                }

                if (bait == "Опарыш" && oparysh == 0)
                {
                    label6.Visible = true;
                    return;
                }

                if (bait == "Кузнечик" && kuznec == 0)
                {
                    label6.Visible = true;
                    return;
                }

                if (bait == "Ручейник" && rucheinik == 0)
                {
                    label6.Visible = true;
                    return;

                }
                if (bait == "Живец" && zhivets == 0)
                {
                    label6.Visible = true;
                    return;
                }

                timer4.Enabled = false;
                timer5.Enabled = false;

                pictureBox5.Visible = true;
                flag = 0;
                UpdateFishingButton();
                groupBox1.Visible = false;
                listBox1.Visible = false;
                listBox2.Visible = false;

                var (selectedFish, waitTime) = GetFish(location, bait);

                GetFishWeight(selectedFish.MinWeight, selectedFish.MaxWeight);

                timer1.Interval = waitTime;
                timer1.Enabled = true;

                bool isTrophy = weight >= selectedFish.TrophyWeight;
                bool isZachet = weight >= selectedFish.ZachetWeight;

                string s = weight.ToString();
                label_weight.Text = s + " грамм";

                if (weight < 100)
                {
                    timer7.Interval = 30;
                    left = 2;
                    right = 1;
                }
                else if (weight < 250)
                {
                    timer7.Interval = 35;
                    left = 4;
                    right = 2;
                }
                else if (weight < 500)
                {
                    timer7.Interval = 35;
                    left = 5;
                    right = 3;
                }
                else if (weight < 1000)
                {
                    timer7.Interval = 32;
                    left = 6;
                    right = 4;
                }
                else if (weight < 2500)
                {
                    timer7.Interval = 20;
                    left = 7;
                    right = 5;
                    label_weight.Text = s.Substring(0, 1) + "," + s.Substring(1, 3) + " кг";
                }
                else if (weight < 5000)
                {
                    timer7.Interval = 20;
                    label_weight.Text = s.Substring(0, 1) + "," + s.Substring(1, 3) + " кг";
                    left = 10;
                    right = 7;
                }
                else if (weight < 10000)
                {
                    timer7.Interval = 17;
                    label_weight.Text = s.Substring(0, 1) + "," + s.Substring(1, 3) + " кг";
                    left = 13;
                    right = 10;
                }
                else
                {
                    timer7.Interval = 17;
                    label_weight.Text = s.Substring(0, 2) + "," + s.Substring(2, 3) + " кг";
                    left = 17;
                    right = 13;
                }

                label_fish.Text = selectedFish.Name;
                pictureBox3.BackgroundImage = selectedFish.Image;
                points_for_fish = weight * selectedFish.Points;
                points_for_fish = Math.Round(points_for_fish, 2);
                money_for_fish = weight * selectedFish.Money;
                money_for_fish = Math.Round(money_for_fish, 2);

                if (isZachet)
                {
                    label_weight.Text += " (зачетная)";
                    label_weight.ForeColor = Color.DarkMagenta;
                    money_for_fish *= 2;
                }
                else
                {
                    label_weight.ForeColor = SystemColors.ControlText;
                }
                pictureBox1.Visible = isTrophy;
                pictureBox2.Visible = isTrophy;
            }

            if (flag == 2) // можно подсекать
            {
                timer6.Enabled = true;
                timer7.Enabled = true;
                timer1.Enabled = false;
                timer2.Enabled = false;
                timer3.Enabled = false;
                timer4.Enabled = false;
                timer5.Enabled = false;
                flag = 3;
                UpdateFishingButton();
                pictureBox5.Visible = false;
                pictureBox7.Visible = true;
            }
        }

        public void Poimka()
        {
            pictureBox8.Visible = false;
            record_check();

            if (weight > record_weight)
            {
                record_update();
                pictureBox8.Visible = true;
            }

            label_points.Text = points_for_fish.ToString();
            points += points_for_fish;
            label4.Text = "+" + label_points.Text;
            label5.Text = "+" + money_for_fish;

            while (points >= 300)
            {
                points -= 300;
                level = Convert.ToInt32(label_lvl.Text);
                level++;
                label_lvl.Text = level.ToString();
            }
            points = Math.Round(points, 2);
            label_progress.Text = points.ToString() + "/300";

            switch (bait)
            {
                case "Червь": 
                    worm -= 1;
                    label2.Text = worm + "шт";
                    break;

                case "Опарыш": 
                    oparysh -= 1;
                    label2.Text = oparysh + "шт";
                    break;

                case "Кузнечик": 
                    kuznec -= 1;
                    label2.Text = kuznec + "шт";
                    break;

                case "Ручейник": 
                    rucheinik -= 1;
                    label2.Text = rucheinik + "шт";
                    break;

                case "Живец": 
                    zhivets -= 1;
                    label2.Text = zhivets + "шт";
                    break;
            };

            save();
            pictureBox3.Location = new Point(((panel1.ClientSize.Width - pictureBox3.Width) / 2), ((panel1.ClientSize.Height - pictureBox3.Height) / 2) - 64);
            //label_fish.Location = new Point(((panel1.ClientSize.Width - label_fish.Width) / 2), ((panel1.ClientSize.Height - label_fish.Height) / 2) + 60);
            //label_weight.Location = new Point(((panel1.ClientSize.Width - label_weight.Width) / 2), ((panel1.ClientSize.Height - label_weight.Height) / 2) + 96);
            //label_points.Location = new Point(((panel1.ClientSize.Width - label_points.Width) / 2), ((panel1.ClientSize.Height - label_points.Height) / 2) + 175);
            //panel4.Location = new Point(((panel1.ClientSize.Width - panel4.Width) / 2) - 168, ((panel1.ClientSize.Height - panel4.Height) / 2) + 146);
            //panel5.Location = new Point(((panel1.ClientSize.Width - panel5.Width) / 2) + 168, ((panel1.ClientSize.Height - panel5.Height) / 2) + 146);

            panel1.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Interval = r.Next(200, 3500);
            timer2.Enabled = true;
            timer3.Enabled = true;
            move = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = true;
            timer5.Enabled = true;
            flag = 2;
            UpdateFishingButton();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            int k = r.Next(1, 3);

            if (move)
            {
                if (k == 1)
                    timer3.Interval = 220;
                else
                    timer3.Interval = 130;

                pictureBox5.Top += 5;
                move = false;
            }

            else
            {
                if (k == 1)
                    timer3.Interval = 180;
                else
                    timer3.Interval = 90;

                pictureBox5.Top -= 5;
                move = true;
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            flag = 1;
            UpdateFishingButton();
            play_btn_Click(sender, e);
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            pictureBox5.Top += 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label6.Visible = false;

            if (flag == 1)
            {
                if (groupBox1.Visible == true)
                {
                    groupBox1.Visible = false;
                }
                else
                {
                    groupBox1.Visible = true;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            bait = "Червь";
            button1.BackgroundImage = Resources.worm;
            label2.Text = worm + " шт";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            bait = "Опарыш";
            button1.BackgroundImage = Resources.oparysh;
            label2.Text = oparysh + " шт";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            bait = "Кузнечик";
            button1.BackgroundImage = Resources.kuznec;
            label2.Text = kuznec + " шт";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            bait = "Ручейник";
            button1.BackgroundImage = Resources.rucheinik;
            label2.Text = rucheinik + " шт";
        }

        private void radioButton5_CheckedChanged_1(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            bait = "Живец";
            button1.BackgroundImage = Resources.zhivets;
            label2.Text = zhivets + " шт";
        }

        private void save()
        {
            string connectStr = @"Data Source=barbie.db;Version=3;";

            try
            {
                using (SQLiteConnection connectSQL = new SQLiteConnection(connectStr))
                {
                    connectSQL.Open();
                    string query = "UPDATE users SET level = @level, points = @points, money = @money, worm = @worm, oparysh = @oparysh, kuznec = @kuznec, rucheinik = @rucheinik, zhivets = @zhivets WHERE name = @player";

                    using (SQLiteCommand commandSQL = new SQLiteCommand(query, connectSQL))
                    {
                        commandSQL.Parameters.AddWithValue("@level", level);
                        commandSQL.Parameters.AddWithValue("@points", points);
                        commandSQL.Parameters.AddWithValue("@money", money);

                        commandSQL.Parameters.AddWithValue("@worm", worm);
                        commandSQL.Parameters.AddWithValue("@oparysh", oparysh);
                        commandSQL.Parameters.AddWithValue("@kuznec", kuznec);
                        commandSQL.Parameters.AddWithValue("@rucheinik", rucheinik);
                        commandSQL.Parameters.AddWithValue("@zhivets", zhivets);

                        commandSQL.Parameters.AddWithValue("@player", player);
                        commandSQL.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения или запроса: " + ex.Message);
            }
        }

        private void record_check()
        {
            string connectStr = @"Data Source=barbie.db;Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectStr))
            {
                conn.Open();

                string query = "SELECT weight FROM records WHERE fish = @fish";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fish", label_fish.Text);

                    record_weight = Convert.ToInt32 (cmd.ExecuteScalar());
                }
            }
        }

        private void record_update()
        {
            string connectStr = @"Data Source=barbie.db;Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectStr))
            {
                conn.Open();

                string query = "UPDATE records SET weight = @weight, name = @name, location = @location, catch_date = @catch_date WHERE fish = @fish";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fish", label_fish.Text);
                    cmd.Parameters.AddWithValue("@weight", weight);
                    cmd.Parameters.AddWithValue("@name", player);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@catch_date", DateTime.Now.ToString("yyyy-MM-dd"));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void play_btn_MouseDown(object sender, MouseEventArgs e)
        {
            reeling = true;
        }

        private void play_btn_MouseUp(object sender, MouseEventArgs e)
        {
            reeling = false;
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            int sideMove = r.Next(-4, 5);

            if (reeling)
            {
                pictureBox7.Top += 2;
            }
            else
            {
                if (pictureBox7.Top >= FishData.LocationDepth[location])
                    pictureBox7.Top -= 2;
            }

            pictureBox7.Left += sideMove;

            if (pictureBox7.Left < 20)
                pictureBox7.Left = 20;

            int maxFishX = ClientSize.Width - 20;

            if (pictureBox7.Left > maxFishX)
                pictureBox7.Left = maxFishX;

            int shoreY = ClientSize.Height - 60;

            if (pictureBox7.Top >= shoreY)
            {
                timer6.Enabled = false;
                timer7.Enabled = false;

                pictureBox7.Visible = false;
                pictureBox7.Location = new Point(x, y);

                Poimka();

                flag = 1;
                UpdateFishingButton();

                pictureBox11.Left = pictureBox10.Left;

                return;
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            int tensionStart = pictureBox10.Left;
            int tensionMax = pictureBox10.Right - pictureBox11.Width;

            if (reeling)
            {
                pictureBox11.Left += right;
            }
            else
            {
                if (pictureBox11.Left > tensionStart)
                    pictureBox11.Left -= left;
            }

            if (pictureBox11.Left < tensionStart)
                pictureBox11.Left = tensionStart;

            if (pictureBox11.Left >= tensionMax)
            {
                timer6.Enabled = false;
                timer7.Enabled = false;

                pictureBox7.Visible = false;
                pictureBox7.Location = new Point(x, y);
                flag = 1;
                UpdateFishingButton();

                pictureBox11.Left = tensionStart;

                return;
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            flag = 0;
            play_btn_Click(sender, e);

            points += points_for_fish;

            while (points >= 300)
            {
                points -= 300;
                level = Convert.ToInt32(label_lvl.Text);
                level++;
                label_lvl.Text = level.ToString();
            }
            points = Math.Round(points, 2);
            label_progress.Text = points.ToString() + "/300";

            save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flag = 0;
            play_btn_Click(sender, e);
            money += money_for_fish;
            money = Math.Round(money, 2);
            label_money.Text = money.ToString();

            save();
        }

        private void Form3_MouseClick(object sender, MouseEventArgs e)
        {
            int maxClickY = (int)(500f * ClientSize.Height / 696f);
            int minClickX = (int)(100f * ClientSize.Width / 1181f);

            if (e.Y >= FishData.LocationDepth[location]
                && e.Y < maxClickY
                && e.X > minClickX
                && flag == 1)
            {
                x = e.X;
                y = e.Y;

                int rodOffsetX = (int)(140f * ClientSize.Width / 1181f);
                int rodY = (int)(206f * ClientSize.Height / 696f);

                pictureBox4.Location = new Point(
    x - 150,
    350);

                pictureBox6.Location = new Point(
                    x - (int)(17f * ClientSize.Width / 1181f),
                    y + (int)(5f * ClientSize.Height / 696f));

                pictureBox7.Location = new Point(x, y);

                play_btn_Click(sender, e);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;

            switch (selected)
            {
                case "Удочка 1": pictureBox4.BackgroundImage = Resources.rod1; break;
                case "Удочка 2": pictureBox4.BackgroundImage = Resources.rod2; break;
                case "Удочка 3": pictureBox4.BackgroundImage = Resources.rod3; break;
                case "Удочка 4": pictureBox4    .BackgroundImage = Resources.rod4; break;
                case "Удочка 5": pictureBox4.BackgroundImage = Resources.rod5; break;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = listBox2.SelectedItem.ToString();
            listBox2.Visible = false;

            switch (selected)
            {
                case "Поплавок 1": pictureBox5.BackgroundImage = Resources.popl1; break;
                case "Поплавок 2": pictureBox5.BackgroundImage = Resources.popl2; break;
                case "Поплавок 3": pictureBox5.BackgroundImage = Resources.popl3; break;
                case "Поплавок 4": pictureBox5.BackgroundImage = Resources.popl4; break;
                case "Поплавок 5": pictureBox5.BackgroundImage = Resources.popl5; break;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (listBox1.Visible == true)
                {
                    listBox1.Visible = false;
                }
                else
                {
                    listBox1.Visible = true;
                    listBox2.Visible = false;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (listBox2.Visible == true)
                {
                    listBox2.Visible = false;
                }
                else
                {
                    listBox2.Visible = true;
                    listBox1.Visible = false;
                }
            }
        }

        private void LoadRodSkins()
        {
            if (rod1 == 1) listBox1.Items.Add("Удочка 1");
            if (rod2 == 1) listBox1.Items.Add("Удочка 2");
            if (rod3 == 1) listBox1.Items.Add("Удочка 3");
            if (rod4 == 1) listBox1.Items.Add("Удочка 4");
            if (rod5 == 1) listBox1.Items.Add("Удочка 5");
        }

        private void LoadPoplSkins()
        {
            if (popl1 == 1) listBox2.Items.Add("Поплавок 1");
            if (popl2 == 1) listBox2.Items.Add("Поплавок 2");
            if (popl3 == 1) listBox2.Items.Add("Поплавок 3");
            if (popl4 == 1) listBox2.Items.Add("Поплавок 4");
            if (popl5 == 1) listBox2.Items.Add("Поплавок 5");
        }

        private void LoadAllFromDB()
        {
            string connectStr = @"Data Source=barbie.db;Version=3;";
            string query = "SELECT worm, oparysh, kuznec, rucheinik, zhivets, rod1, rod2, rod3, rod4, rod5, popl1, popl2, popl3, popl4, popl5 FROM users WHERE name = @player";

            using (SQLiteConnection connection = new SQLiteConnection(connectStr))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@player", player);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            worm = reader.GetInt32(0);
                            oparysh = reader.GetInt32(1);
                            kuznec = reader.GetInt32(2);
                            rucheinik = reader.GetInt32(3);
                            zhivets = reader.GetInt32(4);

                            rod1 = reader.GetInt32(5);
                            rod2 = reader.GetInt32(6);
                            rod3 = reader.GetInt32(7);
                            rod4 = reader.GetInt32(8);
                            rod5 = reader.GetInt32(9);

                            popl1 = reader.GetInt32(10);
                            popl2 = reader.GetInt32(11);
                            popl3 = reader.GetInt32(12);
                            popl4 = reader.GetInt32(13);
                            popl5 = reader.GetInt32(14);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
                }
            }
        }
    }
}