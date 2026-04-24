using ARM_Отдела_кадров.Models;
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
    public partial class UsersManagementForm : Form
    {
        private EmployeeService _service;
        private string _currentUserRole;
        private List<User> _users;

        public UsersManagementForm(EmployeeService service, string currentUserRole)
        {
            InitializeComponent2();
            _service = service;
            _currentUserRole = currentUserRole;
            LoadUsers();
            ConfigureAccessByRole();
        }

        private void InitializeComponent2()
        {
            this.dgvUsers = new DataGridView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();

            // DataGridView
            this.dgvUsers.Location = new System.Drawing.Point(12, 50);
            this.dgvUsers.Size = new System.Drawing.Size(560, 350);
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Кнопки
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnEdit.Text = "Редактировать";
            this.btnEdit.Location = new System.Drawing.Point(120, 12);
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);

            this.btnDelete.Text = "Удалить";
            this.btnDelete.Location = new System.Drawing.Point(230, 12);
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnClose.Text = "Закрыть";
            this.btnClose.Location = new System.Drawing.Point(480, 12);
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // Форма
            this.Text = "Управление пользователями";
            this.Size = new System.Drawing.Size(600, 450);
            this.StartPosition = FormStartPosition.CenterParent;

            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvUsers);

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
        }

        private DataGridView dgvUsers;
        private Button btnAdd, btnEdit, btnDelete, btnClose;

        private void LoadUsers()
        {
            _users = _service.GetAllUsers();
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = _users;

            if (dgvUsers.Columns.Contains("UserID"))
                dgvUsers.Columns["UserID"].Visible = false;
            if (dgvUsers.Columns.Contains("Password"))
                dgvUsers.Columns["Password"].Visible = false;

            dgvUsers.Columns["Login"].HeaderText = "Логин";
            dgvUsers.Columns["Role"].HeaderText = "Роль";
        }

        private void ConfigureAccessByRole()
        {
            if (_currentUserRole != "Administrator")
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var userForm = new UserForm(_service, _currentUserRole);
            if (userForm.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для редактирования", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedUser = (User)dgvUsers.SelectedRows[0].DataBoundItem;
            var userForm = new UserForm(_service, _currentUserRole, selectedUser.UserID);
            if (userForm.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пользователя для удаления", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedUser = (User)dgvUsers.SelectedRows[0].DataBoundItem;

            DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить пользователя {selectedUser.Login}?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _service.DeleteUser(selectedUser.UserID);
                LoadUsers();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
