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
    public partial class TransferForm : Form
    {
        private EmployeeService _service;
        private int _employeeId;
        private string _employeeName;

        public TransferForm(EmployeeService service, int employeeId, string employeeName)
        {
            InitializeComponent2();
            _service = service;
            _employeeId = employeeId;
            _employeeName = employeeName;
            LoadPositions();
            dtpTransferDate.Value = DateTime.Today;
            lblEmployeeName.Text = $"Сотрудник: {_employeeName}";
        }

        private void InitializeComponent2()
        {
            this.lblEmployeeName = new Label();
            this.lblNewPosition = new Label();
            this.lblTransferDate = new Label();
            this.cmbNewPosition = new ComboBox();
            this.dtpTransferDate = new DateTimePicker();
            this.btnTransfer = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            this.lblEmployeeName.Text = "Сотрудник:";
            this.lblEmployeeName.Location = new System.Drawing.Point(20, 20);
            this.lblEmployeeName.Size = new System.Drawing.Size(350, 25);

            this.lblNewPosition.Text = "Новая должность:*";
            this.lblNewPosition.Location = new System.Drawing.Point(20, 60);
            this.lblNewPosition.Size = new System.Drawing.Size(150, 25);

            this.cmbNewPosition.Location = new System.Drawing.Point(180, 60);
            this.cmbNewPosition.Size = new System.Drawing.Size(200, 23);
            this.cmbNewPosition.DisplayMember = "PositionName";
            this.cmbNewPosition.ValueMember = "PositionID";

            this.lblTransferDate.Text = "Дата перевода:*";
            this.lblTransferDate.Location = new System.Drawing.Point(20, 100);
            this.lblTransferDate.Size = new System.Drawing.Size(150, 25);

            this.dtpTransferDate.Location = new System.Drawing.Point(180, 100);
            this.dtpTransferDate.Size = new System.Drawing.Size(200, 23);
            this.dtpTransferDate.Format = DateTimePickerFormat.Short;

            this.btnTransfer.Text = "Перевести";
            this.btnTransfer.Location = new System.Drawing.Point(100, 150);
            this.btnTransfer.Size = new System.Drawing.Size(100, 30);
            this.btnTransfer.Click += new EventHandler(this.btnTransfer_Click);

            this.btnCancel.Text = "Отмена";
            this.btnCancel.Location = new System.Drawing.Point(220, 150);
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            this.Text = "Перевод сотрудника";
            this.Size = new System.Drawing.Size(420, 230);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblEmployeeName, lblNewPosition, lblTransferDate;
        private ComboBox cmbNewPosition;
        private DateTimePicker dtpTransferDate;
        private Button btnTransfer, btnCancel;

        private void LoadPositions()
        {
            var positions = _service.GetAllPositions();
            cmbNewPosition.DataSource = positions;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (cmbNewPosition.SelectedItem == null)
            {
                MessageBox.Show("Выберите новую должность", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int newPositionId = (int)cmbNewPosition.SelectedValue;

            if (_service.TransferEmployee(_employeeId, newPositionId, dtpTransferDate.Value))
            {
                MessageBox.Show("Сотрудник успешно переведён", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка при переводе", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
