using Dal.Api;
using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalCustomerService : IDalCustomers
    {
        dbcontext dbcontext;
        public DalCustomerService(dbcontext d)
        {
            dbcontext = d;
        }

        public void Create(Customer c)
        {
            if (c.CustId != 0 && c.CustName != "string" && c.CustEmail != "string")
            {

                dbcontext.Customers.Add(c);
                dbcontext.SaveChanges();
            }
            //else return 
        }

        public List<Customer> Get()
        {
            return dbcontext.Customers.ToList();
        }

        public Customer Update(Customer newC)
        {
            var x = dbcontext.Customers.ToList().Find(i => i.CustId == newC.CustId);
            dbcontext.Customers.Remove(x);
            dbcontext.Customers.Add(newC);
            dbcontext.SaveChanges();
            return newC;

        }
    }
}
