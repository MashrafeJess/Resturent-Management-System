using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.ViewModel
{
    public class User_Role : BaseModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public int Contact { get; set; }
    }
}
