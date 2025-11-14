using LogisticAppManagement.Models.Entities;

namespace LogisticAppManagement.Repository.Interface
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        Task<Client?> GetByIdAsync(Guid id);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByEmailAsync(string email);
    }
}
