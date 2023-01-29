using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Materials_storage_subsystem.Models.ViewModels
{
    public class ExpenseSheetEditModel
    {
        public ExpenseSheet ExpenseSheet { get; set; }
        public List<Material> Materials { get; set; }
        public MaterialMovement MaterialMovement { get; set; }
    }
}