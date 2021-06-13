using Project.BLL.DesignPatterns.GenericRepository.IntRep;
using Project.BLL.DesignPatterns.SingletonPattern;
using Project.DAL.ContextClasses;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.BaseRep
{
    //Her Repository class'ımda olmasını istediğim kısayollarım
    public abstract class BaseRep<T> : IRepository<T> where T : BaseEntity
    {
        protected MyContext _db; //Protected yapma sebebim: hem miras alan diğer repository'lerde de farklı şekilde kullanmak istersem hazır olması için
        public BaseRep()
        {
            _db = DBTool.DBInstance; //BaseRep'den miras alındığı gibi SingletonPattern çalıştırdım
        }

        protected void Save() //Protected yapma sebebim: hem miras alan diğer repository'lerde de farklı şekilde kullanmak istersem hazır olması için
        {
            _db.SaveChanges();
        }

        public void Add(T item)
        {
            _db.Set<T>().Add(item);
            Save();
        }

        public void AddRange(List<T> item)
        {
            _db.Set<T>().AddRange(item); //DBset class'ında AddRange property'si olduğu için foreach'te dönmek yerine direk property'sini kullandım
            Save();
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Any(exp);
        }

        public void Delete(T item)
        {
            item.DeletedDate = DateTime.Now; //Silinme tarihini ekledim
            item.Status = ENTITIES.Enums.DataStatus.Deleted; //Delete işleminde sadece verinin durumunu delete'e çekiyorum, veriye hala DB'den ulaşılabilir
            Save();
        }

        public void DeleteRange(List<T> item)
        {
            foreach (T element in item)
            {
                Delete(element);
            }
        }

        public void Destroy(T item)
        {
            _db.Set<T>().Remove(item);
            Save();
        }

        public void DestroyRange(List<T> item)
        {
            foreach (T element in item)
            {
                Destroy(element);
            }
        }

        public T Find(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public T FindFirstData()
        {
            return _db.Set<T>().OrderBy(x => x.CreatedDate).FirstOrDefault();

            //return GettAll().FirstOrDefault(); //Burda DB'ye ilk gireni alır, yukarıda CreatedDate'i en genç olanı alır (Her DB ilk gireni ilk çıkarıcak diye bir şart yok)
        }

        public T FindLastData()
        {
            return _db.Set<T>().OrderByDescending(x => x.CreatedDate).FirstOrDefault(); //CreatedDate'e göre en yaşlı olan veriyi alır
        }

        public T FirstOrDefault(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().FirstOrDefault(exp); //İlk değeri getirir, veri yoksa null değerini verir
        }

        public List<T> GetActives()
        {
            return Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted); //Aktif veriler istendiğinde, silinmemiş verilerin hepsini getiririz
        }

        public List<T> GetModifieds()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Updated);
        }

        public List<T> GetPassives()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Deleted); //Passive'den kastımız deleted durumundaki verilerdir
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public object Select(Expression<Func<T, object>> exp) //Gelicek verinin tipi belli olmadığından object yaptım çünkü object her tipin üst hiyerarşisidir
        {
            return _db.Set<T>().Select(exp).ToList();
        }

        public void Update(T item)
        {
            item.Status = ENTITIES.Enums.DataStatus.Updated; //Veri durumunu Updated'a çektim
            item.ModifiedDate = DateTime.Now; //Updated olunduğu tarihi ekledim
            T toBeUpdated = Find(item.ID); //Update olacak veriyi DB'den buldum
            _db.Entry(toBeUpdated).CurrentValues.SetValues(item); //DB'deki verinin değiştirilmiş olan verilerini güncelle diğerleri aynı tut komutu
            Save();
        }

        public void UpdateRange(List<T> item)
        {
            foreach (T element in item)
            {
                Update(element);
            }
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Where(exp).ToList();
        }
    }
}
