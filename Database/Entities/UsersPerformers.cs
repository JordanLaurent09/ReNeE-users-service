using System.ComponentModel.DataAnnotations.Schema;

namespace users_service.Database.Entities
{
    public class UsersPerformers
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("performerId")]
        public int PerformerId { get; set; }


        public UsersPerformers() { }

        public UsersPerformers(int userId, int performerId)
        {
            UserId = userId;
            PerformerId = performerId;
        }

    }
}
