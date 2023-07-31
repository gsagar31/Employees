using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public long Salary { get; set; }
        public string Designation { get; set; }
    }
}
