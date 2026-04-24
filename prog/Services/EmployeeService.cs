using ARM_Отдела_кадров.Data;
using ARM_Отдела_кадров.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM_Отдела_кадров.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees
                .Include(e => e.Position)
                .OrderBy(e => e.LastName)
                .ToList();
        }

        public List<Employee> GetActiveEmployees()
        {
            return _context.Employees
                .Include(e => e.Position)
                .Where(e => e.Status == "Работает")
                .OrderBy(e => e.LastName)
                .ToList();
        }

        public List<Position> GetAllPositions()
        {
            return _context.Positions.OrderBy(p => p.PositionName).ToList();
        }

        public User? ValidateUser(string login, string password)
        {
            return _context.Users
                .FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                var existing = _context.Employees.Find(employee.EmployeeID);
                if (existing == null) return false;

                existing.LastName = employee.LastName;
                existing.FirstName = employee.FirstName;
                existing.MiddleName = employee.MiddleName;
                existing.BirthDate = employee.BirthDate;
                existing.PassportSeries = employee.PassportSeries;
                existing.PassportNumber = employee.PassportNumber;
                existing.PassportIssuedBy = employee.PassportIssuedBy;
                existing.PassportIssueDate = employee.PassportIssueDate;
                existing.Address = employee.Address;
                existing.Phone = employee.Phone;
                existing.PositionID = employee.PositionID;

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                var employee = _context.Employees.Find(employeeId);
                if (employee == null) return false;

                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TerminateEmployee(int employeeId, DateTime terminationDate)
        {
            try
            {
                var employee = _context.Employees.Find(employeeId);
                if (employee == null) return false;

                employee.Status = "Уволен";
                employee.TerminationDate = terminationDate;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TransferEmployee(int employeeId, int newPositionId, DateTime transferDate)
        {
            try
            {
                var employee = _context.Employees.Find(employeeId);
                if (employee == null) return false;

                var oldPositionId = employee.PositionID;
                employee.PositionID = newPositionId;

                var personnelEvent = new PersonnelEvent
                {
                    EmployeeID = employeeId,
                    PositionID = oldPositionId,
                    EventType = "Перевод",
                    EventDate = transferDate,
                    Reason = $"Переведён с должности ID {oldPositionId} на должность ID {newPositionId}"
                };
                _context.PersonnelEvents.Add(personnelEvent);

                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Employee> SearchEmployees(string searchText)
        {
            return _context.Employees
                .Include(e => e.Position)
                .Where(e => e.LastName.Contains(searchText) || e.FirstName.Contains(searchText))
                .OrderBy(e => e.LastName)
                .ToList();
        }

        public Employee? GetEmployeeById(int employeeId)
        {
            return _context.Employees
                .Include(e => e.Position)
                .FirstOrDefault(e => e.EmployeeID == employeeId);
        }

        public string GetPositionNameById(int positionId)
        {
            var position = _context.Positions.Find(positionId);
            return position?.PositionName ?? string.Empty;
        }
    }
}
