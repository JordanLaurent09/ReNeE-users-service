using users_service.Database.Entities;
using users_service.Database.Repositories;
using users_service.Database.Repositories.Interfaces;
using users_service.Resources.Users.Interfaces;

namespace users_service.Resources.Users
{
    public class UsersPerformersService : IUsersPerformersService
    {
        private IRepository<UsersPerformers> _usersPerformersRepository;
        private ILogger<UsersPerformersService> _logger;


        public UsersPerformersService(IRepository<UsersPerformers> usersPerformersRepository, ILogger<UsersPerformersService> logger)
        {
            _usersPerformersRepository = usersPerformersRepository;
            _logger = logger;
        }

        public string CreateNew(UsersPerformers entity)
        {
            _logger.LogInformation("Добавление исполнителя с идентификатором {performerId} в избранное к пользователю с идентификатором {userId}", entity.PerformerId, entity.UserId);
            string result = _usersPerformersRepository.CreateNew(entity);
            _logger.LogInformation("Ответ сервера: {ServerResponse}", result);
            return result;
        }

        public void Delete(int id)
        {
            try
            {
                _usersPerformersRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public void DeleteByIds(int userId, int performerId)
        {
            try
            {
                UsersPerformersRepository repo = (UsersPerformersRepository) _usersPerformersRepository;
                repo.DeleteByIds(userId, performerId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public IEnumerable<UsersPerformers> GetAll()
        {
            IEnumerable<UsersPerformers> usersPerformers = _usersPerformersRepository.GetAll();

            return usersPerformers;
        }

        public UsersPerformers GetById(int id)
        {
            UsersPerformers usersPerformers = _usersPerformersRepository.GetById(id);

            return usersPerformers;
        }

        public IEnumerable<int> GetUserPerformersId(int userId)
        {
            IEnumerable<int> indexes = [0];
            try
            {
                UsersPerformersRepository usersPerformersRepository = (UsersPerformersRepository) _usersPerformersRepository;
                indexes = usersPerformersRepository.GetUserPerformersId(userId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return indexes;
        }

        public void Update(UsersPerformers entity)
        {
            try
            {
                _usersPerformersRepository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
