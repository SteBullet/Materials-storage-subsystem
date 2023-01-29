using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materials_storage_subsystem.Models.ViewModels
{
    public class ExpenseSheetModel
    {
        public List<int> WarehousesId { get; set; }
        public ExpenseSheet ExpenseSheet { get; set; }
    }
}
