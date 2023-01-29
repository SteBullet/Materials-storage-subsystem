using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materials_storage_subsystem.Models
{
    public class Warehouse
    {
        public int Id { set; get; }
        public string Adress { set; get; }
        public int Capacity { set; get; }
        public List<User> Users { set; get; }
    }
}
