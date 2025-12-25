using System;
using System.Collections.Generic;

namespace EmployeeMgt.Models;

public partial class Designation
{
    public int DesignationId { get; set; }

    public string? Title { get; set; }

    public string? Level { get; set; }
}
