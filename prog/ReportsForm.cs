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
    public partial class ReportsForm : Form
    {
        private EmployeeService _service;

        public ReportsForm(EmployeeService service)
        {
            InitializeComponent2();
            _service = service;
        }

        private void InitializeComponent2()
        {
            this.btnEmployeeList = new Button();
            this.btnActiveEmployees = new Button();
            this.btnExportExcel = new Button();
            this.btnClose = new Button();
            this.SuspendLayout();

            this.btnEmployeeList.Text = "Список всех сотрудников";
            this.btnEmployeeList.Location = new System.Drawing.Point(30, 30);
            this.btnEmployeeList.Size = new System.Drawing.Size(200, 40);
            this.btnEmployeeList.Click += new EventHandler(this.btnEmployeeList_Click);

            this.btnActiveEmployees.Text = "Список работающих сотрудников";
            this.btnActiveEmployees.Location = new System.Drawing.Point(30, 90);
            this.btnActiveEmployees.Size = new System.Drawing.Size(200, 40);
            this.btnActiveEmployees.Click += new EventHandler(this.btnActiveEmployees_Click);

            this.btnExportExcel.Text = "Экспорт в Excel";
            this.btnExportExcel.Location = new System.Drawing.Point(30, 150);
            this.btnExportExcel.Size = new System.Drawing.Size(200, 40);
            this.btnExportExcel.Click += new EventHandler(this.btnExportExcel_Click);

            this.btnClose.Text = "Закрыть";
            this.btnClose.Location = new System.Drawing.Point(30, 210);
            this.btnClose.Size = new System.Drawing.Size(200, 40);
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            this.Text = "Формирование отчётов";
            this.Size = new System.Drawing.Size(280, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Button btnEmployeeList, btnActiveEmployees, btnExportExcel, btnClose;

        private void ShowReport(string title, List<ARM_Отдела_кадров.Models.Employee> employees)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Отчёт: {title}");
            sb.AppendLine($"Дата формирования: {DateTime.Now:dd.MM.yyyy}");
            sb.AppendLine(new string('-', 80));
            sb.AppendLine($"{"Фамилия",-15} {"Имя",-12} {"Должность",-20} {"Дата приёма",-12} {"Статус",-10}");
            sb.AppendLine(new string('-', 80));

            foreach (var emp in employees)
            {
                string positionName = _service.GetPositionNameById(emp.PositionID);
                sb.AppendLine($"{emp.LastName,-15} {emp.FirstName,-12} {positionName,-20} {emp.HireDate:dd.MM.yyyy,-12} {emp.Status,-10}");
            }

            sb.AppendLine(new string('-', 80));
            sb.AppendLine($"Всего сотрудников: {employees.Count}");

            MessageBox.Show(sb.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEmployeeList_Click(object sender, EventArgs e)
        {
            var employees = _service.GetAllEmployees();
            ShowReport("Список всех сотрудников", employees);
        }

        private void btnActiveEmployees_Click(object sender, EventArgs e)
        {
            var employees = _service.GetActiveEmployees();
            ShowReport("Список работающих сотрудников", employees);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv";
            saveFileDialog.Title = "Сохранить отчёт";
            saveFileDialog.FileName = $"Отчёт_кадры_{DateTime.Now:yyyyMMdd}";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var employees = _service.GetAllEmployees();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Фамилия;Имя;Отчество;Дата рождения;Телефон;Должность;Дата приёма;Статус");

                    foreach (var emp in employees)
                    {
                        string positionName = _service.GetPositionNameById(emp.PositionID);
                        sb.AppendLine($"{emp.LastName};{emp.FirstName};{emp.MiddleName};{emp.BirthDate:dd.MM.yyyy};{emp.Phone};{positionName};{emp.HireDate:dd.MM.yyyy};{emp.Status}");
                    }

                    System.IO.File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Отчёт успешно сохранён", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
