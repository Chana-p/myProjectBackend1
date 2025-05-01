using System;
using System.Collections.Generic;

namespace CPC_PROJECT.Models;

public partial class ProductsSum
{
    public int ProdId { get; set; }

    public string Pname { get; set; } = null!;

    public int Psum { get; set; }

    public string Pimporter { get; set; } = null!;

    public string? Pcompany { get; set; }

    public string? Pdescription { get; set; }

    public string? Ppicture { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}
