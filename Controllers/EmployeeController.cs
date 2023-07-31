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
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await userDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewEmployee = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    EmailAddress = employee.EmailAddress,
                    Salary = employee.Salary,
                    Designation = employee.Designation
                };
                return await Task.Run(() => View("View", viewEmployee));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await userDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.EmailAddress = model.EmailAddress;
                employee.Salary = model.Salary;
                employee.Designation = model.Designation;
             
                await userDbContext.SaveChangesAsync();
                return RedirectToAction("ViewEmployees");
            }
            return RedirectToAction("ViewEmployees");
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployees()
        {
            var employee = await userDbContext.Employees.ToListAsync();
            return View(employee);
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

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await userDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                userDbContext.Employees.Remove(employee);
                await userDbContext.SaveChangesAsync();
                return RedirectToAction("ViewEmployees");
            }
            return RedirectToAction("ViewEmployees");
        }
    }
}
