namespace NZwalks.APi.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }

        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public string Fname { get; set; }

        public string Lname { get; set; }

    }
}
