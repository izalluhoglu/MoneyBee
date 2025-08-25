namespace MoneyBee.AuthModule.Application.Abstractions
{
    /// <summary>Unit of Work abstraction.</summary>
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}