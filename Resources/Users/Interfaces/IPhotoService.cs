using Microsoft.EntityFrameworkCore;
using users_service.Database.Entities;

namespace users_service.Resources.Users.Interfaces
{
    public interface IPhotoService
    {
        string CreateNew(Photo entity);


        void Delete(int id);

        void DeleteByIds(int userId, int performerId);

        IEnumerable<Photo> GetAll();


        Photo GetById(int id);


        IEnumerable<Photo> GetPerformerPhotos(int userId, int performerId);


        void Update(Photo entity);
    }
}
