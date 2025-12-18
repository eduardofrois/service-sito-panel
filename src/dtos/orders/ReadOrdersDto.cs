using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceSitoPanel.src.model;

namespace ServiceSitoPanel.src.dtos.orders
{
    public class ReadOrdersDto
    {
        public int id { get; set; }
        public string code { get; set; }
        public string? description { get; set; }
        public string size { get; set; }
        public int amount { get; set; }
        public Double cost_price { get; set; }
        public Double sale_price { get; set; }
        public Double total_price { get; set; }
        public string status { get; set; }
        public DateTime date_creation_order { get; set; }
        public int tenant_id { get; set; }
        public string brand { get; set; }
        public DateTime? date_order { get; set; }
        public DateTime? date_purchase_order { get; set; }
        public ClientDto? client_infos { get; set; }
        public SupplierDto? supplier_infos { get; set; }
        public string? status_conference { get; set; }
        public DateTime? date_conference { get; set; }
        public Double paid_price { get; set; }
    }

    public class ClientDto
    {
        public int client_id { get; set; }
        public string client_name { get; set; }
    };

    public class SupplierDto
    {
        public int supplier_id { get; set; }
        public string supplier_name { get; set; }
    };
}