using Barbie_on_the_Kama.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barbie_on_the_Kama
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        string connectStr = @"Data Source=barbie.db;Version=3;";

        public int level { get; set; }
        public double points { get; set; }
        public string player { get; set; }

        int select, worm, oparysh, kuznec, rucheinik, zhivets, popl1, popl2, popl3, popl4, popl5, rod1, rod2, rod3, rod4, rod5;

        private GraphicsPath RoundedRect(
    Rectangle bounds,
    int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int d = radius * 2;

            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);

            path.CloseFigure();

            return path;
        }
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ListBox lb = (ListBox)sender;

            bool selected =
                (e.State & DrawItemState.Selected) ==
                DrawItemState.Selected;

            Color cardColor = selected
                ? Color.FromArgb(255, 210, 220)
                : Color.FromArgb(255, 245, 247);

            Color textColor = Color.FromArgb(194, 66, 112);

            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle cardRect = new Rectangle(
                e.Bounds.X + 4,
                e.Bounds.Y + 4,
                e.Bounds.Width - 8,
                e.Bounds.Height - 8);

            using (GraphicsPath path = RoundedRect(cardRect, 16))
            {
                using (SolidBrush brush = new SolidBrush(cardColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (Pen pen =
                    new Pen(Color.FromArgb(240, 200, 210)))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }

            Image icon = null;

            switch (lb.Items[e.Index].ToString())
            {
                case "Червь":
                    icon = Resources.worm_m;
                    break;

                case "Опарыш":
                    icon = Resources.oparysh_m;
                    break;

                case "Кузнечик":
                    icon = Resources.kuznec_m;
                    break;

                case "Ручейник":
                    icon = Resources.rucheinik_m;
                    break;

                case "Живец":
                    icon = Resources.zhivets_m;
                    break;
            }

            if (icon != null)
            {
                e.Graphics.DrawImage(
                    icon,
                    cardRect.Left + 12,
                    cardRect.Top + 10,
                    65,
                    65);
            }

            using (Font font =
                new Font("Century Gothic", 16,
                    FontStyle.Bold))
            {
                using (Brush textBrush =
                    new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(
                        lb.Items[e.Index].ToString(),
                        font,
                        textBrush,
                        cardRect.Left + 95,
                        cardRect.Top + 28);
                }
            }

            e.DrawFocusRectangle();
        }
        private void DrawImageZoom(Graphics g, Image img, Rectangle bounds)
        {
            float ratioX = (float)bounds.Width / img.Width;
            float ratioY = (float)bounds.Height / img.Height;

            float ratio = Math.Min(ratioX, ratioY);

            int width = (int)(img.Width * ratio);
            int height = (int)(img.Height * ratio);

            int x = bounds.X + (bounds.Width - width) / 2;
            int y = bounds.Y + (bounds.Height - height) / 2;

            g.DrawImage(img, x, y, width, height);
        }
        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            Graphics g = e.Graphics;

            g.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            string text = listBox2.Items[e.Index].ToString();

            Rectangle cardRect = new Rectangle(
                e.Bounds.X + 5,
                e.Bounds.Y + 4,
                e.Bounds.Width - 10,
                e.Bounds.Height - 8);

            bool selected =
                (e.State & DrawItemState.Selected) ==
                DrawItemState.Selected;

            Color backColor = selected
                ? Color.FromArgb(255, 210, 220)
                : Color.White;

            using (GraphicsPath path =
                RoundedRect(cardRect, 16))
            {
                using (SolidBrush brush =
                    new SolidBrush(backColor))
                {
                    g.FillPath(brush, path);
                }

                using (Pen pen =
                    new Pen(Color.FromArgb(240, 200, 210)))
                {
                    g.DrawPath(pen, path);
                }
            }

            Image icon = null;

            switch (text)
            {
                case "Удочка 1":
                    icon = Resources.rod1;
                    break;

                case "Удочка 2":
                    icon = Resources.rod2;
                    break;

                case "Удочка 3":
                    icon = Resources.rod3;
                    break;

                case "Удочка 4":
                    icon = Resources.rod4;
                    break;

                case "Удочка 5":
                    icon = Resources.rod5;
                    break;
            }

            if (icon != null)
            {
                DrawImageZoom(
    g,
    icon,
    new Rectangle(
        cardRect.X + 12,
        cardRect.Y + 10,
        65,
        65));
            }

            using (Font font =
                new Font("Century Gothic", 16,
                    FontStyle.Bold))
            using (Brush textBrush =
                new SolidBrush(Color.FromArgb(194, 66, 112)))
            {
                g.DrawString(
                    text,
                    font,
                    textBrush,
                    cardRect.X + 95,
                    cardRect.Y + 28);
            }

            e.DrawFocusRectangle();
        }

        private void listBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            Graphics g = e.Graphics;

            g.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            string text = listBox3.Items[e.Index].ToString();

            Rectangle cardRect = new Rectangle(
                e.Bounds.X + 5,
                e.Bounds.Y + 4,
                e.Bounds.Width - 10,
                e.Bounds.Height - 8);

            bool selected =
                (e.State & DrawItemState.Selected) ==
                DrawItemState.Selected;

            Color backColor = selected
                ? Color.FromArgb(255, 210, 220)
                : Color.White;

            using (GraphicsPath path =
                RoundedRect(cardRect, 16))
            {
                using (SolidBrush brush =
                    new SolidBrush(backColor))
                {
                    g.FillPath(brush, path);
                }

                using (Pen pen =
                    new Pen(Color.FromArgb(240, 200, 210)))
                {
                    g.DrawPath(pen, path);
                }
            }

            Image icon = null;

            switch (text)
            {
                case "Поплавок 1":
                    icon = Resources.popl1;
                    break;

                case "Поплавок 2":
                    icon = Resources.popl2;
                    break;

                case "Поплавок 3":
                    icon = Resources.popl3;
                    break;

                case "Поплавок 4":
                    icon = Resources.popl4;
                    break;

                case "Поплавок 5":
                    icon = Resources.popl5;
                    break;
            }

            if (icon != null)
            {
                DrawImageZoom(
      g,
      icon,
      new Rectangle(
          cardRect.X + 12,
          cardRect.Y + 10,
          65,
          65));
            }

            using (Font font =
                new Font("Century Gothic", 16,
                    FontStyle.Bold))
            using (Brush textBrush =
                new SolidBrush(Color.FromArgb(194, 66, 112)))
            {
                g.DrawString(
                    text,
                    font,
                    textBrush,
                    cardRect.X + 95,
                    cardRect.Y + 28);
            }

            e.DrawFocusRectangle();
        }

        double price, money;

        private void button5_SizeChanged(object sender, EventArgs e)
        {
            RoundButton(button5);
        }

        string currentItemType = "";

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
            Form2 newForm = new Form2();
            newForm.level = level;
            newForm.points = points;
            newForm.player = player;
            newForm.money = money;
            newForm.Show();
            this.Visible = false;
        }
        private void RoundButton(Button btn)
        {
            GraphicsPath path = new GraphicsPath();

            int radius = 30; // было 20

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);

            path.CloseFigure();

            btn.Region = new Region(path);
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
            update();
            listBox1_SelectedIndexChanged(sender, e);

            label_lvl.Text = level.ToString();
            label_progress.Text = points.ToString() + "/300";
            label_money.Text = money.ToString();
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox2.DrawMode = DrawMode.OwnerDrawFixed;
            listBox3.DrawMode = DrawMode.OwnerDrawFixed;

            listBox1.ItemHeight = 100;
            listBox2.ItemHeight = 100;
            listBox3.ItemHeight = 100;

            //listBox1.DrawItem += listBox1_DrawItem;
            //listBox2.DrawItem += listBox2_DrawItem;
            //listBox3.DrawItem += listBox3_DrawItem;
            button5.Size = new Size(180, 55);
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;

            button5.BackColor = Color.FromArgb(194, 66, 112);
            button5.ForeColor = Color.White;

            button5.Font = new Font("Century Gothic", 16, FontStyle.Bold);

            RoundButton(button5);
            button5.Left = (panel3.Width - button5.Width) / 2;

        }
        private void panelItemInfo_Resize(object sender, EventArgs e)
        {
            button5.Left = (panel3.Width - button5.Width) / 2;
        }
        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.BringToFront();
            listBox1.SelectedIndex = 0;
            listBox1_SelectedIndexChanged(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.BringToFront();
            listBox2.SelectedIndex = 0;
            listBox2_SelectedIndexChanged(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox3.BringToFront();
            listBox3.SelectedIndex = 0;
            listBox3_SelectedIndexChanged(sender, e);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentItemType = "bait";
            button5.Enabled = true;
            label7.Visible = false;
            label5.Visible = true;
            string selectedItem = listBox1.SelectedItem.ToString();

            label2.Text = selectedItem;

            switch (selectedItem)
            {
                case "Червь":
                    price = 3.5;
                    pictureBox3.BackgroundImage = Resources.worm_m;
                    select = worm;
                    break;

                case "Опарыш":
                    price = 4.2;
                    pictureBox3.BackgroundImage = Resources.oparysh_m;
                    select = oparysh;
                    break;

                case "Кузнечик":
                    price = 6.4;
                    pictureBox3.BackgroundImage = Resources.kuznec_m;
                    select = kuznec;
                    break;

                case "Ручейник":
                    price = 7.2;
                    pictureBox3.BackgroundImage = Resources.rucheinik_m;
                    select = rucheinik;
                    break;

                case "Живец":
                    price = 10;
                    pictureBox3.BackgroundImage = Resources.zhivets_m;
                    select = zhivets;
                    break;
            }

            label6.Text = "Цена - " + price;

            label4.Text = "У вас в наличие - " + select + " шт.";
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentItemType = "rod";
            button5.Enabled = true;
            label7.Visible = false;
            label5.Visible = false;
            string selectedItem = listBox2.SelectedItem.ToString();

            label2.Text = selectedItem;

            switch (selectedItem)
            {
                case "Удочка 1":
                    price = 0;
                    pictureBox3.BackgroundImage = Resources.rod1;
                    select = rod1;
                    break;

                case "Удочка 2":
                    price = 35;
                    pictureBox3.BackgroundImage = Resources.rod2;
                    select = rod2;
                    break;

                case "Удочка 3":
                    price = 90;
                    pictureBox3.BackgroundImage = Resources.rod3;
                    select = rod3;
                    break;

                case "Удочка 4":
                    price = 395;
                    pictureBox3.BackgroundImage = Resources.rod4;
                    select = rod4;
                    break;

                case "Удочка 5":
                    price = 1000;
                    pictureBox3.BackgroundImage = Resources.rod5;
                    select = rod5;
                    break;
            }

            label6.Text = "Цена - " + price;

            if (select == 1)
            {
                label4.Text = "Уже у вас в наличие";
                button5.Enabled = false;
            }

            if (select == 0)
                label4.Text = "Отсутствует у вас";
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentItemType = "popl";
            button5.Enabled = true;
            label7.Visible = false;
            label5.Visible = false;
            string selectedItem = listBox3.SelectedItem.ToString();

            label2.Text = selectedItem;

            switch (selectedItem)
            {
                case "Поплавок 1":
                    price = 0;
                    pictureBox3.BackgroundImage = Resources.popl1;
                    select = popl1;
                    break;

                case "Поплавок 2":
                    price = 25;
                    pictureBox3.BackgroundImage = Resources.popl2;
                    select = popl2;
                    break;

                case "Поплавок 3":
                    price = 65;
                    pictureBox3.BackgroundImage = Resources.popl3;
                    select = popl3;
                    break;

                case "Поплавок 4":
                    price = 260;
                    pictureBox3.BackgroundImage = Resources.popl4;
                    select = popl4;
                    break;

                case "Поплавок 5":
                    price = 700;
                    pictureBox3.BackgroundImage = Resources.popl5;
                    select = popl5;
                    break;
            }

            label6.Text = "Цена - " + price;


            if (select == 1)
            {
                label4.Text = "Уже у вас в наличие";
                button5.Enabled = false;
            }

            if (select == 0)
                label4.Text = "Отсутствует у вас";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (money >= price)
            {
                money -= price;
                money = Math.Round(money, 2);
                label_money.Text = money.ToString();

                if (currentItemType == "bait")
                {
                    select += 10; 

                    string item = listBox1.SelectedItem.ToString();
                    if (item == "Червь") worm = select;
                    else if (item == "Опарыш") oparysh = select;
                    else if (item == "Кузнечик") kuznec = select;
                    else if (item == "Ручейник") rucheinik = select;
                    else if (item == "Живец") zhivets = select;

                    label4.Text = "У вас в наличие - " + select + " шт.";
                }

                else if (currentItemType == "rod")
                {
                    select = 1;

                    string item = listBox2.SelectedItem.ToString();
                    if (item == "Удочка 1") rod1 = select;
                    else if (item == "Удочка 2") rod2 = select;
                    else if (item == "Удочка 3") rod3 = select;
                    else if (item == "Удочка 4") rod4 = select;
                    else if (item == "Удочка 5") rod5 = select;

                    label4.Text = "Уже у вас в наличие";
                    button5.Enabled = false;
                }

                else if (currentItemType == "popl")
                {
                    select = 1;

                    string item = listBox3.SelectedItem.ToString();
                    if (item == "Поплавок 1") popl1 = select;
                    else if (item == "Поплавок 2") popl2 = select;
                    else if (item == "Поплавок 3") popl3 = select;
                    else if (item == "Поплавок 4") popl4 = select;
                    else if (item == "Поплавок 5") popl5 = select;

                    label4.Text = "Уже у вас в наличие";
                    button5.Enabled = false;
                }

                save();
                update();
            }

            else
            {
                label7.Visible = true;
            }
        }

        private void save()
        {
            string query = "UPDATE users SET money = @money, worm = @worm, oparysh = @oparysh, kuznec = @kuznec, rucheinik = @rucheinik, zhivets = @zhivets, popl1 = @popl1, popl2 = @popl2, popl3 = @popl3, popl4 = @popl4, popl5 = @popl5, rod1 = @rod1, rod2 = @rod2, rod3 = @rod3, rod4 = @rod4, rod5 = @rod5 WHERE name = @player";

            using (SQLiteConnection connection = new SQLiteConnection(connectStr))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);

                    cmd.Parameters.AddWithValue("@money", money);

                    cmd.Parameters.AddWithValue("@worm", worm);
                    cmd.Parameters.AddWithValue("@oparysh", oparysh);
                    cmd.Parameters.AddWithValue("@kuznec", kuznec);
                    cmd.Parameters.AddWithValue("@rucheinik", rucheinik);
                    cmd.Parameters.AddWithValue("@zhivets", zhivets);

                    cmd.Parameters.AddWithValue("@popl1", popl1);
                    cmd.Parameters.AddWithValue("@popl2", popl2);
                    cmd.Parameters.AddWithValue("@popl3", popl3);
                    cmd.Parameters.AddWithValue("@popl4", popl4);
                    cmd.Parameters.AddWithValue("@popl5", popl5);

                    cmd.Parameters.AddWithValue("@rod1", rod1);
                    cmd.Parameters.AddWithValue("@rod2", rod2);
                    cmd.Parameters.AddWithValue("@rod3", rod3);
                    cmd.Parameters.AddWithValue("@rod4", rod4);
                    cmd.Parameters.AddWithValue("@rod5", rod5);

                    cmd.Parameters.AddWithValue("@player", player);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения: " + ex.Message);
                }
            }
        }
        private void Button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(214, 86, 132);
        }

        private void Button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.FromArgb(194, 66, 112);
        }
        private void update()
        {
            string query = "SELECT money, worm, oparysh, kuznec, rucheinik, zhivets, popl1, popl2, popl3, popl4, popl5, rod1, rod2, rod3, rod4, rod5 FROM users WHERE name = @player";

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
                            money = reader.GetDouble(0);

                            worm = reader.GetInt32(1);
                            oparysh = reader.GetInt32(2);
                            kuznec = reader.GetInt32(3);
                            rucheinik = reader.GetInt32(4);
                            zhivets = reader.GetInt32(5);

                            popl1 = reader.GetInt32(6);
                            popl2 = reader.GetInt32(7);
                            popl3 = reader.GetInt32(8);
                            popl4 = reader.GetInt32(9);
                            popl5 = reader.GetInt32(10);

                            rod1 = reader.GetInt32(11);
                            rod2 = reader.GetInt32(12);
                            rod3 = reader.GetInt32(13);
                            rod4 = reader.GetInt32(14);
                            rod5 = reader.GetInt32(15);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения или запроса: " + ex.Message);
                }
            }
        }
    }
}