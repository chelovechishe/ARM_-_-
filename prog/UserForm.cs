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
    public partial class UserForm : Form
    {
        private EmployeeService _service;
        private int _userId = -1;
        private bool _isEditMode = false;
        private string _currentUserRole; // Роль текущего пользователя для ограничений

        public UserForm(EmployeeService service, string currentUserRole, int userId = -1)
        {
            InitializeComponent2();
            _service = service;
            _currentUserRole = currentUserRole;

            if (userId > 0)
            {
                _isEditMode = true;
                _userId = userId;
                this.Text = "Редактирование пользователя";
                btnSave.Text = "Сохранить";
                LoadUserData();
            }
            else
            {
                this.Text = "Добавление пользователя";
                btnSave.Text = "Создать";
            }

            ConfigureAccessByRole();
        }

        private void InitializeComponent2()
        {
            this.lblLogin = new Label();
            this.lblPassword = new Label();
            this.lblConfirmPassword = new Label();
            this.lblRole = new Label();

            this.txtLogin = new TextBox();
            this.txtPassword = new TextBox();
            this.txtConfirmPassword = new TextBox();
            this.cmbRole = new ComboBox();

            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            int y = 30;
            int labelX = 30;
            int fieldX = 150;
            int fieldWidth = 200;
            int spacing = 40;

            // Логин
            this.lblLogin.Text = "Логин:*";
            this.lblLogin.Location = new System.Drawing.Point(labelX, y);
            this.lblLogin.Size = new System.Drawing.Size(100, 25);

            this.txtLogin.Location = new System.Drawing.Point(fieldX, y);
            this.txtLogin.Size = new System.Drawing.Size(fieldWidth, 27);
            y += spacing;

            // Пароль
            this.lblPassword.Text = "Пароль:*";
            this.lblPassword.Location = new System.Drawing.Point(labelX, y);
            this.lblPassword.Size = new System.Drawing.Size(100, 25);

            this.txtPassword.Location = new System.Drawing.Point(fieldX, y);
            this.txtPassword.Size = new System.Drawing.Size(fieldWidth, 27);
            this.txtPassword.UseSystemPasswordChar = true;
            y += spacing;

            // Подтверждение пароля
            this.lblConfirmPassword.Text = "Подтверждение:*";
            this.lblConfirmPassword.Location = new System.Drawing.Point(labelX, y);
            this.lblConfirmPassword.Size = new System.Drawing.Size(100, 25);

            this.txtConfirmPassword.Location = new System.Drawing.Point(fieldX, y);
            this.txtConfirmPassword.Size = new System.Drawing.Size(fieldWidth, 27);
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            y += spacing;

            // Роль
            this.lblRole.Text = "Роль:*";
            this.lblRole.Location = new System.Drawing.Point(labelX, y);
            this.lblRole.Size = new System.Drawing.Size(100, 25);

            this.cmbRole.Location = new System.Drawing.Point(fieldX, y);
            this.cmbRole.Size = new System.Drawing.Size(fieldWidth, 27);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] { "User", "Administrator" });
            this.cmbRole.SelectedIndex = 0;
            y += spacing + 10;

            // Кнопки
            this.btnSave.Location = new System.Drawing.Point(fieldX - 100, y);
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Отмена";
            this.btnCancel.Location = new System.Drawing.Point(fieldX + 30, y);
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // Настройки формы
            this.Text = "Пользователь";
            this.Size = new System.Drawing.Size(420, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblLogin);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblLogin, lblPassword, lblConfirmPassword, lblRole;
        private TextBox txtLogin, txtPassword, txtConfirmPassword;
        private ComboBox cmbRole;
        private Button btnSave, btnCancel;

        private void ConfigureAccessByRole()
        {
            // Если текущий пользователь не администратор, он не может создавать/редактировать пользователей
            if (_currentUserRole != "Administrator")
            {
                txtLogin.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                cmbRole.Enabled = false;
                btnSave.Enabled = false;
                MessageBox.Show("У вас недостаточно прав для управления пользователями",
                    "Доступ запрещён", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // При редактировании нельзя изменить роль, если это последний администратор
            if (_isEditMode && _userId > 0)
            {
                var user = _service.GetUserById(_userId);
                if (user?.Role == "Administrator" && _service.GetAllUsers().Count(u => u.Role == "Administrator") <= 1)
                {
                    cmbRole.Enabled = false;
                }
            }
        }

        private void LoadUserData()
        {
            var user = _service.GetUserById(_userId);
            if (user != null)
            {
                txtLogin.Text = user.Login;
                cmbRole.SelectedItem = user.Role;
                // Пароль не загружаем для безопасности, пользователь введёт новый
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return false;
            }

            if (!_isEditMode || (!string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtConfirmPassword.Text)))
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Введите пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Пароль и подтверждение не совпадают", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }

                if (txtPassword.Text.Length < 3)
                {
                    MessageBox.Show("Пароль должен содержать не менее 3 символов", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }
            }

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль пользователя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem.ToString();

            bool success;
            if (_isEditMode)
            {
                // Если поле пароля пустое при редактировании, оставляем старый пароль
                if (string.IsNullOrEmpty(password))
                {
                    var existingUser = _service.GetUserById(_userId);
                    if (existingUser != null)
                    {
                        password = existingUser.Password;
                    }
                }
                success = _service.UpdateUser(_userId, login, password, role);
            }
            else
            {
                success = _service.AddUser(login, password, role);
            }

            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
