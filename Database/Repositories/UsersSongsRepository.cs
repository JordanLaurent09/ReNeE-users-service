using Microsoft.EntityFrameworkCore;
using users_service.Database.Context;
using users_service.Database.Entities;
using users_service.Database.Repositories.Interfaces;

namespace users_service.Database.Repositories
{
    public class UsersSongsRepository : IRepository<UsersSongs>
    {
        private readonly ApplicationDbContext _context;

        public UsersSongsRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.SaveChanges();
        }

        public string CreateNew(UsersSongs entity)
        {
            _context.usersSongs.Add(entity);
            _context.SaveChanges();

            return "OK";
        }

        public void Delete(int id)
        {
            UsersSongs song = _context.usersSongs.FirstOrDefault(song => song.Id == id) ?? throw new Exception("Specified data not found");

            _context.usersSongs.Remove(song);
            _context.SaveChanges();
        }

        public IEnumerable<UsersSongs> GetAll()
        {
            IEnumerable<UsersSongs> usersSongs = _context.usersSongs;
            return usersSongs;
        }

        public UsersSongs GetById(int id)
        {
            UsersSongs? song = _context.usersSongs.FirstOrDefault(song => id == song.Id);

            if (song == null) return new UsersSongs();

            return song;
        }

        public IEnumerable<int> GetSongsIds(int userId, int performerId)
        {
            IEnumerable<int> indexes = _context.usersSongs.Where(song => song.UserId == userId && song.PerformerId == performerId).Select(song => song.SongId);

            return indexes;
        }

        public async Task Update(UsersSongs entity)
        {
            UsersSongs song = _context.usersSongs.FirstOrDefault(song => song.Id == entity.Id) ?? throw new Exception("Specified data not found");

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
