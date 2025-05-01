using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
  public  class BLOrderDetail
    {
           public int ProdId { get; set; }
           public string ProdName { get; set; }
           public string ProdPic { get; set; }
        public int OrderId { get; set; }

        public int Count { get; set; }

        public int? Cost { get; set; }

        public BLOrderDetail(OrderDetail od)
        {
            ProdId = od.ProdId;
           OrderId = od.OrderId;
            Count = od.Count;
           // Cost = od.Cost;
        }

        public BLOrderDetail(int ordId, int count, int prodId)
        {
            ProdId = prodId;
            Count = count;
            OrderId = ordId;
        }
        public BLOrderDetail(int prodId, int Count)
        {
            ProdId = prodId;
            this.Count = Count;
        }
        public BLOrderDetail()
        {
           
        }
    }
}
