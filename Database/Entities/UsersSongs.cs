using System.ComponentModel.DataAnnotations.Schema;

namespace users_service.Database.Entities
{
    public class UsersSongs
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("songid")]
        public int SongId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("performerid")]
        public int PerformerId { get; set; }

        public UsersSongs() { }

        public UsersSongs(int songId, int userId, int performerId)
        {            
            SongId = songId;
            UserId = userId;
            PerformerId = performerId;
        }
    }
}
