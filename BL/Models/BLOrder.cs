﻿using Dal;
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

        public int OrderId { get; set; }

        public string OrderDate { get; set; }
        public int CustId { get; set; }
        public int EmpId { get; set; }
        public string? EmailToConnection { get; set; }
        public string? NameToConnection { get; set; }
        public string? PaymentType { get; set; }
        public bool? Sent { get; set; }
        
        public BLOrder(Order o)
        {
            this.OrderId = o.OrderId;
            this.CustId = o.CustId;

            this.EmpId = o.EmpId ?? 0;

            this.Sent = o.Sent;
        }
        public BLOrder(Order o,string empEmail,string empName)
        {
            this.OrderId = o.OrderId;
            this.CustId = o.CustId;
            this.OrderDate = o.OrderDate;
            this.EmpId = o.EmpId ?? 0;

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
