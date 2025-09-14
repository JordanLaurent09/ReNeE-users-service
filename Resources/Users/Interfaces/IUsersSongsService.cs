using users_service.Database.Entities;

namespace users_service.Resources.Users.Interfaces
{
    public interface IUsersSongsService
    {
        string CreateNew(UsersSongs entity);

        void Delete(int id);

        IEnumerable<UsersSongs> GetAll();

        UsersSongs GetById(int id);

        IEnumerable<int> GetSongsIds(int userId, int performerId);

        void Update(UsersSongs entity);
    }
}
