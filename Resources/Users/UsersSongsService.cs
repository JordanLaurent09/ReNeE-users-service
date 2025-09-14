using users_service.Database.Entities;
using users_service.Database.Repositories;
using users_service.Database.Repositories.Interfaces;
using users_service.Resources.Users.Interfaces;

namespace users_service.Resources.Users
{
    public class UsersSongsService: IUsersSongsService
    {
        private IRepository<UsersSongs> _usersSongsRepository;

        public UsersSongsService(IRepository<UsersSongs> usersSongsRepository)
        {
            _usersSongsRepository = usersSongsRepository;
        }

        public string CreateNew(UsersSongs entity)
        {
            string result = _usersSongsRepository.CreateNew(entity);

            return result;
        }

        public void Delete(int id)
        {
            try
            {
                _usersSongsRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public IEnumerable<int> GetSongsIds(int userId, int performerId)
        {
            IEnumerable<int> indexes = [0];
            try
            {
                UsersSongsRepository usersSongsRepository = (UsersSongsRepository)_usersSongsRepository;
                indexes = usersSongsRepository.GetSongsIds(userId, performerId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return indexes;
        }

        public IEnumerable<UsersSongs> GetAll()
        {
            IEnumerable<UsersSongs> usersSongs = _usersSongsRepository.GetAll();

            return usersSongs;
        }

        public UsersSongs GetById(int id)
        {
            UsersSongs usersSongs = _usersSongsRepository.GetById(id);

            return usersSongs;
        }

        public void Update(UsersSongs entity)
        {
            try
            {
                _usersSongsRepository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
