using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.ViewModel
{
    public class EmployeeViewModel
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
        public IFormFile? Photo { get; set; }
    }
}