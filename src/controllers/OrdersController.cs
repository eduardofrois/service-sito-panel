using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceSitoPanel.src.dtos.orders;
using ServiceSitoPanel.src.interfaces;

namespace ServiceSitoPanel.src.controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _repo;

        public OrdersController(IOrdersService repo)
        {
            _repo = repo;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _repo.GetAllOrders(pageNumber, pageSize);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{status}")]
        public async Task<IActionResult> GetOrderByStatus(int status, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _repo.GetOrdersByStatus(status, pageNumber, pageSize);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto[] dto)
        {
            var result = await _repo.CreateOrder(dto);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateStatusOrder([FromBody] int[] orders, [FromQuery] int value)
        {
            var result = await _repo.UpdateOrderStatus(orders, value);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("new-client-order")]
        public async Task<IActionResult> NewClientInOrder([FromBody] NewClientInOrderDto dto)
        {
            var result = await _repo.NewClientInOrder(dto);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("pending")]
        public async Task<IActionResult> PendingPaidOrders(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _repo.GetAllPendingPaidOrders(pageNumber, pageSize);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("filter")]
        public async Task<IActionResult> GetOrdersWithFilters(
            DateTime? dateStart = null,
            DateTime? dateEnd = null,
            [FromQuery] int[]? statuses = null,
            int? clientId = null,
            int? supplierId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result = await _repo.GetOrdersWithFilters(dateStart, dateEnd, statuses, clientId, supplierId, pageNumber, pageSize);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("update-paid-price")]
        public async Task<IActionResult> UpdatePaidPrice([FromBody] UpdatePaidPriceDto[] dto)
        {
            var result = await _repo.UpdatePricePaid(dto);

            if (!result.Flag) ResponseHelper.HandleError(this, result);

            return Ok(result);
        }
    }
}