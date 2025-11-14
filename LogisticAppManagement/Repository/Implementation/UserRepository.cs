using LogisticAppManagement.Data;
using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;



namespace LogisticAppManagement.Repository.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LogisticsDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .Include(x => x.Driver)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _dbSet
                .Include(x => x.Driver)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
