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
    public partial class HireForm : Form
    {
        private EmployeeService _service;

        public HireForm(EmployeeService service)
        {
            InitializeComponent2();
            _service = service;
            LoadPositions();
            dtpHireDate.Value = DateTime.Today;
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

            this.btnSave.Text = "Принять на работу";
            this.btnSave.Location = new System.Drawing.Point(fieldX - 100, y);
            this.btnSave.Size = new System.Drawing.Size(150, 30);
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Отмена";
            this.btnCancel.Location = new System.Drawing.Point(fieldX + 60, y);
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            this.Text = "Приём на работу";
            this.Size = new System.Drawing.Size(500, 550);
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

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpBirthDate.Value > DateTime.Now.AddYears(-14))
            {
                MessageBox.Show("Сотрудник должен быть старше 14 лет", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbPosition.SelectedItem == null)
            {
                MessageBox.Show("Выберите должность", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

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
                PositionID = (int)cmbPosition.SelectedValue,
                Status = "Работает"
            };

            if (_service.AddEmployee(employee))
            {
                MessageBox.Show("Сотрудник успешно принят на работу", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
