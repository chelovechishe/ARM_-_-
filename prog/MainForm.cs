using ARM_Отдела_кадров.Models;
using ARM_Отдела_кадров.Services;

namespace ARM_Отдела_кадров
{
    public partial class MainForm : Form
    {
        
        private EmployeeService _service;
        private List<Employee> _employees;
        private string _userRole;

        public MainForm(string role)
        {
            InitializeComponent();
            InitializeComponent2();
            _service = new EmployeeService();
            _userRole = role;
            LoadEmployees();
            ConfigureAccessByRole();
        }

        private void InitializeComponent2()
        {
            this.dgvEmployees = new DataGridView();
            this.txtSearch = new TextBox();
            this.btnSearch = new Button();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnHire = new Button();
            this.btnTransfer = new Button();
            this.btnTerminate = new Button();
            this.btnReports = new Button();
            this.statusStrip = new StatusStrip();
            this.lblStatus = new ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.SuspendLayout();

            this.dgvEmployees.Location = new System.Drawing.Point(12, 50);
            this.dgvEmployees.Size = new System.Drawing.Size(860, 400);
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.MultiSelect = false;
            this.dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            this.txtSearch.Location = new System.Drawing.Point(12, 15);
            this.txtSearch.Size = new System.Drawing.Size(250, 23);
            this.txtSearch.Text = "Введите фамилию для поиска...";
            this.txtSearch.Enter += new EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new EventHandler(this.txtSearch_Leave);

            this.btnSearch.Text = "Найти";
            this.btnSearch.Location = new System.Drawing.Point(270, 14);
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);

            this.btnAdd.Text = "Добавить";
            this.btnAdd.Location = new System.Drawing.Point(380, 14);
            this.btnAdd.Size = new System.Drawing.Size(90, 25);
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);

