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
            try
            {
                _context = new AppDbContext();
                _context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации базы данных: {ex.Message}\n\n" +
                    "Проверьте, что файл базы данных доступен для записи и нет проблем с подключением.",
                    "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public List<Employee> GetAllEmployees()
        {
            try
            {
                return _context.Employees
                    .Include(e => e.Position)
                    .OrderBy(e => e.LastName)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка сотрудников: {ex.Message}",
                    "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Employee>();
            }
        }

        public List<Employee> GetActiveEmployees()
        {
            try
            {
                return _context.Employees
                    .Include(e => e.Position)
                    .Where(e => e.Status == "Работает")
                    .OrderBy(e => e.LastName)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка работающих сотрудников: {ex.Message}",
                    "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Employee>();
            }
        }

        public List<Position> GetAllPositions()
        {
            try
            {
                return _context.Positions.OrderBy(p => p.PositionName).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка должностей: {ex.Message}",
                    "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Position>();
            }
        }

        public User? ValidateUser(string login, string password)
        {
            try
            {
                return _context.Users
                    .FirstOrDefault(u => u.Login == login && u.Password == password);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке учётных данных: {ex.Message}\n\n" +
                    "Проверьте подключение к базе данных.",
                    "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Ошибка базы данных при добавлении сотрудника: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непредвиденная ошибка при добавлении сотрудника: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                var existing = _context.Employees.Find(employee.EmployeeID);
                if (existing == null)
                {
                    MessageBox.Show("Сотрудник не найден в базе данных", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

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
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Ошибка базы данных при обновлении сотрудника: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непредвиденная ошибка при обновлении сотрудника: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                var employee = _context.Employees.Find(employeeId);
                if (employee == null)
                {
                    MessageBox.Show("Сотрудник не найден в базе данных", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Невозможно удалить сотрудника, так как с ним связаны другие данные.\n\n" +
                    $"Детали ошибки: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении сотрудника: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool TerminateEmployee(int employeeId, DateTime terminationDate, string reason = "")
        {
            try
            {
                var employee = _context.Employees.Find(employeeId);
                if (employee == null)
                {
                    MessageBox.Show("Сотрудник не найден в базе данных", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (employee.Status == "Уволен")
                {
                    MessageBox.Show("Сотрудник уже уволен", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                employee.Status = "Уволен";
                employee.TerminationDate = terminationDate;

                var personnelEvent = new PersonnelEvent
                {
                    EmployeeID = employeeId,
                    PositionID = employee.PositionID,
                    EventType = "Увольнение",
                    EventDate = terminationDate,
                    Reason = string.IsNullOrEmpty(reason) ? "Увольнение по инициативе работодателя" : reason
                };
                _context.PersonnelEvents.Add(personnelEvent);

                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Ошибка базы данных при увольнении сотрудника: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непредвиденная ошибка при увольнении сотрудника: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool TransferEmployee(int employeeId, int newPositionId, DateTime transferDate)
        {
            try
            {
                var employee = _context.Employees.Find(employeeId);
                if (employee == null)
                {
                    MessageBox.Show("Сотрудник не найден в базе данных", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (employee.Status == "Уволен")
                {
                    MessageBox.Show("Нельзя перевести уволенного сотрудника", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var oldPositionId = employee.PositionID;

                if (oldPositionId == newPositionId)
                {
                    MessageBox.Show("Сотрудник уже занимает эту должность", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var newPosition = _context.Positions.Find(newPositionId);
                if (newPosition == null)
                {
                    MessageBox.Show("Выбранная должность не существует", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                employee.PositionID = newPositionId;

                var personnelEvent = new PersonnelEvent
                {
                    EmployeeID = employeeId,
                    PositionID = oldPositionId,
                    EventType = "Перевод",
                    EventDate = transferDate,
                    Reason = $"Переведён на должность {newPosition.PositionName}"
                };
                _context.PersonnelEvents.Add(personnelEvent);

                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Ошибка базы данных при переводе сотрудника: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непредвиденная ошибка при переводе сотрудника: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public List<Employee> SearchEmployees(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return GetAllEmployees();
                }

                return _context.Employees
                    .Include(e => e.Position)
                    .Where(e => e.LastName.Contains(searchText) ||
                                e.FirstName.Contains(searchText) ||
                                (e.MiddleName != null && e.MiddleName.Contains(searchText)))
                    .OrderBy(e => e.LastName)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске сотрудников: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Employee>();
            }
        }

        public Employee? GetEmployeeById(int employeeId)
        {
            try
            {
                return _context.Employees
                    .Include(e => e.Position)
                    .FirstOrDefault(e => e.EmployeeID == employeeId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных сотрудника: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public string GetPositionNameById(int positionId)
        {
            try
            {
                var position = _context.Positions.Find(positionId);
                return position?.PositionName ?? "Не указана";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении названия должности: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Ошибка загрузки";
            }
        }

        public bool AddPosition(string positionName, decimal salary)
        {
            try
            {
                var existing = _context.Positions.FirstOrDefault(p => p.PositionName == positionName);
                if (existing != null)
                {
                    MessageBox.Show("Должность с таким названием уже существует", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var position = new Position
                {
                    PositionName = positionName,
                    Salary = salary
                };

                _context.Positions.Add(position);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении должности: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DatabaseConnected()
        {
            try
            {
                return _context.Database.CanConnect();
            }
            catch
            {
                return false;
            }
        }

        public int GetActiveEmployeesCount()
        {
            try
            {
                return _context.Employees.Count(e => e.Status == "Работает");
            }
            catch
            {
                return 0;
            }
        }

        public int GetAllEmployeesCount()
        {
            try
            {
                return _context.Employees.Count();
            }
            catch
            {
                return 0;
            }
        }
        // Получить всех пользователей
        public List<User> GetAllUsers()
        {
            try
            {
                return _context.Users.OrderBy(u => u.Login).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<User>();
            }
        }

        // Получить пользователя по ID
        public User? GetUserById(int userId)
        {
            try
            {
                return _context.Users.Find(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователя: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Проверка существования логина
        public bool LoginExists(string login, int? excludeUserId = null)
        {
            try
            {
                var query = _context.Users.Where(u => u.Login == login);
                if (excludeUserId.HasValue)
                {
                    query = query.Where(u => u.UserID != excludeUserId.Value);
                }
                return query.Any();
            }
            catch
            {
                return false;
            }
        }

        // Добавление нового пользователя
        public bool AddUser(string login, string password, string role)
        {
            try
            {
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(login))
                {
                    MessageBox.Show("Логин не может быть пустым", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Пароль не может быть пустым", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Проверка на существование логина
                if (LoginExists(login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var user = new User
                {
                    Login = login,
                    Password = password,
                    Role = role
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                MessageBox.Show($"Пользователь {login} успешно создан", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании пользователя: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Обновление пользователя
        public bool UpdateUser(int userId, string login, string password, string role)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Проверка на существование логина (исключая текущего пользователя)
                if (LoginExists(login, userId))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                user.Login = login;
                user.Password = password;
                user.Role = role;

                _context.SaveChanges();

                MessageBox.Show($"Пользователь {login} успешно обновлён", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении пользователя: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Удаление пользователя
        public bool DeleteUser(int userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Запрет на удаление последнего администратора
                if (user.Role == "Administrator" && _context.Users.Count(u => u.Role == "Administrator") <= 1)
                {
                    MessageBox.Show("Нельзя удалить последнего администратора системы",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                _context.Users.Remove(user);
                _context.SaveChanges();

                MessageBox.Show($"Пользователь {user.Login} удалён", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Смена пароля текущим пользователем
        public bool ChangePassword(string login, string oldPassword, string newPassword)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Login == login);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (user.Password != oldPassword)
                {
                    MessageBox.Show("Старый пароль введён неверно", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show("Новый пароль не может быть пустым", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                user.Password = newPassword;
                _context.SaveChanges();

                MessageBox.Show("Пароль успешно изменён", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при смене пароля: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
