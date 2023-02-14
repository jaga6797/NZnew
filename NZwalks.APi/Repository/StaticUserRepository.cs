using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public class StaticUserRepository : IUserRepository
    {

        private List<User> Users = new List<User>()
     {
        new User()
        {
            Fname= "Read only " , Lname= "User1", EmailAddress="readonly@user.com"
            ,Id= Guid.NewGuid(),Username="readonly@user.com",Password="Read@user",
            Roles =new List<string>{"reader"}
        },

        new User()
        {
            Fname= "Read write" , Lname= "User", EmailAddress="readwrite@user.com"
            ,Id= Guid.NewGuid(),Username="readwrite@user.com",Password="Write@user",
            Roles =new List<string>{"reader","writer"}
        }
    };
        public async Task<User> AuthenticateUserAsync(string Username, string Password)
        {
           var user = Users.Find(x => x.Username.Equals(Username, StringComparison.InvariantCultureIgnoreCase)
            && x.Password == Password);


          
            return user;
        }
    }
}
