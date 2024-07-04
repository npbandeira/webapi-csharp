using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.models;

namespace webapi.infra
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionContext _context = new();
        public void Add(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
        }

        public List<Employee> Get(int pageNumber, int pageQuantity)
        {
            return _context.Employees.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToList();
        }

        public Employee? GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }
    }
}