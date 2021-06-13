using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.ShoppingTools
{
    //Sepette product sınıfımızın yansıması olarak bu class'ı kullanıcaz
    public class CartItem
    {
        public int ID { get; set; } //Ürünü Post tarafında find edebilmemiz için product'ın ID'sini CartItem'ın ID'sine aticaz
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get { return Price * Amount; } } //AraToplam property'miz ReadOnly, Cart class'ında sepetin toplam tutarını hesaplamak için kullandık
        public short Amount { get; set; }
        public string ImagePath { get; set; } //Sepetteki her ürünün resmini görmek istiyoruz
        public CartItem()
        {
            ++Amount; //Amount property'miz struct type olduğu için 0'dan başlar, bunu düzeltmemiz için her instance'da 1 arttırdık
        }
    }
}