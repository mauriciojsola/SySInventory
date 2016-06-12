using System;

namespace SySInventory.Core.Model.Entities
{
    public abstract class AuditableEntityBase : EntityBase, IEntityAuditable
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
