namespace users_service.Database.Repositories.Interfaces
{
    public interface IRepository<T, U>
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        T GetByCredentials(U credential, string password);

        string CreateNew(T entity);

        void Update(T entity);

        void Delete(int id);

    }
}
