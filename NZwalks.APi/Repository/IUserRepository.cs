using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string Username, string Password);
    }
}
