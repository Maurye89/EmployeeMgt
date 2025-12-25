using System;
using System.Collections.Generic;

namespace EmployeeMgt.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public string? Location { get; set; }
}
