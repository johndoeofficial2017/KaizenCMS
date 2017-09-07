using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Admin.Repository
{
    public interface IGenericDataRepository<T> where T : class
    {
        ////    IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        ////    DataCollection<T> GetAllWithPaging(int PageSize, int CurrentPage, params Expression<Func<T, object>>[] navigationProperties);
        ////    IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        ////    DataCollection<T> GetListWithPaging(Func<T, bool> where, int PageSize, int CurrentPage, params Expression<Func<T, object>>[] navigationProperties);
        ////    DataCollection<T> GetListWithPaging(string where, int PageSize, int CurrentPage, params Expression<Func<T, object>>[] navigationProperties);
        ////    T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        ////    void Add(params T[] items);
        ////    void Update(params T[] items);
        ////    void Remove(params T[] items);
    }
}
