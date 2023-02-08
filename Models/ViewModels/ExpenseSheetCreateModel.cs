using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materials_storage_subsystem.Models.ViewModels
{
    public class ExpenseSheetCreateModel
    {
        public int? WarehouseId { get; set; }
        public ExpenseSheet ExpenseSheet { get; set; }
    }
}
