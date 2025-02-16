namespace Domain.Contracts
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
