using Dapper;
using EmployeeMgt.DapperModel;
using EmployeeMgt.Models;
using EmployeeMgt.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeMgt.Services.Repository
{
    public class EmployeeRepo : IEmployee
    {
        private readonly DapperDBContext _context;
        public EmployeeRepo(DapperDBContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ActionResult<List<Employee>>> GetEmployeeDetails(int iEmpID)
        {
            try
            {
                await using var connection = await _context.CreateOpenConnectionAsync();
                // Open connection explicitly (optional, Dapper will open if closed)
                if (connection.State == ConnectionState.Closed)
                    await ((SqlConnection)connection).OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@EmpID", iEmpID, DbType.Int32, ParameterDirection.Input);

                // Assuming your stored procedure name is "sp_GetEmployeeDetails"
                var employees = await connection.QueryAsync<Employee>(
                    "sp_GetEmployeeDetails",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return new ActionResult<List<Employee>>(employees.ToList());
                
            }
            catch (Exception ex)
            {
                // Log exception if needed
                throw new Exception("Error fetching employee details", ex);
            }
        }

    }
}
