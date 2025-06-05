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
    public class BLOrdersService : IBLOrders
    {
        IDal Dal;

        public BLOrdersService(IDal dal)
        {
            this.Dal = dal;
        }

<<<<<<< HEAD
        public int Add(int custId, int? empId)
        {
            Order o = new()
=======
        public int Add(int custId)
        {
            DateTime dt=DateTime.Now;
            Console.WriteLine("DateTime in Normal format: ");
            string sqlFormattedDate=dt.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine("DateTime in SQL format: ");
            
                        Order o = new()
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
            {
                
                OrderDate= DateOnly.FromDateTime(DateTime.Today).ToShortDateString(),
                CustId = custId,
<<<<<<< HEAD
                EmpId = empId==0?empId:null,
=======
                EmpId = Dal.Employees.AvailableEmployee().EmpId,
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
                //PaymentType = bLOrder.PaymentType,
                Sent = false
            };
           return  Dal.Orders.Create(o);
        }

        public List<BLOrder> addDetails(List<BLOrderDetail> list,int orderId)
        {
            List<OrderDetail> dalList = new();
            foreach (var item in list)
            {
                OrderDetail od = new()
                {
                    OrderId=orderId,
                    ProdId=item.ProdId,
                    Count=item.Count
                };
<<<<<<< HEAD
                Dal.Products.UpdateSum(item.ProdId, item.Count);
=======
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
                dalList.Add(od);
            }
            Dal.OrderDetail.addDetailsForOrder(dalList);
            return Get();
        }

        public void deleteAll()
        {
            Dal.Orders.Delete();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public List<BLOrder> Get()
        {
<<<<<<< HEAD
            List<Order> dallist = Dal.Orders.Get();
            List<BLOrder> bllist = new();

            foreach (var item in dallist)
            {
                string email = null;
                string name = null;

                if (item.EmpId.HasValue) // Check if EmpId has a value
                {
                    email = Dal.Employees.getByID(item.EmpId.Value).Egmail; // Use .Value to access the int value
                    name = Dal.Employees.getByID(item.EmpId.Value).Ename;
                }

                bllist.Add(new BLOrder(item, email, name));
=======
         List<Order>  dallist=Dal.Orders.Get();
         List<BLOrder>  bllist=new();

            foreach (var item in dallist)
            {
               string email= Dal.Employees.getByID(item.EmpId).Egmail;
               string name= Dal.Employees.getByID(item.EmpId).Ename;
                bllist.Add(new BLOrder(item,email,name));
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
            }
            return bllist;
        }

        public List<BLOrder> GetForCustomer(int custId)
        {
            List<Order> dallist = Dal.Orders.GetForCustomer(custId);
            List<BLOrder> bllist = new();
<<<<<<< HEAD

            foreach (var item in dallist)
            {
                string email = null;
                string name = null;

                if (item.EmpId.HasValue) // Check if EmpId has a value
                {
                    email = Dal.Employees.getByID(item.EmpId.Value).Egmail; // Use .Value to access the int value
                    name = Dal.Employees.getByID(item.EmpId.Value).Ename;
                }

                bllist.Add(new BLOrder(item, email, name));
=======
            
            foreach (var item in dallist)
            {
                string email = Dal.Employees.getByID(item.EmpId).Egmail;
                string name = Dal.Employees.getByID(item.EmpId).Ename;
                bllist.Add(new BLOrder(item,email,name));
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
            }
            return bllist;
        }

        public List<BLOrder> GetForEmployee(int empId)
        {
            List<Order> dallist = Dal.Orders.GetForEmployee(empId);
            List<BLOrder> bllist = new();

            foreach (var item in dallist)
            {
<<<<<<< HEAD
                if (!(bool)item.Sent)
                {
                string email = Dal.Customers.Get().ToList().Find(cust=>cust.CustId==item.CustId).CustEmail;
                string name = Dal.Customers.Get().ToList().Find(cust => cust.CustId == item.CustId).CustName;
                bllist.Add(new BLOrder(item, email, name));
                }
                
            }
            return bllist;
        }
        public List<BLOrder> GetCompletedForEmployee(int empId)
        {
            List<Order> dallist = Dal.Orders.GetForEmployee(empId);
            List<BLOrder> bllist = new();

            foreach (var item in dallist)
            {
                if ((bool)item.Sent)
                {
                string email = Dal.Customers.Get().ToList().Find(cust => cust.CustId == item.CustId).CustEmail;
                string name = Dal.Customers.Get().ToList().Find(cust => cust.CustId == item.CustId).CustName;
                bllist.Add(new BLOrder(item, email, name));
                }
             
            }
            return bllist;
        }

        public List<BLOrder> GetNews()
        {
            List<Order> dallist = Dal.Orders.Get();
            List<BLOrder> bllist = new();

            foreach (var item in dallist)
            {
                if (item.Sent == false && item.EmpId == 0) { 
                    string email = Dal.Customers.Get().ToList().Find(cust => cust.CustId == item.CustId).CustEmail;
                    string name = Dal.Customers.Get().ToList().Find(cust => cust.CustId == item.CustId).CustName;
                    bllist.Add(new BLOrder(item, email, name));
                }
            }
            return bllist;
        }
        //the employee update about  sending the order
        public void UpdateSending(int orderId,int empId)
        {
            Dal.Orders.UpdateSending(orderId, empId);
            //List<OrderDetail> sendingProducts = Dal.Orders.Get().ToList().Find(p => p.OrderId == orderId).orderdetails;
            //Dal.Products.UpdateAmount(prodId);
        }
      

        public void AssignOrders(int empId, List<BLOrder> orderList)
        {
            orderList.ToList().ForEach(o =>
            {
                Dal.Orders.AssignOrdersToEmp(empId, o.OrderId);
            });
        }

       
=======
                string email = Dal.Customers.Get().ToList().Find(cust=>cust.CustId==item.CustId).CustEmail;
                string name = Dal.Customers.Get().ToList().Find(cust => cust.CustId == item.CustId).CustName;
                bllist.Add(new BLOrder(item, email, name));
            }
            return bllist;
        }
        
        public List<BLOrder> GetNews()
        {
            throw new NotImplementedException();
        }
        //the employee update about  sending the order
        public void UpdateSending(int orderId)
        {
            Dal.Orders.UpdateSending(orderId);
            //List<OrderDetail> sendingProducts = Dal.Orders.Get().ToList().Find(p => p.OrderId == orderId).orderdetails;
            //Dal.Products.UpdateAmount(prodId);
        }
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
    }
}
