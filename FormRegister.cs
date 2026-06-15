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

namespace Barbie_on_the_Kama
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }
        string connectStr = @"Data Source=barbie.db;Version=3;";
        private void btnCreateProfile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUserName.Text) &&
                !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                try
                {
                    using (SQLiteConnection connectSQL =
                           new SQLiteConnection(connectStr))
                    {
                        connectSQL.Open();

                        SQLiteCommand commandSQL =
                            new SQLiteCommand(
                            "INSERT INTO users (name, password, level, points, money) " +
                            "VALUES(@name, @password, 0, 0, 0)",
                            connectSQL);

                        commandSQL.Parameters.AddWithValue(
                            "@name",
                            txtUserName.Text);

                        string hashedPassword =
                     PasswordHasher.Hash(txtPassword.Text);

                        commandSQL.Parameters.AddWithValue(
                            "@password",
                            hashedPassword);

                        commandSQL.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        "Профиль успешно создан!",
                        "Успех",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Form1 loginForm = new Form1();

                    loginForm.Show();

                    this.Close();
                }
                catch (SQLiteException ex)
                {
                    if (ex.Message.Contains("UNIQUE"))
                    {
                        MessageBox.Show(
                            "Это имя уже занято!",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(
                            ex.Message,
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Заполните все поля!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ===========================
        // КНОПКА НАЗАД
        // btnBack
        // ===========================

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();

            loginForm.Show();

            this.Close();
        }

        // ===========================
        // Закрытие формы
        // ===========================

        private void FormRegister_FormClosed(
            object sender,
            FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox_see_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void pictureBox_see_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
        e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z')) && e.KeyChar != (char)Keys.Back) e.Handled = true;

        }
    }
}
