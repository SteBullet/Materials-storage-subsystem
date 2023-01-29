using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Materials_storage_subsystem.Models
{
    public class ExpenseSheet
    {
        public long Id { set; get; }
        public string Date { set; get; }
        public int WarehouseId { set; get; }
        public Warehouse Warehouse { set; get; }
        public List<MaterialMovement> Expenses { set; get; }
    }
}