namespace MoneyBee.CustomerModule.Application.Abstractions
{
    /// <summary>Unit of Work abstraction.</summary>
    public interface IUnitOfWork
    {
        /// <summary>Persists all changes as a single transaction.</summary>
        Task<int> SaveChangesAsync();
    }
}