using users_service.Database.Context;
using users_service.Database.Entities;
using users_service.Database.Repositories.Interfaces;

namespace users_service.Database.Repositories
{
    public class UserRepository : IRepository<User, string>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.SaveChanges();
        }

        public string CreateNew(User entity)
        {
            User? foundUserWithEmail = _context.Users.FirstOrDefault(u => u.Email == entity.Email);

            User? foundUserWithLogin = _context.Users.FirstOrDefault(entity => entity.Login == entity.Login);

            if (foundUserWithEmail != null && foundUserWithLogin is null)
            {
                return "Пользователь с таким адресом уже существует!";
            }
            else if (foundUserWithEmail is null && foundUserWithLogin != null)
            {
                return "Пользователь с таким логином уже существует!";
            }

            else
            {
                _context.Users.Add(entity);
                _context.SaveChanges();
                return "Пользователь успешно зарегистрирован";
            }                         
        }

        public void Delete(int id)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user is null) return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetByCredentials(string credential, string password)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Email == credential || u.Login == credential);

            if (user is null) return new User();

            return user;
        }

        public User GetById(int id)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user is null) return new User();

            else return user;
         
        }

        public void Update(User entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
