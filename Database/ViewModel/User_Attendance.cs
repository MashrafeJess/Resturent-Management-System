using System.ComponentModel.DataAnnotations;
namespace Database.ViewModel
{
    public class User_Attendance
    {
        [Key]
        public string UserId { get; set; }
        public string? Name { get; set; }
        public int? Role { get; set; }
        public DateTime? CurrentDate { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public int? TotalHours { get; set; }
        public bool? HasChecked { get; set; } = false;
    }
}
