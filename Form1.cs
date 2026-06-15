using MySql.Data.MySqlClient;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static Hangfire.Storage.JobStorageFeatures;

namespace Barbie_on_the_Kama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connectStr = @"Data Source=barbie.db;Version=3;";
        string player; 
        int level; 
        double points, money;

    

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void input_users()
        {
            string query = "SELECT name FROM users";
            listBox_users.Items.Clear();

            using (SQLiteConnection connection = new SQLiteConnection(connectStr))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        listBox_users.Items.Add(reader["name"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private bool CheckPassword(string enteredPassword)
        {
            string query = "SELECT password FROM users WHERE name = @player";

            using (SQLiteConnection connection = new SQLiteConnection(connectStr))
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.AddWithValue("@player", player);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string enteredHash =
    PasswordHasher.Hash(enteredPassword);

                    return result.ToString() == enteredHash;
                }
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            input_users();
        }

        private void listBox_users_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_users.SelectedItem != null)
            {
                player = listBox_users.SelectedItem.ToString();
            }
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            if (player == null)
            {
                MessageBox.Show("Выберите пользователя из списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CheckPassword(password_tb.Text))
            {
                string query = "SELECT level, points, money FROM users WHERE name = @player";

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
                                level = reader.GetInt32(0);
                                points = reader.GetDouble(1);
                                money = reader.GetDouble(2);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка подключения или запроса: " + ex.Message);
                    }
                }

                Form2 newForm = new Form2();
                newForm.level = level;
                newForm.points = points;
                newForm.player = player;
                newForm.money = money;
                newForm.Show();
                this.Visible = false;
            }

            else
                MessageBox.Show("Пароль введен неверно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            if (player == null)
            {
                MessageBox.Show("Выберите пользователя из списка!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CheckPassword(password_tb.Text))
            {
                string query = "DELETE FROM users WHERE name = @player";

                DialogResult dialogResult = MessageBox.Show("Профиль будет удален безвозвратно, вы уверены что хотите продолжить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SQLiteConnection conn = new SQLiteConnection(connectStr))
                    {
                        try
                        {
                            conn.Open();
                            SQLiteCommand cmd = new SQLiteCommand(query, conn);
                            cmd.Parameters.AddWithValue("@player", player);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Профиль успешно удалён.");
                            input_users();
                            password_tb.Text = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: " + ex.Message);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Пароль введен неверно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void newplayer_label_Click(object sender, EventArgs e)
        {
            FormRegister registerForm = new FormRegister();

            registerForm.Show();

            this.Hide();
        }
        

       

        private void listBox_users_DoubleClick(object sender, EventArgs e)
        {
            start_btn_Click(sender, e);
        }


        // --- МОДЕРАЦИЯ ВВОДА ЛОГИНА И ПАРОЛЯ --- //

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) && !((e.KeyChar >= 'А' && e.KeyChar <= 'Я') || (e.KeyChar >= 'а' && e.KeyChar <= 'я')) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }

      

  

        private void listBox_users_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Выбираем цвета: если строка выделена -> розовый фон + чёрный текст, иначе -> стандартные
            Color back = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? Color.LightPink : e.BackColor;
            Color fore = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? Color.Black : e.ForeColor;

            // Рисуем фон
            using (Brush br = new SolidBrush(back))
                e.Graphics.FillRectangle(br, e.Bounds);

            // Рисуем текст с небольшим отступом
            using (Brush br = new SolidBrush(fore))
                e.Graphics.DrawString(listBox_users.Items[e.Index].ToString(), e.Font, br, e.Bounds.X + 2, e.Bounds.Y + 2);

            // Рисуем пунктирную рамку фокуса
            e.DrawFocusRectangle();
        }

        private void pictureBox_see_MouseDown(object sender, MouseEventArgs e)
        {
            password_tb.UseSystemPasswordChar = false;
        }

        private void pictureBox_see_MouseUp(object sender, MouseEventArgs e)
        {
            password_tb.UseSystemPasswordChar = true;
        }

        private void pictureBox_exit_Click(object sender, EventArgs e)
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

        private void pictureBox_inf_Click(object sender, EventArgs e)
        {
            FormHelp help = new FormHelp();
            help.ShowDialog();
        }

        private void pictureBox_set_Click(object sender, EventArgs e)
        {
            if (player == null)
            {
                MessageBox.Show(
                    "Сначала выберите профиль.",
                    "Настройки",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            FormSettings settings = new FormSettings();

            settings.PlayerName = player;

            settings.ShowDialog();
        }

        private void password_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) && e.KeyChar != (char)Keys.Back) e.Handled = true;
        }
    }
}