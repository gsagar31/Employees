using Employees.Data;
using Employees.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly UserDbContext userDbContext;

        public EmployeeController(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployees()
        {
            var employees = await userDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeViewModelRequest)
        {
            var newEmployee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeViewModelRequest.Name,
                EmailAddress = addEmployeeViewModelRequest.EmailAddress,
                Salary = addEmployeeViewModelRequest.Salary,
                Designation = addEmployeeViewModelRequest.Designation
            };
            await userDbContext.Employees.AddAsync(newEmployee);
            await userDbContext.SaveChangesAsync();
            return RedirectToAction("ViewEmployees");
        }
    }
}
