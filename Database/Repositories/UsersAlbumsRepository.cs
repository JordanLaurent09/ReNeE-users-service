using Microsoft.EntityFrameworkCore;
using users_service.Database.Context;
using users_service.Database.Entities;
using users_service.Database.Repositories.Interfaces;

namespace users_service.Database.Repositories
{
    public class UsersAlbumsRepository : IRepository<UsersAlbums>
    {
        private readonly ApplicationDbContext _context;

        public UsersAlbumsRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.SaveChanges();
        }

        public string CreateNew(UsersAlbums entity)
        {
            _context.usersAlbums.Add(entity);
            _context.SaveChanges();

            return "OK";
        }

        public void Delete(int id)
        {
            UsersAlbums album = _context.usersAlbums.FirstOrDefault(album => album.Id == id) ?? throw new Exception("Specified data not found");

            _context.usersAlbums.Remove(album);
            _context.SaveChanges();
        }

        public IEnumerable<UsersAlbums> GetAll()
        {
            IEnumerable<UsersAlbums> usersAlbums = _context.usersAlbums;
            return usersAlbums;
        }

        public UsersAlbums GetById(int id)
        {
            UsersAlbums? album = _context.usersAlbums.FirstOrDefault(album => id == album.Id);

            if (album == null) return new UsersAlbums();

            return album;
        }

        public IEnumerable<int> GetAlbumsIds(int userId, int performerId)
        {
            IEnumerable<int> indexes = _context.usersAlbums.Where(album => album.UserId == userId &&  album.PerformerId == performerId).Select(album => album.AlbumId);

            return indexes;
        }

        public async Task Update(UsersAlbums entity)
        {
            UsersAlbums album = _context.usersAlbums.FirstOrDefault(album => album.Id == entity.Id) ?? throw new Exception("Specified data not found");

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
