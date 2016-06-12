namespace SySInventory.Core.Model.Entities
{
    public abstract class EntityBase : IEntityIdentifiable
    {
        public int Id { get; set; }
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}
