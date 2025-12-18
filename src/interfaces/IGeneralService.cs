using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceSitoPanel.src.interfaces
{
    public interface IGeneralService
    {
        Task<IResponses> GetAllProfiles();
        Task<IResponses> GetAllClients();
        Task<IResponses> CreateClient(string name);
    }
}