using Dal.Api;
using Dal.newModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalProductsService : IDalProducts
    {
        dbcontext dbcontext;
        public DalProductsService(dbcontext d)
        {
            dbcontext = d;
        }

        public List<ProductsSum> Add(ProductsSum product)
        {
            dbcontext.ProductsSums.Add(product);
            dbcontext.SaveChanges();
            return dbcontext.ProductsSums.ToList();
        }

        public List<ProductsSum> Get()
        {   
            return dbcontext.ProductsSums.ToList().Where(ps=>ps.Psum>0).ToList();
        }         


        public List<ProductsSum> GetAll()
        {
            return dbcontext.ProductsSums.ToList();

        }
        //ביטלתי את הפונקציה כדי לא לגרור בעיות בהזמנות במקום זה נעדכן כמות במלאי 0
        //public void Remove(int prod, int count)
        //{
        //    List<ProductsSum> ls=Get();

        // var x= ls.Find(i => i.ProdId == prod);
        //    dbcontext.ProductsSums.Remove(x);
        //}

        public List<ProductsSum> Update(ProductsSum product)
        {
            //List<ProductsSum> ls = Get();
            //var x = ls.Find(i => i.ProdId == product.ProdId);
            //product.ProdId=x.ProdId;
            //dbcontext.ProductsSums.Remove(x);
            //dbcontext.ProductsSums.Add(product);
            dbcontext.ProductsSums.Update(product);
            dbcontext.SaveChanges();
            return Get();
          
        }
        public List<ProductsSum> RemoveFromActualList(int prodId)
        {
            List<ProductsSum> ls = Get();
            var x = ls.Find(i => i.ProdId == prodId);
            x.Psum = 0;
            dbcontext.ProductsSums.Update(x);
            dbcontext.SaveChanges();
            return Get();
        }

        public List<ProductsSum> UpdateSum(int prodId, int count)
        {
            List<ProductsSum> ls = Get(); 
            var x = ls.Find(i => i.ProdId == prodId);
            x.Psum = count;
            dbcontext.ProductsSums.Update(x);
            dbcontext.SaveChanges();
            return Get();
        }
    }
}
