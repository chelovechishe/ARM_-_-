using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM_Отдела_кадров.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PassportSeries { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;
        public string PassportIssuedBy { get; set; } = string.Empty;
        public DateTime PassportIssueDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Status { get; set; } = "Работает";

        public int PositionID { get; set; }
        public Position? Position { get; set; }

        public List<PersonnelEvent> PersonnelEvents { get; set; } = new List<PersonnelEvent>();
    }
}
