using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDal
    {
        public IDalCustomers Customers { get; }
        public IDalEmployee Employees { get; }
        public IDalProducts Products { get; }
        public IDalOrders Orders { get; }
        public IDalOrderDetails OrderDetail { get; }
    }
}
