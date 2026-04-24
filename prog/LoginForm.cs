using ARM_Отдела_кадров.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARM_Отдела_кадров
{
    public partial class LoginForm : Form
    {
        private EmployeeService _service;
        private Label lblLogin;
        private Label lblPassword;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        public LoginForm()
        {
            //InitializeComponent();
            InitializeComponent2();
            _service = new EmployeeService();
        }

        private void InitializeComponent2()
        {
            // Создание элементов управления
            this.lblLogin = new Label();
            this.lblPassword = new Label();
            this.txtLogin = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnExit = new Button();
            this.SuspendLayout();

            // Метка "Логин"
            this.lblLogin.Text = "Логин:";
            this.lblLogin.Location = new System.Drawing.Point(30, 30);
            this.lblLogin.Size = new System.Drawing.Size(60, 25);

            // Метка "Пароль"
            this.lblPassword.Text = "Пароль:";
            this.lblPassword.Location = new System.Drawing.Point(30, 70);
            this.lblPassword.Size = new System.Drawing.Size(60, 25);

            // Поле ввода логина
            this.txtLogin.Location = new System.Drawing.Point(100, 30);
            this.txtLogin.Size = new System.Drawing.Size(150, 23);

            // Поле ввода пароля
            this.txtPassword.Location = new System.Drawing.Point(100, 70);
            this.txtPassword.Size = new System.Drawing.Size(150, 23);
            this.txtPassword.UseSystemPasswordChar = true;

            // Кнопка входа
            this.btnLogin.Text = "Вход";
            this.btnLogin.Location = new System.Drawing.Point(70, 120);
            this.btnLogin.Size = new System.Drawing.Size(80, 30);
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // Кнопка выхода
            this.btnExit.Text = "Выход";
            this.btnExit.Location = new System.Drawing.Point(170, 120);
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.Click += new EventHandler(this.btnExit_Click);

            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblLogin);

            // Настройки формы
            this.Text = "Авторизация";
            this.Size = new System.Drawing.Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string login = txtLogin.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите логин и пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = _service.ValidateUser(login, password);
                if (user != null)
                {
                    MainForm mainForm = new MainForm(user.Role);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе в систему: {ex.Message}", "Критическая ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
