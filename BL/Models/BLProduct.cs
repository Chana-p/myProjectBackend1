using Dal.newModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace BL.Models
{
  public  class BLProduct
    {
        public int ProdId { get; set; }

        public string Pname { get; set; } = null!;

        public int Psum { get; set; }

        public string Pimporter { get; set; } = null!;

        public string? Pcompany { get; set; }

        public string Ppicture { get; set; }
       
        public string? Pdescription { get; set; }

       

        public BLProduct(ProductsSum p)
        {
            this.ProdId = p.ProdId;
            this.Pcompany = p.Pcompany;
            this.Pdescription = p.Pdescription;
            this.Psum = p.Psum;
            this.Pname = p.Pname;
            this.Pimporter = p.Pimporter;

            this.Ppicture =p.Ppicture;
           
        }

        
        public BLProduct()
        {

        }
//        {
//  "prodId": 0,
//  "pname": "string",
//  "psum": 100,
//  "pimporter": "aaaa",
//  "pcompany": "aaaa",
//  "ppicture": "{BuildIcon}",
//  "pdescription": "excellent"
//}

}
}
