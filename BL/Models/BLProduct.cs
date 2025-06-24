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
<<<<<<< HEAD
        public string? Ppath { get; set; }
       // public string Ppicture { get; set; }
        public byte[] Ppicture { get; set; }

=======

        public string Ppicture { get; set; }
       
>>>>>>> origin/main
        public string? Pdescription { get; set; }

       

        public BLProduct(ProductsSum p)
        {
            this.ProdId = p.ProdId;
            this.Pcompany = p.Pcompany;
            this.Pdescription = p.Pdescription;
            this.Psum = p.Psum;
            this.Pname = p.Pname;
            this.Pimporter = p.Pimporter;
<<<<<<< HEAD
            this.Ppath = "D:\\GitHubמה שקיים ב - Copy\\c#\\CPC#PROJECT\\wwwroot\\img\\"+p.Ppicture;
            this.Ppicture =
                File.ReadAllBytes(Ppath);

=======

            this.Ppicture =p.Ppicture;
           
>>>>>>> origin/main
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
