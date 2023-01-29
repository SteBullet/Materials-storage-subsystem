using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materials_storage_subsystem.Models
{
    public class MaterialRemaining
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public int Quantity { get; set; }
    }
}
