using LogisticAppManagement.Data;
using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;



namespace LogisticAppManagement.Repository.Implementation
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(LogisticsDbContext context) : base(context)
        {
        }

        public async Task<Client?> GetByEmailAsync(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
