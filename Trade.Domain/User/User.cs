namespace Trade.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public User(string firstName, string lastName, string email)
        {
            Id = new();
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            CreatedAt = DateTime.Now.ToUniversalTime();
            UpdatedAt = DateTime.Now.ToUniversalTime();
        }
    }
}