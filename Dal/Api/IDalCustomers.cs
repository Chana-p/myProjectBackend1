using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
   public interface IDalCustomers
    {
        List<Customer> Get();
        void Create(Customer c);
        Customer Update(Customer c);
    }
}
