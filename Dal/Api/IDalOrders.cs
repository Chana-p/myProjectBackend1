using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
   public interface IDalOrders
    {

        List<Order> Get();
        List<Order> GetForCustomer(int custId);
        List<Order> GetForEmployee(int empId);

        int Create(Order o);
        void Delete();
        void UpdateSending(int orderId,int empId);
        void AssignOrdersToEmp(int empId,int ordId);


    }
}
