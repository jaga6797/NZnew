using Microsoft.EntityFrameworkCore;
using NZwalks.APi.Data;
using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public class DynamicUserRepository : IUserRepository
    {
        private readonly NzWalksDbContext nzWalksDbContext;

        public DynamicUserRepository(NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }
        public async Task<User> AuthenticateUserAsync(string Username, string Password)
        {

            var user = await nzWalksDbContext.Users.FirstOrDefaultAsync(
                  x => x.Username.ToLower() == Username.ToLower() && x.Password == Password);

            if (user == null)
            {
                return null;
            }
            var userRoles = await nzWalksDbContext.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await nzWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);

                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }

            }
            user.Password = null;
            return user;
        }
    }
}
