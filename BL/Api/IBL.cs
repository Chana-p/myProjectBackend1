using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
  public  interface IBL
    {
        public IBLCustomer Customers { get; }
        public IBLEmployee Employees { get; }
        public IBLProducts Products { get; }
        public IBLOrders Orders { get; }
        public IBLOrderDetails OrderDetails { get; }
    }
}
