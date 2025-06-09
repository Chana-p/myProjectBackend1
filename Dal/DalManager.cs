using Dal.Api;
using Dal.newModels;
using Dal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DalManager : IDal
    {
      //  dbcontext d = new dbcontext();
        public IDalCustomers Customers{ get; }
        public IDalEmployee Employees{ get; }
        public IDalProducts Products { get; }

        public IDalOrders Orders { get; }
        public IDalOrderDetails OrderDetail { get; }

        //public DalManager()
        //{
        //    Customers = new DalCustomerService(d);
        //    Products = new DalProductsService(d);
        //   Orders = new DalOrdersService(d);
        //   OrderDetail= new DalOrderDetailsService(d);
        //    Employees = new DalEmployeeService(d);
        //}
        private readonly dbcontext _context;

        public DalManager(dbcontext context)
        {
            _context = context;
            Customers = new DalCustomerService(_context);
            Employees = new DalEmployeeService(_context); // אם קיים
            Products = new DalProductsService(_context);
            Orders = new DalOrdersService(_context);
            OrderDetail = new DalOrderDetailsService(_context);
        }
    }
}
