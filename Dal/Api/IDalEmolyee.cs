using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
  public  interface IDalEmployee
    {
        List<Employee> getAll();
        Employee getByName(string name);
        Employee getByID(int id);
     //   Employee AvailableEmployee();
        void Add(Employee e);
        void Delete(Employee e);
    }
}
