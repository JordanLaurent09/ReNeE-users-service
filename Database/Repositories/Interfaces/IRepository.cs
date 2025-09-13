namespace users_service.Database.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        //T GetByCredentials(U credential, string password);

        string CreateNew(T entity);

        Task Update(T entity);

        void Delete(int id);

    }
}
