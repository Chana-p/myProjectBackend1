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
        List<Order> GetNews();
        List<Order> GetForCustomer(int custId);
        List<Order> GetForEmployee(int empId);

        //void Create(Order o, List<OrderDetail> od);
        int Create(Order o);
        void Delete();
        void UpdateSending(int orderId);


    }
}
