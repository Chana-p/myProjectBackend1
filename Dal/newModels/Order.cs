using System;
using System.Collections.Generic;

namespace Dal.newModels;

public partial class Order
{
    public int OrderId { get; set; }

    public string? OrderDate { get; set; }

    public int CustId { get; set; }

    public int EmpId { get; set; }

    public string? PaymentType { get; set; }

    public bool? Sent { get; set; }

    public string? Pcc { get; set; }

    public virtual Customer Cust { get; set; } = null!;

    public virtual Employee Emp { get; set; } = null!;
}
