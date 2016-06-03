using System.Data.Entity;
using System.Web;

namespace SySInventory.Core.Infrastructure.Persistence
{
    public class WebBasedContextProvider : IContextProvider
    {
        private const string Httpcontextkey = "EF.DbContext.Key";

        public IContextFactory ContextFactory { private get; set; }

        public bool HasCurrent
        {
            get { return (HttpContext.Current.Items[Httpcontextkey] as DbContext) != null; }
        }

        public DbContext Current
        {
            get
            {
                var context = HttpContext.Current.Items[Httpcontextkey] as DbContext;
                if (context == null)
                {
                    HttpContext.Current.Items[Httpcontextkey] =
                        context = ContextFactory.CreateContext();
                }

                return context;
            }
        }

        public void Clear()
        {
            HttpContext.Current.Items[Httpcontextkey] = null;
        }
    }
}
