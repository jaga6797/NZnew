using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public interface Itokenhandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
