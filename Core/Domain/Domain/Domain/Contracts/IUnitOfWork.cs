namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
