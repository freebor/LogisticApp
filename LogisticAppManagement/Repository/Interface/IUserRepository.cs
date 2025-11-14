using LogisticAppManagement.Models.Entities;

namespace LogisticAppManagement.Repository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByRefreshTokenAsync(string refreshToken);
        Task<bool> EmailExistsAsync(string email);
    }
}
