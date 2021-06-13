using PagedList;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    //Bu VM'imiz ProductVM ile aynı gözükebilir ama bu class'ın amacı: ProductVM'e ek olarak alışveriş tarafında sayfalandırma işlemlerini de yapacak bir Pagination tipi tutmaktır. Admin için sayfalandırmaya ihtiyaç yoktur çünkü Admin'in kullandığı Template'te bu işlemler hazır yapılmıştır.
    //PA : Pagination
    public class PAVM
    {
        public IPagedList<Product> PagedProducts { get; set; }//Ürünleri normal List'ten farklı olarak, sayfalandırılabilecek bir şekilde tutar.
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }


    }
}