            this.btnEdit.Text = "Редактировать";
            this.btnEdit.Location = new System.Drawing.Point(480, 14);
            this.btnEdit.Size = new System.Drawing.Size(90, 25);
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);

            this.btnDelete.Text = "Удалить";
            this.btnDelete.Location = new System.Drawing.Point(580, 14);
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnHire.Text = "Приём";
            this.btnHire.Location = new System.Drawing.Point(380, 460);
            this.btnHire.Size = new System.Drawing.Size(90, 30);
            this.btnHire.Click += new EventHandler(this.btnHire_Click);

            this.btnTransfer.Text = "Перевод";
            this.btnTransfer.Location = new System.Drawing.Point(480, 460);
            this.btnTransfer.Size = new System.Drawing.Size(90, 30);
            this.btnTransfer.Click += new EventHandler(this.btnTransfer_Click);

            this.btnTerminate.Text = "Увольнение";
            this.btnTerminate.Location = new System.Drawing.Point(580, 460);
            this.btnTerminate.Size = new System.Drawing.Size(90, 30);
            this.btnTerminate.Click += new EventHandler(this.btnTerminate_Click);

            this.btnReports.Text = "Отчёты";
            this.btnReports.Location = new System.Drawing.Point(780, 460);
            this.btnReports.Size = new System.Drawing.Size(90, 30);
            this.btnReports.Click += new EventHandler(this.btnReports_Click);

            this.statusStrip.Items.Add(this.lblStatus);
            this.statusStrip.Location = new System.Drawing.Point(0, 500);
            this.statusStrip.Size = new System.Drawing.Size(884, 22);

            this.Text = "АРМ Сотрудник отдела кадров";
            this.Size = new System.Drawing.Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.statusStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DataGridView dgvEmployees;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnHire;
        private Button btnTransfer;
        private Button btnTerminate;
        private Button btnReports;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;

        private void LoadEmployees()
        {
            try
            {
                _employees = _service.GetAllEmployees();
                var bindingList = new System.ComponentModel.BindingList<Employee>(_employees);
                var source = new System.Windows.Forms.BindingSource(bindingList, null);
                dgvEmployees.DataSource = source;

                if (dgvEmployees.Columns.Contains("EmployeeID"))
                    dgvEmployees.Columns["EmployeeID"].Visible = false;
                if (dgvEmployees.Columns.Contains("PositionID"))
                    dgvEmployees.Columns["PositionID"].Visible = false;
                if (dgvEmployees.Columns.Contains("PersonnelEvents"))
                    dgvEmployees.Columns["PersonnelEvents"].Visible = false;
                if (dgvEmployees.Columns.Contains("Position"))
                    dgvEmployees.Columns["Position"].Visible = false;

                if (dgvEmployees.Columns.Contains("LastName"))
                    dgvEmployees.Columns["LastName"].HeaderText = "Фамилия";
                if (dgvEmployees.Columns.Contains("FirstName"))
                    dgvEmployees.Columns["FirstName"].HeaderText = "Имя";
                if (dgvEmployees.Columns.Contains("MiddleName"))
                    dgvEmployees.Columns["MiddleName"].HeaderText = "Отчество";
                if (dgvEmployees.Columns.Contains("BirthDate"))
                    dgvEmployees.Columns["BirthDate"].HeaderText = "Дата рождения";
                if (dgvEmployees.Columns.Contains("PassportSeries"))
                    dgvEmployees.Columns["PassportSeries"].HeaderText = "Серия паспорта";
                if (dgvEmployees.Columns.Contains("PassportNumber"))
                    dgvEmployees.Columns["PassportNumber"].HeaderText = "Номер паспорта";
                if (dgvEmployees.Columns.Contains("Address"))
                    dgvEmployees.Columns["Address"].HeaderText = "Адрес";
                if (dgvEmployees.Columns.Contains("Phone"))
                    dgvEmployees.Columns["Phone"].HeaderText = "Телефон";
                if (dgvEmployees.Columns.Contains("HireDate"))
                    dgvEmployees.Columns["HireDate"].HeaderText = "Дата приёма";
                if (dgvEmployees.Columns.Contains("TerminationDate"))
                    dgvEmployees.Columns["TerminationDate"].HeaderText = "Дата увольнения";
                if (dgvEmployees.Columns.Contains("Status"))
                    dgvEmployees.Columns["Status"].HeaderText = "Статус";

                lblStatus.Text = $"Всего сотрудников: {_employees.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureAccessByRole()
        {
            if (_userRole == "User")
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnHire.Enabled = false;
                btnTransfer.Enabled = false;
                btnTerminate.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(searchText) || searchText == "Введите фамилию для поиска...")
                {
                    LoadEmployees();
                    return;
                }

                var filtered = _service.SearchEmployees(searchText);
                var bindingList = new System.ComponentModel.BindingList<Employee>(filtered);
                var source = new System.Windows.Forms.BindingSource(bindingList, null);
                dgvEmployees.DataSource = source;

                lblStatus.Text = $"Найдено: {filtered.Count} сотрудников";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Введите фамилию для поиска...")
                txtSearch.Text = "";
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
                txtSearch.Text = "Введите фамилию для поиска...";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm(_service);
            if (employeeForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника для редактирования", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedEmployee = (Employee)dgvEmployees.SelectedRows[0].DataBoundItem;
            EmployeeForm employeeForm = new EmployeeForm(_service, selectedEmployee.EmployeeID);
            if (employeeForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника для удаления", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedEmployee = (Employee)dgvEmployees.SelectedRows[0].DataBoundItem;
            string fullName = $"{selectedEmployee.LastName} {selectedEmployee.FirstName}";

            DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить сотрудника {fullName}?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_service.DeleteEmployee(selectedEmployee.EmployeeID))
                    {
                        MessageBox.Show("Сотрудник успешно удалён", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployees();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHire_Click(object sender, EventArgs e)
        {
            HireForm hireForm = new HireForm(_service);
            if (hireForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника для перевода", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedEmployee = (Employee)dgvEmployees.SelectedRows[0].DataBoundItem;
            string fullName = $"{selectedEmployee.LastName} {selectedEmployee.FirstName}";

            TransferForm transferForm = new TransferForm(_service, selectedEmployee.EmployeeID, fullName);
            if (transferForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника для увольнения", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedEmployee = (Employee)dgvEmployees.SelectedRows[0].DataBoundItem;
            string fullName = $"{selectedEmployee.LastName} {selectedEmployee.FirstName}";

            TerminateForm terminateForm = new TerminateForm(_service, selectedEmployee.EmployeeID, fullName);
            if (terminateForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsForm reportsForm = new ReportsForm(_service);
            reportsForm.ShowDialog();
        }
    }
}
