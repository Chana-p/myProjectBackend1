﻿using BL.Api;
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
        public int Add(int custId, int? empId)
        {
            Order o = new()
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Today).ToShortDateString(),
                CustId = custId,
                EmpId = empId ?? 0, // Fix for CS8629: Use null-coalescing operator to handle nullable value type
                Sent = false
            };
            return Dal.Orders.Create(o);
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

                Dal.Products.UpdateSum(item.ProdId, item.Count);


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
            List<Order> dallist = Dal.Orders.Get();
            List<BLOrder> bllist = new();

            foreach (var item in dallist)
            {
                string? email = null; // Fix CS8600: Use nullable type for email
                string? name = null;  // Fix CS8600: Use nullable type for name

                if (item.EmpId != 0) // Fix: Check if EmpId is not zero instead of using HasValue  
                {
                    var employee = Dal.Employees.getByID(item.EmpId); // Ensure employee is fetched only once
                    email = employee?.Egmail; // Use null-conditional operator to avoid null reference
                    name = employee?.Ename;  // Use null-conditional operator to avoid null reference
                }

                bllist.Add(new BLOrder(item, email, name));
            }
            return bllist;
        }

        public List<BLOrder> GetForCustomer(int custId)
        {
            List<Order> dallist = Dal.Orders.GetForCustomer(custId);
            List<BLOrder> bllist = new();

            foreach (var item in dallist)
            {
                string email = null;
                string name = null;

                if (item.EmpId != 0) // Fix: Check if EmpId is not zero instead of using HasValue  
                {
                    email = Dal.Employees.getByID(item.EmpId).Egmail;
                    name = Dal.Employees.getByID(item.EmpId).Ename;
                }

                bllist.Add(new BLOrder(item, email, name));
            }
            return bllist;
        }

        public List<BLOrder> GetForEmployee(int empId)
        {
            List<Order> dallist = Dal.Orders.GetForEmployee(empId);
            List<BLOrder> bllist = new();

            foreach (var item in dallist)
            {

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

  
    }
}
