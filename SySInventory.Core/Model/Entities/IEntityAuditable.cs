using System;

namespace SySInventory.Core.Model.Entities
{
    public interface IEntityAuditable
    {
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}