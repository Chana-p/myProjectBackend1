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
<<<<<<< HEAD
        //void Add(BLOrder bLOrder);
        int Add(int custId);
        List<BLOrder> addDetails(List<BLOrderDetail> list,int orderId);
        void UpdateSending(int orderId);
=======

        int Add(int custId,int? empId);
        List<BLOrder> addDetails(List<BLOrderDetail> list,int orderId);
        void UpdateSending(int orderId, int empId);

>>>>>>> origin/main
        List<BLOrder> Get();
        void DeleteAll();
        List<BLOrder> GetNews();
        List<BLOrder> GetForCustomer(int custId);
        List<BLOrder> GetForEmployee(int empId);

<<<<<<< HEAD
    //    void Create(Order o, List<OrderDetail> od);

=======
        List<BLOrder> GetCompletedForEmployee(int empId);

        void AssignOrders(int empId, List<BLOrder> ordList);
>>>>>>> origin/main


    }
}
