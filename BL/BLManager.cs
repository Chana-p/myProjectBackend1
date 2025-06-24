using BL.Api;
using BL.Services;
using Dal;
using Dal.Api;
using Dal.newModels;
using Dal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLManager : IBL
    {
         public IBLCustomer Customers { get; set; }
        public IBLProducts Products { get; set; }
        public IBLOrders Orders { get; set; }
        public IBLOrderDetails OrderDetails { get; set; }

        public IBLEmployee Employees { get; set; }

        private readonly IDal _dal;

        public BLManager(IDal dal)
        {
            _dal = dal;
            Customers = new BLCustomerService(_dal);
            Employees = new BLEmployeeService(_dal);
            Products = new BLProductService(_dal);
            Orders = new BLOrdersService(_dal);
            OrderDetails = new BLOrderDetailsService(_dal);
        }
    }
}
