namespace ScientificStudyWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string UserRole { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PaswordSalt { get; set; }
    }
}