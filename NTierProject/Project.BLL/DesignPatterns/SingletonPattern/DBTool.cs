using Project.DAL.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.SingletonPattern
{
    //Bu paternimin amacı eğer DB'nin instance'ı alınmışsa tekrar aldırtmamak
    //Sadece class static değil çünkü içerisinde instance member barındırıyor
    public class DBTool
    {
        DBTool() { }

        static MyContext _dbInstance;

        public static MyContext DBInstance
        {
            get
            {
                if (_dbInstance != null) return _dbInstance;

                return _dbInstance = new MyContext();
            }
        }
    }
}
