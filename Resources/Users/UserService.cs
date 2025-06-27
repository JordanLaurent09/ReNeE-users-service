using users_service.Database.Entities;
using users_service.Database.Repositories.Interfaces;
using users_service.Resources.Users.Interfaces;

namespace users_service.Resources.Users
{
    public class UserService : IUserService
    {
        private IRepository<User, string> _usersRepository;

        public UserService(IRepository<User, string> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public string CreateUser(User newUser)
        {
            return _usersRepository.CreateNew(newUser);
        }

        public void DeleteUser(int id)
        {
            _usersRepository.Delete(id);
        }

        public User GetByCredentials(string credential, string password)
        {
            return _usersRepository.GetByCredentials(credential, password);
        }

        public User GetById(int id)
        {
            return _usersRepository.GetById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _usersRepository.GetAll();
        }

        public void UpdateUser(User user)
        {
            _usersRepository.Update(user);
        }
    }
}
