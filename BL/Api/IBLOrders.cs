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
<<<<<<< HEAD
        int Add(int custId,int? empId);
        List<BLOrder> addDetails(List<BLOrderDetail> list,int orderId);
        void UpdateSending(int orderId, int empId);
=======
        int Add(int custId);
        List<BLOrder> addDetails(List<BLOrderDetail> list,int orderId);
        void UpdateSending(int orderId);
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
        List<BLOrder> Get();
        void DeleteAll();
        List<BLOrder> GetNews();
        List<BLOrder> GetForCustomer(int custId);
        List<BLOrder> GetForEmployee(int empId);
<<<<<<< HEAD
        List<BLOrder> GetCompletedForEmployee(int empId);

        void AssignOrders(int empId, List<BLOrder> ordList);
=======

    //    void Create(Order o, List<OrderDetail> od);
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550



    }
}
