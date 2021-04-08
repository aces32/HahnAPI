using Hahn.ApplicationProcess.February2021.Data.DBContext;
using Hahn.ApplicationProcess.February2021.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Services
{
    public abstract class GenericRepository<T>
        : IRepository<T> where T : class
    {
        protected AssetDBContext assetDBContext;

        public GenericRepository(AssetDBContext assetDBContext)
        {
            this.assetDBContext = assetDBContext;
        }
        public virtual T Add(T entity)
        {
            return assetDBContext.Add(entity).Entity;
        }

        public virtual void Delete(T entity)
        {
            assetDBContext.Remove(entity);
        }

        public virtual IEnumerable<T> FInd(Expression<Func<T, bool>> predicate)
        {
            return assetDBContext.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToList();
        }

        public virtual T Get(int ID)
        {
            return assetDBContext.Find<T>(ID);
        }

        public virtual void SaveChanges()
        {
            assetDBContext.SaveChanges();
        }

        public virtual T Update(T entity)
        {
            return assetDBContext.Update(entity).Entity;
        }
    }
}
