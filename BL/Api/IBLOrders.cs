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



        int Add(int custId,int? empId);
        List<BLOrder> addDetails(List<BLOrderDetail> list,int orderId);
        void UpdateSending(int orderId, int empId);

        List<BLOrder> Get();
        void DeleteAll();
        List<BLOrder> GetNews();
        List<BLOrder> GetForCustomer(int custId);
        List<BLOrder> GetForEmployee(int empId);

        List<BLOrder> GetCompletedForEmployee(int empId);

        void AssignOrders(int empId, List<BLOrder> ordList);



    }
}
