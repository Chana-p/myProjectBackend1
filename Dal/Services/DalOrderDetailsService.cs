using Dal.Api;
using Dal.newModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalOrderDetailsService : IDalOrderDetails
    {

          dbcontext dbcontext;
        public DalOrderDetailsService(dbcontext d)
        {
            dbcontext = d;
        }

        public void addDetailsForOrder(List<OrderDetail> list)
        {
            foreach (var item in list)
            {
                dbcontext.OrderDetails.Add(item);
            }
            dbcontext.SaveChanges();
            dbcontext.OrderDetails.ToList();
        }

        public List<OrderDetail> detailsForOrder(int orderId)
        {
            return dbcontext.OrderDetails.ToList().Where(e=>e.OrderId==orderId).ToList();
        }

        public List<OrderDetail> GetAll()
        {
            return dbcontext.OrderDetails.ToList();
        }



        //public void Create(Order o,List< OrderDetail>  od)
        //{
        //    dbcontext.Orders.Add(o);
        //    od.ForEach(item => dbcontext.OrderDetails.Add(item));
        //    dbcontext.SaveChanges();

        //}

        //public List<Order> Get()
        //{
        // var list=   dbcontext.Orders.Include(x=>x.OrderDetails).ToList();
        //    var x = list;
        //    //foreach (var item in list)
        //    //{
        //    //    item.OrderDetails = dbcontext.OrderDetails.ToList().Where(i => i.OrderId == item.OrderId).ToList();  
        //    //}
        //    return list;
        //}


    }
}
