using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceSitoPanel.src.model
{
    public class Supplier
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int tenant_id { get; set; }
    }
}
