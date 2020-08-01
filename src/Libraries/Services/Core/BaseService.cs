using Domain;
using Repositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core
{
    public abstract class BaseService<T> : IService<T>, IDependency where T : BaseEntity, new()
    {
        #region Fields
        private readonly IRepository<T> _repository;
        #endregion

        #region Ctor
        protected BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> All()
        {
            return _repository.All();
        }

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        /// <returns></returns>
        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return _repository.Filter(predicate);
        }

        /// <summary>
        /// Gets objects from database with filtering and paging.
        /// </summary>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="filter">Specified a filter</param>
        /// <param name="order">Specified a order</param>
        /// <param name="isAsc">Specified ascending or descending</param>
        /// <returns></returns>
        public virtual IQueryable<T> Filter<TOrder>(int index, int size, out int total, Expression<Func<T, bool>> filter, Expression<Func<T, TOrder>> order, bool isAsc = true)
        {
            return _repository.Filter<TOrder>(index, size, out total, filter, order, isAsc);
        }

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        /// <returns></returns>
        public virtual bool Contains(Expression<Func<T, bool>> predicate)
        {
            return _repository.Contains(predicate);
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _repository.Count(predicate);
        }

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        /// <returns></returns>
        public virtual T Find(params object[] keys)
        {
            return _repository.Find(keys);
        }

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        /// <returns></returns>
        public virtual void Create(T t)
        {
            _repository.Create(t);

            _repository.SaveChanges();
        }

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>
        public virtual void Delete(T t)
        {
            _repository.Delete(t);

            _repository.SaveChanges();
        }

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Delete(Expression<Func<T, bool>> predicate)
        {
            return _repository.Delete(predicate);
        }

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        /// <returns></returns>
        public virtual void Update(T t)
        {
            _repository.Update(t);

            _repository.SaveChanges();
        }

        /// <summary>
        /// Select Single Item by specified expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return All().FirstOrDefault(expression);
        }
        #endregion

    }
}
