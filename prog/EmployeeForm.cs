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
    public partial class EmployeeForm : Form
    {
        private EmployeeService _service;
        private int _employeeId = -1;
        private bool _isEditMode = false;

        public EmployeeForm(EmployeeService service, int id = -1)
        {
            InitializeComponent2();
            _service = service;

            if (id > 0)
            {
                _isEditMode = true;
                _employeeId = id;
                this.Text = "Редактирование сотрудника";
                LoadEmployeeData();
            }
            else
            {
                this.Text = "Добавление сотрудника";
            }

            LoadPositions();
        }

        private void InitializeComponent2()
        {
            this.lblLastName = new Label();
            this.lblFirstName = new Label();
            this.lblMiddleName = new Label();
            this.lblBirthDate = new Label();
            this.lblPassportSeries = new Label();
            this.lblPassportNumber = new Label();
            this.lblPassportIssuedBy = new Label();
            this.lblPassportIssueDate = new Label();
            this.lblAddress = new Label();
            this.lblPhone = new Label();
            this.lblHireDate = new Label();
            this.lblPosition = new Label();

            this.txtLastName = new TextBox();
            this.txtFirstName = new TextBox();
            this.txtMiddleName = new TextBox();
            this.dtpBirthDate = new DateTimePicker();
            this.txtPassportSeries = new TextBox();
            this.txtPassportNumber = new TextBox();
            this.txtPassportIssuedBy = new TextBox();
            this.dtpPassportIssueDate = new DateTimePicker();
            this.txtAddress = new TextBox();
            this.txtPhone = new TextBox();
            this.dtpHireDate = new DateTimePicker();
            this.cmbPosition = new ComboBox();

            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            int y = 20;
            int labelX = 20;
            int fieldX = 180;
            int fieldWidth = 250;
            int spacing = 35;

            this.lblLastName.Text = "Фамилия:*";
            this.lblLastName.Location = new System.Drawing.Point(labelX, y);
            this.lblLastName.Size = new System.Drawing.Size(120, 25);

            this.txtLastName.Location = new System.Drawing.Point(fieldX, y);
            this.txtLastName.Size = new System.Drawing.Size(fieldWidth, 23);
            y += spacing;

            this.lblFirstName.Text = "Имя:*";
            this.lblFirstName.Location = new System.Drawing.Point(labelX, y);
            this.lblFirstName.Size = new System.Drawing.Size(120, 25);

            this.txtFirstName.Location = new System.Drawing.Point(fieldX, y);
            this.txtFirstName.Size = new System.Drawing.Size(fieldWidth, 23);
            y += spacing;

            this.lblMiddleName.Text = "Отчество:";
            this.lblMiddleName.Location = new System.Drawing.Point(labelX, y);
            this.lblMiddleName.Size = new System.Drawing.Size(120, 25);

            this.txtMiddleName.Location = new System.Drawing.Point(fieldX, y);
            this.txtMiddleName.Size = new System.Drawing.Size(fieldWidth, 23);
            y += spacing;

            this.lblBirthDate.Text = "Дата рождения:*";
            this.lblBirthDate.Location = new System.Drawing.Point(labelX, y);
            this.lblBirthDate.Size = new System.Drawing.Size(120, 25);

            this.dtpBirthDate.Location = new System.Drawing.Point(fieldX, y);
            this.dtpBirthDate.Size = new System.Drawing.Size(fieldWidth, 23);
            this.dtpBirthDate.Format = DateTimePickerFormat.Short;
            y += spacing;

            this.lblPassportSeries.Text = "Серия паспорта:*";
            this.lblPassportSeries.Location = new System.Drawing.Point(labelX, y);
            this.lblPassportSeries.Size = new System.Drawing.Size(120, 25);

            this.txtPassportSeries.Location = new System.Drawing.Point(fieldX, y);
            this.txtPassportSeries.Size = new System.Drawing.Size(fieldWidth, 23);
            y += spacing;

            this.lblPassportNumber.Text = "Номер паспорта:*";
            this.lblPassportNumber.Location = new System.Drawing.Point(labelX, y);
            this.lblPassportNumber.Size = new System.Drawing.Size(120, 25);

            this.txtPassportNumber.Location = new System.Drawing.Point(fieldX, y);
            this.txtPassportNumber.Size = new System.Drawing.Size(fieldWidth, 23);
            y += spacing;

            this.lblPassportIssuedBy.Text = "Кем выдан:*";
            this.lblPassportIssuedBy.Location = new System.Drawing.Point(labelX, y);
            this.lblPassportIssuedBy.Size = new System.Drawing.Size(120, 25);

            this.txtPassportIssuedBy.Location = new System.Drawing.Point(fieldX, y);
            this.txtPassportIssuedBy.Size = new System.Drawing.Size(fieldWidth, 23);
            y += spacing;

            this.lblPassportIssueDate.Text = "Дата выдачи:*";
            this.lblPassportIssueDate.Location = new System.Drawing.Point(labelX, y);
            this.lblPassportIssueDate.Size = new System.Drawing.Size(120, 25);

            this.dtpPassportIssueDate.Location = new System.Drawing.Point(fieldX, y);
            this.dtpPassportIssueDate.Size = new System.Drawing.Size(fieldWidth, 23);
            this.dtpPassportIssueDate.Format = DateTimePickerFormat.Short;
            y += spacing;

            this.lblAddress.Text = "Адрес:*";
            this.lblAddress.Location = new System.Drawing.Point(labelX, y);
            this.lblAddress.Size = new System.Drawing.Size(120, 25);

            this.txtAddress.Location = new System.Drawing.Point(fieldX, y);
            this.txtAddress.Size = new System.Drawing.Size(fieldWidth, 23);
            y += spacing;

            this.lblPhone.Text = "Телефон:*";
            this.lblPhone.Location = new System.Drawing.Point(labelX, y);
            this.lblPhone.Size = new System.Drawing.Size(120, 25);

            this.txtPhone.Location = new System.Drawing.Point(fieldX, y);
            this.txtPhone.Size = new System.Drawing.Size(fieldWidth, 23);
            y += spacing;

            this.lblHireDate.Text = "Дата приёма:*";
            this.lblHireDate.Location = new System.Drawing.Point(labelX, y);
            this.lblHireDate.Size = new System.Drawing.Size(120, 25);

            this.dtpHireDate.Location = new System.Drawing.Point(fieldX, y);
            this.dtpHireDate.Size = new System.Drawing.Size(fieldWidth, 23);
            this.dtpHireDate.Format = DateTimePickerFormat.Short;
            y += spacing;

            this.lblPosition.Text = "Должность:*";
            this.lblPosition.Location = new System.Drawing.Point(labelX, y);
            this.lblPosition.Size = new System.Drawing.Size(120, 25);

            this.cmbPosition.Location = new System.Drawing.Point(fieldX, y);
            this.cmbPosition.Size = new System.Drawing.Size(fieldWidth, 23);
            this.cmbPosition.DisplayMember = "PositionName";
            this.cmbPosition.ValueMember = "PositionID";
            y += spacing + 10;

            this.btnSave.Text = "Сохранить";
            this.btnSave.Location = new System.Drawing.Point(fieldX - 100, y);
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Отмена";
            this.btnCancel.Location = new System.Drawing.Point(fieldX + 20, y);
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            this.Text = "Сотрудник";
            this.Size = new System.Drawing.Size(500, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private TextBox txtLastName, txtFirstName, txtMiddleName, txtPassportSeries, txtPassportNumber;
        private TextBox txtPassportIssuedBy, txtAddress, txtPhone;
        private DateTimePicker dtpBirthDate, dtpPassportIssueDate, dtpHireDate;
        private ComboBox cmbPosition;
        private Button btnSave, btnCancel;
        private Label lblLastName, lblFirstName, lblMiddleName, lblBirthDate;
        private Label lblPassportSeries, lblPassportNumber, lblPassportIssuedBy, lblPassportIssueDate;
        private Label lblAddress, lblPhone, lblHireDate, lblPosition;

        private void LoadPositions()
        {
            var positions = _service.GetAllPositions();
            cmbPosition.DataSource = positions;
        }

        private void LoadEmployeeData()
        {
            var employee = _service.GetEmployeeById(_employeeId);
            if (employee == null) return;

            txtLastName.Text = employee.LastName;
            txtFirstName.Text = employee.FirstName;
            txtMiddleName.Text = employee.MiddleName;
            dtpBirthDate.Value = employee.BirthDate;
            txtPassportSeries.Text = employee.PassportSeries;
            txtPassportNumber.Text = employee.PassportNumber;
            txtPassportIssuedBy.Text = employee.PassportIssuedBy;
            dtpPassportIssueDate.Value = employee.PassportIssueDate;
            txtAddress.Text = employee.Address;
            txtPhone.Text = employee.Phone;
            dtpHireDate.Value = employee.HireDate;
            cmbPosition.SelectedValue = employee.PositionID;
        }

        private bool ValidateForm()
        {
            // Проверка фамилии
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Поле 'Фамилия' обязательно для заполнения.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            // Проверка имени
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Поле 'Имя' обязательно для заполнения.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            // Проверка даты рождения (не должна быть в будущем)
            if (dtpBirthDate.Value > DateTime.Now)
            {
                MessageBox.Show("Дата рождения не может быть в будущем.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Проверка возраста (не моложе 14 лет для работы)
            if (dtpBirthDate.Value > DateTime.Now.AddYears(-14))
            {
                MessageBox.Show("Сотрудник должен быть старше 14 лет.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Проверка паспортных данных
            if (string.IsNullOrWhiteSpace(txtPassportSeries.Text) ||
                string.IsNullOrWhiteSpace(txtPassportNumber.Text))
            {
                MessageBox.Show("Паспортные данные обязательны для заполнения.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Проверка серии паспорта (4 цифры)
            if (txtPassportSeries.Text.Length != 4 || !System.Text.RegularExpressions.Regex.IsMatch(txtPassportSeries.Text, @"^\d{4}$"))
            {
                MessageBox.Show("Серия паспорта должна состоять из 4 цифр.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Проверка номера паспорта (6 цифр)
            if (txtPassportNumber.Text.Length != 6 || !System.Text.RegularExpressions.Regex.IsMatch(txtPassportNumber.Text, @"^\d{6}$"))
            {
                MessageBox.Show("Номер паспорта должен состоять из 6 цифр.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Проверка телефона (простая проверка на наличие цифр)
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"[\d\-\+\(\)\s]{10,}"))
            {
                MessageBox.Show("Введите корректный номер телефона.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Проверка выбора должности
            if (cmbPosition.SelectedItem == null)
            {
                MessageBox.Show("Выберите должность сотрудника.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPosition.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                var employee = new Employee
                {
                    LastName = txtLastName.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(),
                    MiddleName = string.IsNullOrWhiteSpace(txtMiddleName.Text) ? null : txtMiddleName.Text.Trim(),
                    BirthDate = dtpBirthDate.Value,
                    PassportSeries = txtPassportSeries.Text.Trim(),
                    PassportNumber = txtPassportNumber.Text.Trim(),
                    PassportIssuedBy = txtPassportIssuedBy.Text.Trim(),
                    PassportIssueDate = dtpPassportIssueDate.Value,
                    Address = txtAddress.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    HireDate = dtpHireDate.Value,
                    PositionID = (int)cmbPosition.SelectedValue
                };

                bool success;
                if (_isEditMode)
                {
                    employee.EmployeeID = _employeeId;
                    success = _service.UpdateEmployee(employee);
                }
                else
                {
                    success = _service.AddEmployee(employee);
                }

                if (success)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении данных", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
