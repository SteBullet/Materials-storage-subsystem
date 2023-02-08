using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materials_storage_subsystem.Models
{
    public class User
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public int? WarehouseId { set; get; }
        public Warehouse Warehouse { set; get; }
        public string Discriminator { get; set; }
    }
}
