using Autofac;

namespace SySInventory.Core.Infrastructure.Ioc
{
    public interface IResolvable
    {
        IContainer IoCContainer { get; set; }
    }
}
