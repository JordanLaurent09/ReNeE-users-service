using users_service.Database.Entities;

namespace users_service.Resources.Users.Interfaces
{
    public interface IUsersPerformersService
    {
        IEnumerable<UsersPerformers> GetAll();

        string CreateNew(UsersPerformers entity);

        void Delete(int id);

        void DeleteByIds(int userId, int performerId);

        UsersPerformers GetById(int id);

        IEnumerable<int> GetUserPerformersId(int userId);

        void Update(UsersPerformers entity);
    }
}
