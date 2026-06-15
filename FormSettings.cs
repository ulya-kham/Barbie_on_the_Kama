using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mysqlx.Datatypes.Scalar.Types;

namespace Barbie_on_the_Kama
{
    public partial class FormSettings : Form
    {
        private string connectStr = @"Data Source=barbie.db;Version=3;";
        public string PlayerPassword;
        public string PlayerName;
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            lblPlayer.Text = PlayerName;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeletePassword.Text))
            {
                MessageBox.Show(
                    "Введите пароль для подтверждения удаления.",
                    "Удаление профиля",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string passwordFromDb = "";

            try
            {
                using (SQLiteConnection connection =
                       new SQLiteConnection(connectStr))
                {
                    connection.Open();

                    string query =
                        "SELECT password FROM users WHERE name = @player";

                    SQLiteCommand cmd =
                        new SQLiteCommand(query, connection);

                    cmd.Parameters.AddWithValue("@player", PlayerName);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                        passwordFromDb = result.ToString();
                }

                string enteredHash =
      PasswordHasher.Hash(txtDeletePassword.Text);

                if (passwordFromDb != enteredHash)
                {
                    MessageBox.Show(
                        "Пароль введён неверно.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                DialogResult answer = MessageBox.Show(
                    "🌸 Вы уверены, что хотите удалить профиль?\n\n" +
                    "Все достижения, рекорды и прогресс будут потеряны.",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (answer == DialogResult.Yes)
                {
                    using (SQLiteConnection connection =
                           new SQLiteConnection(connectStr))
                    {
                        connection.Open();

                        string deleteQuery =
                            "DELETE FROM users WHERE name = @player";

                        SQLiteCommand cmd =
                            new SQLiteCommand(deleteQuery, connection);

                        cmd.Parameters.AddWithValue("@player", PlayerName);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        "Профиль успешно удалён.",
                        "Удаление профиля",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Application.Restart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void pictureBox_see_MouseUp(object sender, MouseEventArgs e)
        {
            txtDeletePassword.UseSystemPasswordChar = true;
        }

        private void pictureBox_see_MouseDown(object sender, MouseEventArgs e)
        {
            txtDeletePassword.UseSystemPasswordChar = false;
        }

        private void newplayer_label_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
