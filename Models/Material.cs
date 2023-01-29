using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materials_storage_subsystem.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Volume { get; set; }
        public int Weight { get; set; }
    }
}
