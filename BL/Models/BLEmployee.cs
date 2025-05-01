using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
   public class BLEmployee
    {
        public int EmpId { get; set; }

        public int EmpNum { get; set; }

        public string Ename { get; set; } = null!;

        public string Egmail { get; set; } = null!;

        public string Ephone { get; set; } = null!;

        public BLEmployee()
        {
            
        }
        public BLEmployee(Employee e)
        {
            this.EmpId = e.EmpId;
            this.EmpNum = e.EmpNum;
            this.Ename = e.Ename;
            this.Egmail = e.Egmail;
            this.Ephone = e.Ephone;

        }
    }
}
