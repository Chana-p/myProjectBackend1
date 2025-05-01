using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BLOrderDetailsService : IBLOrderDetails
    {
        IDal dal;
        public BLOrderDetailsService(IDal dal)
        {
            this.dal = dal;
        }

        public List<BLOrderDetail> GetAll()
        {
            List<BLOrderDetail> list = new();
            foreach (var item in dal.OrderDetail.GetAll())
            {
                BLOrderDetail newbl = new BLOrderDetail(item);
                //var a = dal.Products.Get().Find(p => p.ProdId == item.ProdId);
                //newbl.ProdName = a.Pname;
                //newbl.ProdPic = a.Ppicture;

                list.Add(newbl);

            }
            return list;
        }

        //public void Add(BLOrder bLOrder)
        //{
        //    Order o = new()
        //    {
        //        OrderId = bLOrder.OrderId,
        //       //OrdersDetais = bLOrder.OrdersDetais,
        //        OrderDate = bLOrder.OrderDate,
        //        CustId = bLOrder.CustId,
        //        EmpId = bLOrder.EmpId,
        //        PaymentType = bLOrder.PaymentType,
        //        Sent = bLOrder.Sent
        //    };
        //   // dal.Orders.Create(o, bLOrder.OrderDetails);
        //}

        //public List<BLOrder> Get()
        //{
        // List<Order>  dallist=dal.Orders.Get();
        // List<BLOrder>  bllist=new();

        //    foreach (var item in dallist)
        //    {
        //        bllist.Add(new BLOrder(item));
        //    }
        //    return bllist;
        //}

        public List<BLOrderDetail> GetForOrderId(int orderId)
        {
            List<BLOrderDetail> list = new();
            foreach (var item in dal.OrderDetail.detailsForOrder(orderId))
            {
                BLOrderDetail newbl =new BLOrderDetail(item);
                var a = dal.Products.Get().Find(p => p.ProdId == item.ProdId);
                newbl.ProdName = a.Pname;
                newbl.ProdPic = a.Ppicture;

                list.Add(newbl);

            }
            return list ;
        }

        public void setOrderId(List<BLOrderDetail> list,int orderId)
        {
            foreach (var item in list)
            {
                item.OrderId = orderId;
            }
        }

        
    }
}
