using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
   public class BLCustomer
    {
        public int CustId { get; set; }
        public int CustNum { get;  }

        public string CustName { get; set; } = null!;

        public string? CustAddress { get; set; }

        public string CustEmail { get; set; } = null!;

        public string? CustPhone { get; set; }

        public BLCustomer()
        {
            
        }
        //  public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public BLCustomer(Customer c)
        {
            this.CustId = c.CustId;
            this.CustName = c.CustName;
            this.CustNum= c.CustNum;
            this.CustAddress = c.CustAddress;
            this.CustEmail = c.CustEmail;
            this.CustPhone = c.CustPhone;


        }
    }
}
