using BL.Api;
using BL.Models;
using Dal;
using Dal.Api;
using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    class BLEmployeeService : IBLEmployee
    {
        IDal dal;
        public BLEmployeeService(IDal dal) { 
            this.dal = dal;
        }
        public BLEmployee Create(BLEmployee bLEmp)
        {
            Employee e = new()
            {
                EmpId = bLEmp.EmpId,
               // EmpNum = bLEmp.EmpNum,
                Ename = bLEmp.Ename,
                Egmail = bLEmp.Egmail,
                Ephone = bLEmp.Ephone,
            };
            dal.Employees.Add(e);
            return new BLEmployee(dal.Employees.getByID(bLEmp.EmpId));
        }

        public List<BLEmployee> Get()
        {
            List<BLEmployee> blList = new();
            foreach (var emp in dal.Employees.getAll())
            {
                BLEmployee e = new BLEmployee()
                {
                    EmpId = emp.EmpId,
                    EmpNum = emp.EmpNum,
                    Ename = emp.Ename,
                    Egmail = emp.Egmail,
                    Ephone = emp.Ephone,
                };
               blList.Add(e);
            }
            return blList  ;
        }

        public BLEmployee GetById(int id)
        {
            Employee e = dal.Employees.getByID(id);
          return new BLEmployee()
          {
              EmpId = e.EmpId,
              EmpNum = e.EmpNum,
              Ename = e.Ename,
              Egmail = e.Egmail,
              Ephone = e.Ephone
          };
        }
    }
}
