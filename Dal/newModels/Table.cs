using System;
using System.Collections.Generic;

namespace Dal.newModels;

public partial class Table
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProdId { get; set; }

    public int Count { get; set; }

    public int? Cost { get; set; }

    public virtual ProductsSum Prod { get; set; } = null!;
}
