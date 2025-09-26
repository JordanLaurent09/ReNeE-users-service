using Microsoft.EntityFrameworkCore;
using users_service.Database.Context;
using users_service.Database.Entities;
using users_service.Database.Repositories.Interfaces;

namespace users_service.Database.Repositories
{
    public class PhotoRepository : IRepository<Photo>
    {
        private readonly ApplicationDbContext _context;

        public PhotoRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.SaveChanges();
        }

        public string CreateNew(Photo entity)
        {
            try
            {
                _context.photos.Add(entity);
                _context.SaveChanges();

                return "OK";
            }
            catch (Exception ex)
            {
                return "Failure";
            }
        }

        public void Delete(int id)
        {
            Photo photo = _context.photos.FirstOrDefault(p => p.Id == id) ?? throw new Exception("Specified photo didn't find");

            _context.photos.Remove(photo);
            _context.SaveChanges();
        }

        public void DeleteByIds(int userId, int performerId)
        {
            IQueryable<Photo> photos = _context.photos.Where(photo => photo.PerformerId == performerId && photo.UserId == userId);

            _context.photos.RemoveRange(photos);
            _context.SaveChanges();
        }

        public IEnumerable<Photo> GetAll()
        {
            IEnumerable<Photo> photos = _context.photos;
            return photos;
        }

        public Photo GetById(int id)
        {
            Photo? photo = _context.photos.FirstOrDefault(photo => photo.Id == id);

            if (photo == null) return new Photo();
            return photo;
        }

        public IEnumerable<Photo> GetPerformerPhotos(int userId, int performerId)
        {
            IEnumerable<Photo> photos = _context.photos.Where(p => p.UserId == userId &&  p.PerformerId == performerId);

            return photos;
        }

        public async Task Update(Photo entity)
        {
            Photo photo = _context.photos.FirstOrDefault(p => p.Id == entity.Id) ?? throw new Exception("Specified photo didn't find");

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
