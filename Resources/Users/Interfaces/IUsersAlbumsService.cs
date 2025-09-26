using Microsoft.EntityFrameworkCore;
using users_service.Database.Entities;

namespace users_service.Resources.Users.Interfaces
{
    public interface IUsersAlbumsService
    {
        string CreateNew(UsersAlbums entity);

        void Delete(int id);

        void DeleteByIds(int userId, int performerId);

        IEnumerable<UsersAlbums> GetAll();

        UsersAlbums GetById(int id);

        IEnumerable<int> GetAlbumsIds(int userId, int performerId);

        void Update(UsersAlbums entity);
    }
}
