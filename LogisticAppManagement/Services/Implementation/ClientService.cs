using LogisticAppManagement.Models.Entities;
using LogisticAppManagement.Repository.Implementation;
using LogisticAppManagement.Repository.Interface;
using LogisticAppManagement.Services.Interface;

namespace LogisticAppManagement.Services.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<Client> RegisterClientAsync(Client client)
        {
            await _unitOfWork.Clients.AddAsync(client);
            await _unitOfWork.SaveChangesAsync();
            return client;
        }

        public async Task<Client?> GetClientByIdAsync(Guid id)
        {
            return await _unitOfWork.Clients.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _unitOfWork.Clients.GetAllAsync();
        }
    }
}
