using System;
using System.Collections.Generic;

namespace CPC_PROJECT.Models;

public partial class Customer
{
    public int CustId { get; set; }

    public int CustNum { get; set; }

    public string CustName { get; set; } = null!;

    public string? CustAddress { get; set; }

    public string CustEmail { get; set; } = null!;

    public string? CustPhone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
