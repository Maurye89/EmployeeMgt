using System;
using System.Collections.Generic;

namespace EmployeeMgt.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }=DateTime.Now.Date;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? HireDate { get; set; } = DateTime.Now.Date;

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }
}
