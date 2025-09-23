using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Food : BaseModel
    {
        [Key]
        public int FoodId { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Qty { get; set; }
        //public string Category { get; set; } = string.Empty; (Next Iteration)
    }
}
