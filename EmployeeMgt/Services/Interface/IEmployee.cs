using EmployeeMgt.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMgt.Services.Interface
{
    public interface IEmployee
    {
        public Task<ActionResult<List<Employee>>> GetEmployeeDetails(int iEmpID);
    }
}
