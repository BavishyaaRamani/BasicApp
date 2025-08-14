using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicApp.Database;
using BasicApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        //create variable for DI
        private readonly EmployeeDbContext EmployeeDbContext;

        //perform DI using constructor
        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            this.EmployeeDbContext = employeeDbContext;
        }

        //Fetch Employee
        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var employee = await EmployeeDbContext.Employees.ToListAsync();

            return Ok(employee);
        }

        //Create Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody]Employee emp)
        {
            emp.Id = new Guid();

            await EmployeeDbContext.Employees.AddAsync(emp);

            await EmployeeDbContext.SaveChangesAsync();

            return Ok(emp);
            
        }

        //Update Employee
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody]Employee emp)
        {
            var employee = await EmployeeDbContext.Employees.FirstOrDefaultAsync(a => a.Id == id);

            if(employee != null)
            {
                employee.Name = emp.Name;
                employee.MobileNo = emp.MobileNo;
                employee.EmailID = emp.EmailID;

                await EmployeeDbContext.SaveChangesAsync();

                return Ok(emp);
            }
            else
            {
                return NotFound("Employee not found");
            }

        }

        //Delete Employee
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await EmployeeDbContext.Employees.FirstOrDefaultAsync(a => a.Id == id);

            if(employee != null)
            {
                EmployeeDbContext.Employees.Remove(employee);
                await EmployeeDbContext.SaveChangesAsync();

                return Ok(employee);

            }
            else
            {
                return NotFound("Employee not found");
            }
        }

    }
}
