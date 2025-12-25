using System;
using System.Collections.Generic;

namespace EmployeeMgt.Models;

public partial class Salary
{
    public int SalaryId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? BasicSalary { get; set; }

    public decimal? Hra { get; set; }

    public decimal? Allowances { get; set; }

    public decimal? Deductions { get; set; }

    public DateOnly? EffectiveFrom { get; set; }
}
