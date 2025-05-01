using BL.Models;
using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
 public   interface IBLProducts
    {
        List<BLProduct> Get();
        BLProduct GetByID(int id);
        List<BLProduct> DeleteFromList(int prod);
        List<BLProduct> Add(BLProduct product);
        List<BLProduct> UpdateAmount(int prodId,int amount);
        List<BLProduct> Update(BLProduct product);
    }
}
