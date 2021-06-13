using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.ShoppingTools
{
    //Sepet işlevini görücek class'ımız
    public class Cart
    {
        Dictionary<int, CartItem> _sepetim; //List yapmamamızın nedeni: sepetteki ürün için her find işleminde ürünlerde dönmek değil de, key'ini verip direk value'suna ulaşmak istediğimiz için
        public Cart()
        {
            _sepetim = new Dictionary<int, CartItem>();
        }

        public List<CartItem> Sepetim //ReadOnly property'miz, sadece Dictionary'nin value'larını döndürücek
        {
            get
            {
                return _sepetim.Values.ToList(); 
            }
        }

        public void SepeteEkle(CartItem item) //Önceden  sepetteyse Amount'unu arttık, yoksa sepete ekle
        {
            if (_sepetim.ContainsKey(item.ID))
            {
                _sepetim[item.ID].Amount += 1;
                return;
            }

            _sepetim.Add(item.ID, item);
        }

        public void SepettenSil(int id) //Amount 1'den fazlaysa Amount'unu azalt, yoksa sepeti sil
        {
            if (_sepetim[id].Amount > 1)
            {
                _sepetim[id].Amount -= 1;
                return;
            }

            _sepetim.Remove(id);
        }

        public decimal TotalPrice //Sepetteki ürünlerin toplam tutarını hesapladık
        {
            get
            {
                return _sepetim.Sum(x => x.Value.SubTotal);
            }
        }
    }
}