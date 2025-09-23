using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Order : BaseModel
    {
        [Key]
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
    }
}
