using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.models
{
    public interface IEmployeeRepository
    {

        void Add(Employee employee);
        List<Employee> Get();
    }
}