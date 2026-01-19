using EmployeeMgt.Models;
using EmployeeMgt.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeMgt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeMgtContext _context;
        private readonly IEmployee _Employee;
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _cache;


        public EmployeeController(EmployeeMgtContext employeeMgtContext, IEmployee employee, IConnectionMultiplexer redis)
        {
            _context = employeeMgtContext;
            _Employee = employee;
            _cache = redis.GetDatabase();
        }

        //// GET: api/employee/GetEmployeeDetails
        //[HttpGet]
        //[Route("EmployeeDetails/{iEmpID}")]
        //public async Task<ActionResult<List<Employee>>> GetEmployeeDetails(int iEmpID)
        //{
        //    List<Employee> employees=new List<Employee>();
        //    try
        //    {
        //        if (_context!=null)
        //        {
        //            employees = await _context.Employees.Where(x => x.EmployeeId == iEmpID).ToListAsync();
        //            if (employees == null || employees.Count == 0)
        //            {
        //                return NotFound(new { success = false, message = "Employee not found" });
        //            }
        //        }
        //        return Ok(employees);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Return generic server error with minimal details
        //        return  employees = new List<Employee>();

        //    }
            
        //}

        //// POST: api/employee/SaveEmployeeDetails
        //[HttpPost]
        //[Route("SaveEmployeeDetails")]
        //public async Task<ActionResult<Employee>> SaveEmployeeDetails([FromBody] Employee employee)
        //{
        //    try
        //    {
        //        if (_context != null)
        //        {
        //            // Add the new employee
        //            await _context.Employees.AddAsync(employee);
        //            await _context.SaveChangesAsync();

        //            // Return 201 Created with the saved employee
        //            return CreatedAtAction(nameof(GetEmployeeDetails),
        //                new { iEmpID = employee.EmployeeId },
        //                employee);
        //        }

        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new { success = false, message = "Database context not available" });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Return generic server error with minimal details
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new { success = false, message = "Failed to save employee", error = ex.Message });
        //    }
        //}

        //// PUT: api/employee/UpdateEmployeeDetails/{iEmpID}
        //[HttpPut]
        //[Route("UpdateEmployeeDetails/{iEmpID}")]
        //public async Task<ActionResult<Employee>> UpdateEmployeeDetails(int iEmpID, [FromBody] Employee updatedEmployee)
        //{
        //    try
        //    {
        //        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == iEmpID);

        //        if (employee == null)
        //        {
        //            return NotFound(new { success = false, message = "Employee not found" });
        //        }

        //        // Update fields
        //        employee.FirstName = updatedEmployee.FirstName;
        //        employee.LastName = updatedEmployee.LastName;
        //        employee.DateOfBirth = updatedEmployee.DateOfBirth;

        //        _context.Employees.Update(employee);
        //        await _context.SaveChangesAsync();

        //        return Ok(new { success = true, message = "Employee updated successfully", data = employee });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new { success = false, message = "Failed to update employee", error = ex.Message });
        //    }
        //}

        //// DELETE: api/employee/DeleteEmployee/{iEmpID}
        //[HttpDelete]
        //[Route("DeleteEmployee/{iEmpID}")]
        //public async Task<ActionResult> DeleteEmployee(int iEmpID)
        //{
        //    try
        //    {
        //        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == iEmpID);

        //        if (employee == null)
        //        {
        //            return NotFound(new { success = false, message = "Employee not found" });
        //        }

        //        _context.Employees.Remove(employee);
        //        await _context.SaveChangesAsync();

        //        return Ok(new { success = true, message = "Employee deleted successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new { success = false, message = "Failed to delete employee", error = ex.Message });
        //    }
        //}


        //---------------- Get data using service layer with stored procedure----------------//
        // GET: api/employee/GetEmpDetails
        //[HttpGet]
        //[Route("EmpDetails/{iEmpID}")]
        //public async Task<IActionResult> GetEmpDetails(int iEmpID)

        //{
        //    string cacheKey = $"EmpDetails:{iEmpID}";
        //    // Try to get from cache
        //    var cachedData = await _cache.StringGetAsync(cacheKey);
        //    if (cachedData.HasValue)
        //    {
        //        var employees = JsonSerializer.Deserialize<List<Employee>>(cachedData);
        //        return Ok(employees);
        //    }

        //    // Call repository (returns ActionResult<List<Employee>>)
        //    var repoResult = await _Employee.GetEmployeeDetails(iEmpID);

        //    // If repository returned a specific IActionResult (e.g. NotFound, Problem), forward it
        //    if (repoResult.Result != null)
        //    {
        //        return repoResult.Result;
        //    }

        //    // Otherwise repo returned a Value
        //    var employees = repoResult.Value ?? new List<Employee>();

        //    if (employees.Count == 0)
        //    {
        //        return NotFound(new { success = false, message = "Employee not found" });
        //    }

        //    return Ok(employees);

        //}



        [HttpGet]
        [Route("EmpDetails/{iEmpID}")]
        public async Task<IActionResult> GetEmpDetails(int iEmpID)
        {
            try
            {
                string cacheKey = $"EmpDetails:{iEmpID}";

                // Try to get from cache
                var cachedData = await _cache.StringGetAsync(cacheKey);  // API reduce
                if (cachedData.HasValue)
                {
                    var employees = JsonSerializer.Deserialize<List<Employee>>(cachedData.ToString());
                    return Ok(employees);
                }

                // Call repository
                var repoResult = await _Employee.GetEmployeeDetails(iEmpID);

                if (repoResult.Result != null)
                {
                    return repoResult.Result;
                }

                var employeesFromDb = repoResult.Value ?? new List<Employee>();

                if (employeesFromDb.Count == 0)
                {
                    return NotFound(new { success = false, message = "Employee not found" });
                }

                // Save to cache with expiration (e.g., 1 minutes)
                await _cache.StringSetAsync(cacheKey,JsonSerializer.Serialize(employeesFromDb),TimeSpan.FromMinutes(1)
                );

                return Ok(employeesFromDb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
