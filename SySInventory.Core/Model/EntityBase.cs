using System;
using System.ComponentModel.DataAnnotations;

namespace SySInventory.Core.Model
{
    public abstract class EntityBase : IIdentifiable
    {
        public int Id { get; set; }
        //public DateTime DateCreated { get; set; }
        //public DateTime DateModified { get; set; }
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}
