using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Database.ViewModel
{
    public class JoinCart
    {
        [Key]
        public int CartId { get; set; }
        public int FoodId { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string CreatedBy { get; set; }
        public string FoodName { get; set; }
        public string Link { get; set; }

    }
}
