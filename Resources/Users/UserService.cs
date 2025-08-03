using users_service.Database.Entities;
using users_service.Database.Repositories.Interfaces;
using users_service.Resources.Users.Interfaces;

namespace users_service.Resources.Users
{
    public class UserService : IUserService
    {
        private IRepository<User, string> _usersRepository;
        private ILogger<UserService> _logger;

        public UserService(IRepository<User, string> usersRepository, ILogger<UserService> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public string CreateUser(User newUser)
        {
            _logger.LogInformation("Создание пользователя с логином {Login} и адресом электронной почты {Email}", newUser.Login, newUser.Email);
            string result = _usersRepository.CreateNew(newUser);
            _logger.LogInformation("Ответ сервера: {ServerResponse}", result);
            return result;
        }

        public void DeleteUser(int id)
        {
            _logger.LogInformation("Удаление пользователя с идентификатором {id}", id);
            try
            {
                _usersRepository.Delete(id);
                _logger.LogInformation("Пользователь с идентификатором {id} успешно удалён", id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка удаления пользователя {ex.Message}", ex.Message);
                throw new Exception();
            }
        }

        public User GetByCredentials(string credential, string password)
        {
            _logger.LogInformation("Логин пользователя по {credential}", credential);
            User user = _usersRepository.GetByCredentials(credential, password);
            _logger.LogInformation("Получен пользователь с идентификатором {user.Id}", user.Id);
            return user;
        }

        public User GetById(int id)
        {
            _logger.LogInformation("Получение пользователя с идентификатором {id}", id);
            User user = _usersRepository.GetById(id);
            if (user.Id == 0)
            {
                _logger.LogInformation("Пользователь с идентификатором {id} не найден", id);
            }
            else
            {
                _logger.LogInformation("Получен пользователь с идентификатором {user.Id}", user.Id);
            }
                
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            _logger.LogInformation("Запрос на получение списка всех пользователей");

            IEnumerable<User> users = _usersRepository.GetAll();

            if (!users.Any())
            {
                _logger.LogInformation("В БД не найдено пользователей");
            }
            else
            {
                _logger.LogInformation("Получен список всех пользователей");
            }
            return _usersRepository.GetAll();
        }

        public void UpdateUser(User user)
        {
            try
            {
                _logger.LogInformation("Изменение данных пользователя с идентификатором {user.Id}", user.Id);
                _usersRepository.Update(user);
                _logger.LogInformation("Данные пользователя с идентификатором {user.Id} успешно изменены", user.Id);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Ошибка при изменении данных пользователя {ex.Data}, стек {ex.StackTrace}", ex.Data, ex.StackTrace);
                throw new Exception();
            }
            
        }
    }
}
