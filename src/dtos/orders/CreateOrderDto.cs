using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceSitoPanel.src.dtos.orders
{
    public class CreateOrderDto
    {
        public string client { get; set; }
        public string brand { get; set; }
        public string code { get; set; }
        public string? description { get; set; }
        public string size { get; set; }
        public int amount { get; set; }
        public Double cost_price { get; set; }
        public Double sale_price { get; set; }
        public string supplier { get; set; }
        public DateTime date_creation_order { get; set; }
    }
}