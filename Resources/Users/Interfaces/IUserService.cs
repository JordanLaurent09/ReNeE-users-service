using users_service.Database.Entities;

namespace users_service.Resources.Users.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        User GetById(int id);

        User GetByCredentials(string credential, string password);

        string CreateUser(User newUser);

        void UpdateUser(User user);

        void DeleteUser(int id);
    }
}
