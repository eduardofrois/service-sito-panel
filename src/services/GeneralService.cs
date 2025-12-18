using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceSitoPanel.src.context;
using Microsoft.EntityFrameworkCore;
using ServiceSitoPanel.src.interfaces;
using ServiceSitoPanel.src.model;
using static ServiceSitoPanel.src.responses.ResponseFactory;
using ServiceSitoPanel.src.constants;

namespace ServiceSitoPanel.src.services
{
    public class GeneralService : IGeneralService
    {
        private readonly ApplicationDbContext _context;

        public GeneralService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResponses> GetAllProfiles()
        {
            var profiles = await _context.profiles.ToListAsync();

            if (profiles.Count == 0)
                return new ErrorResponse(false, 404, ErrorMessages.NoProfilesFound);

            return new SuccessResponse<IEnumerable<Profile>>(true, 200, SuccessMessages.ProfilesRetrieved, profiles);
        }

        public async Task<IResponses> GetAllClients()
        {
            var clients = await _context.client.ToListAsync();

            if (clients.Count == 0)
                return new ErrorResponse(false, 404, ErrorMessages.NoClientsFound);

            return new SuccessResponse<List<Client>>(true, 200, SuccessMessages.ClientsRetrieved, clients);
        }

        public async Task<IResponses> CreateClient(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new ErrorResponse(false, 400, "Nome do cliente é obrigatório");

            var existingClient = await _context.client
                .FirstOrDefaultAsync(c => c.name.Trim().ToUpper() == name.Trim().ToUpper());

            if (existingClient != null)
                return new SuccessResponse<Client>(true, 200, "Cliente já existe", existingClient);

            var newClient = new Client
            {
                name = name.Trim(),
                tenant_id = _context.CurrentTenantId
            };

            await _context.client.AddAsync(newClient);
            await _context.SaveChangesAsync();

            return new SuccessResponse<Client>(true, 201, "Cliente criado com sucesso", newClient);
        }
    }
}