using Microsoft.EntityFrameworkCore;
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
            entity.RegisterTime = DateTime.UtcNow;
            entity.LastVisit = DateTime.UtcNow;

            User? foundUserWithEmail = _context.users.FirstOrDefault(u => u.Email == entity.Email);

            User? foundUserWithLogin = _context.users.FirstOrDefault(u => u.Login == entity.Login);
     

            if (foundUserWithEmail != null && foundUserWithLogin is null)
            {
                return "Пользователь с таким адресом уже существует!";
            }
            else if (foundUserWithLogin != null && foundUserWithEmail is null)
            {
                return "Пользователь с таким логином уже существует!";
            }
            else if (foundUserWithEmail != null && foundUserWithLogin != null)
            {
                return "Пользователь с такми логином и паролем уже существует!";
            }
            else
            {   
                _context.users.Add(entity);
                _context.SaveChanges();
                return "Пользователь успешно зарегистрирован";
            }                         
        }

        public void Delete(int id)
        {
            User? user = _context.users.FirstOrDefault(u => u.Id == id);
            if (user is null) return;

            _context.users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.users;
        }

        public User GetByCredentials(string credential, string password)
        {
            User? user = _context.users.FirstOrDefault(u => (u.Email == credential || u.Login == credential) && u.Password == password);

            if (user is null) return new User();

            return user;
        }

        public User GetById(int id)
        {
            User? user = _context.users.FirstOrDefault(u => u.Id == id);

            if (user is null) return new User();

            else return user;
         
        }

        public async Task Update(User entity)
        {
            entity.RegisterTime = entity.RegisterTime.ToUniversalTime();
            entity.LastVisit = entity.LastVisit.ToUniversalTime();
                      
            _context.Entry(entity).State = EntityState.Modified;
           
            await _context.SaveChangesAsync();
        }
    }
}
