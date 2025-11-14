using LogisticAppManagement.Models.Entities;

namespace LogisticAppManagement.Services.Interface
{
    public interface IClientService
    {
        Task<Client> RegisterClientAsync(Client client);
        Task<Client?> GetClientByIdAsync(Guid id);
        Task<IEnumerable<Client>> GetAllClientsAsync();
    }
}
