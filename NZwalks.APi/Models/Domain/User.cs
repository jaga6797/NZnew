using System.ComponentModel.DataAnnotations.Schema;

namespace NZwalks.APi.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }

        public string Password { get; set; }
        //we made roles dynamic from db
        [NotMapped]
       public List<string> Roles { get; set; }
        public string Fname { get; set; }

        public string Lname { get; set; }
        //nav prop

        public List<User_Role> UserRoles { get; set; }

    }
}
