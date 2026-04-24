using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM_Отдела_кадров.Models
{
    public class Position
    {
        public int PositionID { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<PersonnelEvent> PersonnelEvents { get; set; } = new List<PersonnelEvent>();
    }
}
