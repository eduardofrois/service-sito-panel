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
    [Route("api/general")]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralService _general;
        public GeneralController(IGeneralService general)
        {
            _general = general;
        }

        [HttpGet("getProfiles")]
        public async Task<IActionResult> GetProfiles()
        {
            var result = await _general.GetAllProfiles();

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("clients")]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _general.GetAllClients();

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("clients")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto dto)
        {
            var result = await _general.CreateClient(dto.name);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }
    }

    public class CreateClientDto
    {
        public string name { get; set; }
    }
}