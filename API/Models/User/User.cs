namespace API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname {  get; set; }
        public string Email { get; set; }

        public User (string name, string surname, string email)
        {
            Id = new();
            Name = name;
            Surname = surname;
            Email = email;
        }
    }
}
