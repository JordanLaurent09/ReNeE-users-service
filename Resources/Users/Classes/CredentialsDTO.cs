namespace users_service.Resources.Users.Classes
{
    public class CredentialsDTO
    {
        public string Credential { get; set; }
        public string Password { get; set; }

        public CredentialsDTO() { }

        public CredentialsDTO(string credential, string password)
        {
            Credential = credential;
            Password = password;
        }
    }
}
