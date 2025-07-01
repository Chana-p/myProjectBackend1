using Dal.Api;
using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
   public class DalEmployeeService : IDalEmployee
    {
        dbcontext Dal;

        public DalEmployeeService(dbcontext db)
        {
            Dal = db;
        }
        public void Add(Employee e)
        {
            Dal.Employees.Add(e);
            Dal.SaveChanges();
        }


        public void Delete(Employee e)
        {
            throw new NotImplementedException();
        }

        public List<Employee> getAll()
        {
          return Dal.Employees.ToList();
        }

        public Employee getByID(int id)
        {
            return Dal.Employees.ToList().Find(e =>e.EmpId==id);
        }

        public Employee getByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
