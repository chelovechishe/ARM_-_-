using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM_Отдела_кадров.Models
{
    public class PersonnelEvent
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public int PositionID { get; set; }
        public string EventType { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string? Reason { get; set; }
        public string? OrderNumber { get; set; }

        public Employee? Employee { get; set; }
        public Position? Position { get; set; }
    }
}
