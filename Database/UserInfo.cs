using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class UserInfo : BaseModel
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int Contact { get; set; } = 0;
        public int Role { get; set; }
    }
}
