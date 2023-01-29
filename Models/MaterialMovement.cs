using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materials_storage_subsystem.Models
{
    public class MaterialMovement
    {
        public int Id { get; set; }
        public long ExpenseSheetId { get; set; }
        public ExpenseSheet ExpenseSheet { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public int Quantity { get; set; }
    }
}
