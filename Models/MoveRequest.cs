using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materials_storage_subsystem.Models
{
    public class MoveRequest
    {
        public int Id { get; set; }
        public int FromWarehouseId { get; set; }
        public Warehouse FromWarehouse { get; set; }
        public int ToWarehouseId { get; set; }
        public Warehouse ToWarehouse { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public int Quantity { get; set; }
    }
}
