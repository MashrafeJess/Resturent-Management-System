using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
namespace Database
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public string UserId { get; set; }
        public DateTime CurrentDate { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public int TotalHours { get; set; }
        public bool HasChecked { get; set; } = false;
    }
}
