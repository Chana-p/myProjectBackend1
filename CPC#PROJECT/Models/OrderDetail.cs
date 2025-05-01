using System;
using System.Collections.Generic;

namespace CPC_PROJECT.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProdId { get; set; }

    public int Count { get; set; }

    public int? Cost { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ProductsSum Prod { get; set; } = null!;
}
