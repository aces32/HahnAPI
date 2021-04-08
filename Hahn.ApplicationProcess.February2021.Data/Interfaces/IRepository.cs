using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Interfaces
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Get(int ID);
        void Delete(T entity);

        IEnumerable<T> FInd(Expression<Func<T, bool>> predicate);

        void SaveChanges();
    }
}
