using BL.Models;
using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
   public interface IBLOrders
    {
        //void Add(BLOrder bLOrder);
        int Add(int custId);
        List<BLOrder> addDetails(List<BLOrderDetail> list,int orderId);
        void UpdateSending(int orderId);
        List<BLOrder> Get();
        void DeleteAll();
        List<BLOrder> GetNews();
        List<BLOrder> GetForCustomer(int custId);
        List<BLOrder> GetForEmployee(int empId);

    //    void Create(Order o, List<OrderDetail> od);



    }
}
