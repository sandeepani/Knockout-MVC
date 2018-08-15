
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T item);
        bool Update(T item);
        bool Delete(int id);
    }
}