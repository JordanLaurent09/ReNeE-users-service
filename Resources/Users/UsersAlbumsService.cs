using users_service.Database.Entities;
using users_service.Database.Repositories;
using users_service.Database.Repositories.Interfaces;
using users_service.Resources.Users.Interfaces;

namespace users_service.Resources.Users
{
    public class UsersAlbumsService : IUsersAlbumsService
    {

        private IRepository<UsersAlbums> _usersAlbumsRepository;

        public UsersAlbumsService(IRepository<UsersAlbums> usersAlbumsRepository)
        {
            _usersAlbumsRepository = usersAlbumsRepository;
        }

        public string CreateNew(UsersAlbums entity)
        {
            string result = _usersAlbumsRepository.CreateNew(entity);

            return result;
        }

        public void Delete(int id)
        {
            try
            {
                _usersAlbumsRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public IEnumerable<int> GetAlbumsIds(int userId, int performerId)
        {
            IEnumerable<int> indexes = [0];
            try
            {
                UsersAlbumsRepository usersAlbumsRepository = (UsersAlbumsRepository)_usersAlbumsRepository;
                indexes = usersAlbumsRepository.GetAlbumsIds(userId, performerId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return indexes;
        }

        public IEnumerable<UsersAlbums> GetAll()
        {
            IEnumerable<UsersAlbums> usersAlbums = _usersAlbumsRepository.GetAll();

            return usersAlbums;
        }

        public UsersAlbums GetById(int id)
        {
            UsersAlbums usersAlbums = _usersAlbumsRepository.GetById(id);

            return usersAlbums;
        }

        public void Update(UsersAlbums entity)
        {
            try
            {
                _usersAlbumsRepository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
