using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceSitoPanel.src.interfaces;

namespace ServiceSitoPanel.src.controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _repo;

        public SupplierController(ISupplierService repo)
        {
            _repo = repo;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var result = await _repo.GetAllSuppliers();

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierDto dto)
        {
            var result = await _repo.CreateSupplier(dto.name);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }
    }

    public class CreateSupplierDto
    {
        public string name { get; set; }
    }
}
