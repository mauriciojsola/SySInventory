using System;
using System.Web.Mvc;
using SySInventory.Core.Infrastructure.Persistence;

namespace SySInventory.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
        protected void InUnitOfWork(Action action)
        {
            using (var uow = UnitOfWorkProvider.BeginUnitOfWork())
            {
                action();
                uow.Commit();
            }
        }
    }
}