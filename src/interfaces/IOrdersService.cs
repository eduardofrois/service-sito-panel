using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceSitoPanel.src.dtos.orders;
using ServiceSitoPanel.src.model;

namespace ServiceSitoPanel.src.interfaces
{
    public interface IOrdersService
    {
        Task<IResponses> GetAllOrders(int pageNumber, int pageSize);
        Task<IResponses> GetOrdersByStatus(int status, int pageNumber, int pageSize);
        Task<IResponses> GetOrdersWithFilters(DateTime? dateStart, DateTime? dateEnd, int[]? statuses, int? clientId, int? supplierId, int pageNumber, int pageSize);
        Task<IResponses> CreateOrder([FromBody] CreateOrderDto[] order);
        Task<IResponses> UpdateOrderStatus([FromBody] int[] orders, [FromQuery] int value);
        Task<IResponses> NewClientInOrder([FromBody] NewClientInOrderDto values);
        Task<IResponses> GetAllPendingPaidOrders(int pageNumber, int pageSize);
        Task<IResponses> UpdatePricePaid([FromBody] UpdatePaidPriceDto[] dto);
    }
}