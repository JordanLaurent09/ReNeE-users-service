using System.ComponentModel.DataAnnotations.Schema;

namespace users_service.Database.Entities
{
    public class Photo
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("performerId")]
        public int PerformerId { get; set; }

        [Column("user")]
        public User? User { get; set; }

        [Column("userId")]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Column("image")]
        public string? Image {  get; set; }


        public Photo() { }

        public Photo(int performerId, int userId, string image)
        {
            PerformerId = performerId;
            UserId = userId;
            Image = image;
        }
    }
}
