namespace API.Contracts
{
    public interface IRepository<T> where T : class
    {
        T Create(T entity);
        T GetById(Guid guid);
        IEnumerable<T> GetAll();
        bool Update(T entity);
        bool Delete(Guid guid);
    }

}
