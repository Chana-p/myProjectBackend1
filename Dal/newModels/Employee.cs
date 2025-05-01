using System;
using System.Collections.Generic;

namespace Dal.newModels;

public partial class Employee
{
    public int EmpId { get; set; }

    public int EmpNum { get; set; }

    public string Ename { get; set; } = null!;

    public string Egmail { get; set; } = null!;

    public string Ephone { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
