using System.Web.Mvc;
using SySInventory.Core.Infrastructure.Persistence;

namespace SySInventory.Web.Controllers.Filters
{
    public class UnitOfWorkFilter : IActionFilter, IExceptionFilter
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { private get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UnitOfWorkProvider.BeginUnitOfWork();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null) return;

            UnitOfWorkProvider.Current.Commit();
            //UnitOfWorkProvider.Current.Dispose();
        }

        public void OnException(ExceptionContext filterContext)
        {
            UnitOfWorkProvider.Current.Rollback();
            //UnitOfWorkProvider.Current.Dispose();
        }
    }
}