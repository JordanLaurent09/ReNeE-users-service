using users_service.Database.Entities;
using users_service.Database.Repositories;
using users_service.Database.Repositories.Interfaces;
using users_service.Resources.Users.Interfaces;

namespace users_service.Resources.Users
{
    public class PhotoService : IPhotoService
    {
        private IRepository<Photo> _photosRepository;

        public PhotoService(IRepository<Photo> photosRepository)
        {
            _photosRepository = photosRepository;
        }

        public string CreateNew(Photo entity)
        {
            string result = _photosRepository.CreateNew(entity);

            return result;
        }

        public void Delete(int id)
        {
            try
            {
                _photosRepository.Delete(id);
               
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
                PhotoRepository repo = (PhotoRepository)_photosRepository;
                repo.DeleteByIds(userId, performerId);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public IEnumerable<Photo> GetAll()
        {
            IEnumerable<Photo> photos = _photosRepository.GetAll();

            return photos;
        }

        public Photo GetById(int id)
        {
            Photo photo = _photosRepository.GetById(id);

            return photo;
        }

        public IEnumerable<Photo> GetPerformerPhotos(int userId, int performerId)
        {
            IEnumerable<Photo> images = [];

            try
            {
                PhotoRepository repo = (PhotoRepository) _photosRepository;

                images = repo.GetPerformerPhotos(userId, performerId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return images;
        }

        public void Update(Photo entity)
        {
            try
            {
                _photosRepository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
