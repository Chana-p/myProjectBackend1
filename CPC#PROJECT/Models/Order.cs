using System;
using System.Collections.Generic;

namespace CPC_PROJECT.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string OrdersDetails { get; set; } = null!;

    public DateOnly? OrderDate { get; set; }

    public int CustId { get; set; }

    public int EmpId { get; set; }

    public string? PaymentType { get; set; }

    public bool? Sent { get; set; }

    public string Pcc { get; set; } = null!;

    public virtual Customer Cust { get; set; } = null!;

    public virtual Employee Emp { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}
