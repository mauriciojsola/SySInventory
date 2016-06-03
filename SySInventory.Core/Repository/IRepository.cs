using System.Linq;
using SySInventory.Core.Model;

namespace SySInventory.Core.Repository
{
    public interface IRepository { }

    public interface IRepository<T> : IRepository where T : class, IIdentifiable
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        void Add(T t);
        void Update(T t);
        void Remove(T t);
        void Remove(int id);
    }

   
}
