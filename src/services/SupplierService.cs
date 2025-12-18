using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceSitoPanel.src.context;
using ServiceSitoPanel.src.interfaces;
using ServiceSitoPanel.src.model;
using static ServiceSitoPanel.src.responses.ResponseFactory;

namespace ServiceSitoPanel.src.services
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResponses> GetAllSuppliers()
        {
            var suppliers = await _context.supplier.ToListAsync();

            if (suppliers.Count == 0)
                return new ErrorResponse(false, 404, "Nenhum fornecedor encontrado");

            return new SuccessResponse<List<Supplier>>(true, 200, "Fornecedores retornados com sucesso", suppliers);
        }

        public async Task<IResponses> CreateSupplier([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new ErrorResponse(false, 400, "Nome do fornecedor é obrigatório");

            var existingSupplier = await _context.supplier
                .FirstOrDefaultAsync(s => s.name.Trim().ToUpper() == name.Trim().ToUpper());

            if (existingSupplier != null)
                return new SuccessResponse<Supplier>(true, 200, "Fornecedor já existe", existingSupplier);

            var newSupplier = new Supplier
            {
                name = name.Trim(),
                tenant_id = _context.CurrentTenantId
            };

            await _context.supplier.AddAsync(newSupplier);
            await _context.SaveChangesAsync();

            return new SuccessResponse<Supplier>(true, 201, "Fornecedor criado com sucesso", newSupplier);
        }
    }
}
