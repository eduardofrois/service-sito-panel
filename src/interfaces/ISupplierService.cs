using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ServiceSitoPanel.src.interfaces
{
    public interface ISupplierService
    {
        Task<IResponses> GetAllSuppliers();
        Task<IResponses> CreateSupplier([FromBody] string name);
    }
}
