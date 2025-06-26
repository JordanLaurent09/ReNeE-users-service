using System.ComponentModel.DataAnnotations;

namespace users_service.Database.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Login { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public Sex Sex { get; set; }

        // На первое время
        public string? Password { get; set; }

        public DateTime RegisterTime { get; set; }

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
