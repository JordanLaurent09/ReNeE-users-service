using Microsoft.EntityFrameworkCore;
using users_service.Database.Entities;

namespace users_service.Resources.Users.Interfaces
{
    public interface IPhotoService
    {
        string CreateNew(Photo entity);


        void Delete(int id);


        IEnumerable<Photo> GetAll();


        Photo GetById(int id);


        IEnumerable<string> GetPerformerPhotos(int userId, int performerId);


        void Update(Photo entity);
    }
}
