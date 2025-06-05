using Dal;
using Dal.newModels;
using Dal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
  public  class BLOrder
    {
<<<<<<< HEAD
        public int OrderId { get; set; }
=======
        public int OrderId { get; }
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
        public string OrderDate { get; set; }
        public int CustId { get; set; }
        public int EmpId { get; set; }
        public string? EmailToConnection { get; set; }
        public string? NameToConnection { get; set; }
        public string? PaymentType { get; set; }
        public bool? Sent { get; set; }
        //public /*virtual*/ ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public BLOrder(Order o)
        {
            this.OrderId = o.OrderId;
            this.CustId = o.CustId;
<<<<<<< HEAD
            this.EmpId = o.EmpId ?? 0;
=======
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
            this.OrderDate = o.OrderDate;
            this.PaymentType = o.PaymentType;
            this.Sent = o.Sent;
        }
        public BLOrder(Order o,string empEmail,string empName)
        {
            this.OrderId = o.OrderId;
            this.CustId = o.CustId;
            this.OrderDate = o.OrderDate;
<<<<<<< HEAD
            this.EmpId = o.EmpId ?? 0;
=======
>>>>>>> 3cd299ced9c0d58f45d3f948a703dc21b0ed7550
            this.PaymentType = o.PaymentType;
            this.Sent = o.Sent;
            this.EmailToConnection = empEmail;
            this.NameToConnection = empName;
        }
        override
        public  string ToString()
        {
            return OrderId + " " + OrderDate + " " + CustId;
        }
       
    }
}
