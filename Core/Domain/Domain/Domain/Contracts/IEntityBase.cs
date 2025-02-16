namespace Domain.Contracts
{
    public interface IEntityBase
    {
        public int Id { get; set; }
        DateTime? CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
