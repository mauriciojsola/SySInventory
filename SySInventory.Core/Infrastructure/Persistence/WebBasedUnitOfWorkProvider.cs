using System.Web;

namespace SySInventory.Core.Infrastructure.Persistence
{
    public class WebBasedUnitOfWorkProvider : IUnitOfWorkProvider
    {
        private const string HttpContextKey = "EF.Uow.Key";

        public IContextProvider ContextProvider { private get; set; }

        public IUnitOfWork BeginUnitOfWork()
        {
            ContextProvider.Clear();
            HttpContext.Current.Items[HttpContextKey] = new UnitOfWork(ContextProvider);
            return Current;
        }

        public IUnitOfWork Current
        {
            get { return HttpContext.Current.Items[HttpContextKey] as UnitOfWork; }
        }
    }
}
