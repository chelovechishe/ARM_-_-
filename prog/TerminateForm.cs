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
    public partial class TerminateForm : Form
    {
        private EmployeeService _service;
        private int _employeeId;
        private string _employeeName;

        public TerminateForm(EmployeeService service, int employeeId, string employeeName)
        {
            InitializeComponent2();
            _service = service;
            _employeeId = employeeId;
            _employeeName = employeeName;
            dtpTerminationDate.Value = DateTime.Today;
            lblEmployeeName.Text = $"Сотрудник: {_employeeName}";
        }

        private void InitializeComponent2()
        {
            this.lblEmployeeName = new Label();
            this.lblTerminationDate = new Label();
            this.lblReason = new Label();
            this.dtpTerminationDate = new DateTimePicker();
            this.txtReason = new TextBox();
            this.btnTerminate = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            this.lblEmployeeName.Text = "Сотрудник:";
            this.lblEmployeeName.Location = new System.Drawing.Point(20, 20);
            this.lblEmployeeName.Size = new System.Drawing.Size(350, 25);

            this.lblTerminationDate.Text = "Дата увольнения:*";
            this.lblTerminationDate.Location = new System.Drawing.Point(20, 60);
            this.lblTerminationDate.Size = new System.Drawing.Size(150, 25);

            this.dtpTerminationDate.Location = new System.Drawing.Point(180, 60);
            this.dtpTerminationDate.Size = new System.Drawing.Size(200, 23);
            this.dtpTerminationDate.Format = DateTimePickerFormat.Short;

            this.lblReason.Text = "Причина увольнения:";
            this.lblReason.Location = new System.Drawing.Point(20, 100);
            this.lblReason.Size = new System.Drawing.Size(150, 25);

            this.txtReason.Location = new System.Drawing.Point(180, 100);
            this.txtReason.Size = new System.Drawing.Size(200, 23);

            this.btnTerminate.Text = "Уволить";
            this.btnTerminate.Location = new System.Drawing.Point(100, 150);
            this.btnTerminate.Size = new System.Drawing.Size(100, 30);
            this.btnTerminate.Click += new EventHandler(this.btnTerminate_Click);

            this.btnCancel.Text = "Отмена";
            this.btnCancel.Location = new System.Drawing.Point(220, 150);
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            this.Text = "Увольнение сотрудника";
            this.Size = new System.Drawing.Size(420, 230);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblEmployeeName, lblTerminationDate, lblReason;
        private DateTimePicker dtpTerminationDate;
        private TextBox txtReason;
        private Button btnTerminate, btnCancel;

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Вы уверены, что хотите уволить сотрудника {_employeeName}?",
                "Подтверждение увольнения", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (_service.TerminateEmployee(_employeeId, dtpTerminationDate.Value))
                {
                    MessageBox.Show("Сотрудник успешно уволен", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при увольнении", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
