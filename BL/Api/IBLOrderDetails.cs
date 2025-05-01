using BL.Models;
using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
   public interface IBLOrderDetails
    {

        List<BLOrderDetail> GetForOrderId(int orderId);
        List<BLOrderDetail> GetAll();


    }
}
