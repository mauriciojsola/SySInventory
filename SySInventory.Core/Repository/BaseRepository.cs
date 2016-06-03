using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using SySInventory.Core.Common.Extensions;
using SySInventory.Core.Infrastructure.Persistence;
using SySInventory.Core.Model;

namespace SySInventory.Core.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class, IIdentifiable
    {
        private static readonly Expression<Func<IIdentifiable, int>> ID_PROP = d => d.Id;
        private static readonly string ID_PROP_NAME = ID_PROP.GetPropertyName();

        private IContextProvider _objectContextProvider { get; set; }

        public BaseRepository(IContextProvider objectContextProvider)
        {
            _objectContextProvider = objectContextProvider;
        }

        protected DbSet<T> Query()
        {
            return _objectContextProvider.Current.Set<T>();
        }

        public T GetById(int id)
        {
            return Query().SingleOrDefault(IdEqualTo(id));
        }

        private static Expression<Func<T, bool>> IdEqualTo(int id)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, ID_PROP_NAME);
            var constant = Expression.Constant(id);
            var equal = Expression.Equal(property, constant);

            return Expression.Lambda<Func<T, bool>>(equal, parameter);
        }

        public void Add(T t)
        {
            Query().Add(t);
        }

        public void Update(T t)
        {
            _objectContextProvider.Current.Entry(t).State = EntityState.Modified;
        }

        public void Remove(T t)
        {
            Query().Remove(t);
        }

        public void Remove(int id)
        {
            Remove(GetById(id));
        }

        /// <summary>
        /// Find for all objects, even the ones that are marked as "deleted"
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return Query();
        }
    }
}
