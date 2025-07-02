using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace users_service.Database.Entities
{
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("login")]
        public string? Login { get; set; }

        [Column("firstname")]
        public string? FirstName { get; set; }

        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("sex")]
        public Sex Sex { get; set; }

        // На первое время
        [Column("password")]
        public string? Password { get; set; }

        [Column("registertime")]
        public DateTime RegisterTime { get; set; }

        [Column("lastvisit")]
        public DateTime LastVisit { get; set; }


        public User () { }

        public User(string? login, string? firstName, string? lastName, string? email, Sex sex, string? password, DateTime registerTime, DateTime lastVisit)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Sex = sex;
            Password = password;
            RegisterTime = registerTime;
            LastVisit = lastVisit;
        }
    }
}
