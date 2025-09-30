using Microsoft.EntityFrameworkCore;
using users_service.Database.Context;
using users_service.Database.Entities;
using users_service.Database.Repositories.Interfaces;

namespace users_service.Database.Repositories
{
    public class UsersPerformersRepository : IRepository<UsersPerformers>
    {
        private readonly ApplicationDbContext _context;

        public UsersPerformersRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.SaveChanges();
        }

        public string CreateNew(UsersPerformers entity)
        {
            try
            {
                _context.usersPerformers.Add(entity);
                _context.SaveChanges();

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error";
            }
            
        }

        public void Delete(int id)
        {
            UsersPerformers up = _context.usersPerformers.FirstOrDefault(up => up.Id == id) ?? throw new Exception("Specified data not found");

            _context.usersPerformers.Remove(up);
            _context.SaveChanges();
        }

        // new method
        public void DeleteByIds(int userId, int performerId)
        {
            UsersPerformers up = _context.usersPerformers.FirstOrDefault(up => up.UserId == userId && up.PerformerId == performerId) ?? throw new Exception("Specified data not found");

            _context.usersPerformers.Remove(up);
            _context.SaveChanges();
        }

        public IEnumerable<UsersPerformers> GetAll()
        {
            IEnumerable<UsersPerformers> usersPerformers = _context.usersPerformers;
            return _context.usersPerformers;
        }

        public UsersPerformers GetById(int id)
        {
            UsersPerformers? data = _context.usersPerformers.FirstOrDefault(up => id == up.Id);

            if (data == null) return new UsersPerformers();

            return data;
        }

        // new method
        public IEnumerable<int> GetUserPerformersId(int userId)
        {
            IEnumerable<int> ids = _context.usersPerformers.Where(up => userId == up.UserId).Select(up => up.PerformerId);

            return ids;
        }
 

        public async Task Update(UsersPerformers entity)
        {
            UsersPerformers data = _context.usersPerformers.FirstOrDefault(up => up.Id == entity.Id) ?? throw new Exception("Specified data not found");

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
