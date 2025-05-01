using BL.Api;
using BL.Services;
using Dal;
using Dal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLManager : IBL
    {
        public string Path { get; init; }
        public IBLCustomer Customers { get; set; }
        public IBLProducts Products { get; set; }
        public IBLOrders Orders { get; set; }
        public IBLOrderDetails OrderDetails { get; set; }

        public IBLEmployee Employees { get; set; }

        public BLManager()// string path)
        {
            IDal dal = new DalManager();
            Customers = new BLCustomerService(dal);
            Products = new BLProductService(dal);
            Orders = new BLOrdersService(dal);
            OrderDetails = new BLOrderDetailsService(dal);
            Employees = new BLEmployeeService(dal);
        }
    }
}
