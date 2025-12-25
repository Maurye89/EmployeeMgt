using System;
using System.Collections.Generic;

namespace EmployeeMgt.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? AttendanceDate { get; set; }

    public string? Status { get; set; }
}
