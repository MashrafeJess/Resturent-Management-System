using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Context;
using static Database.Context.Context;
namespace Business
{
    public class AttendanceService
    {
        ResturantContext context = new ResturantContext();
        public Result CheckIn(string userId)
        {
            // Validate user exists
            var userExists = context.UserInfo.Any(u => u.UserId.ToString() == userId);
            if (!userExists)
            {
                return new Result(false, "User not found", null);
            }

            // Check if today's attendance already exists
            var today = DateTime.Today;
            var existingAttendance = context.Attendance
                .FirstOrDefault(a => a.UserId.ToString() == userId && a.CurrentDate == today);

            if (existingAttendance == null)
            {
                // Create new record
                var newAttendance = new Attendance
                {
                    UserId = userId,    // adjust if your UserId is string or int
                    CurrentDate = today,
                    CheckInTime = DateTime.Now.TimeOfDay,
                    HasChecked = true
                };

                context.Attendance.Add(newAttendance);
                return new Result().DBcommit(context, "Check-in successful", null, newAttendance);
            }
            else
            {
                // Already exists → update check-in (optional: prevent multiple check-ins)
                existingAttendance.CheckInTime = DateTime.Now.TimeOfDay;
                existingAttendance.HasChecked = true;

                context.Attendance.Update(existingAttendance);
                return new Result().DBcommit(context, "Check-in updated", null, existingAttendance);
            }
        }

        public Result CheckOut(string UserId)
        {
            var userExists = context.UserInfo.Any(u => u.UserId.ToString() == UserId);
            if (!userExists)
            {
                return new Result(false, "User not found", null);
            }
            var attendance = context.Attendance.Where(x => x.UserId == UserId && x.CurrentDate == DateTime.Today).FirstOrDefault();
            if (attendance==null)
            {
                return new Result(false, "User has not checked in today!!", null);
            }
            //var attendance = context.Attendance.FirstOrDefault(a => a.UserId == UserId && a.CheckOutTime == null);
            attendance.CheckOutTime = DateTime.Now.TimeOfDay;
            attendance.TotalHours = (int)(attendance.CheckOutTime - attendance.CheckInTime).TotalHours;
            attendance.HasChecked = false;
            context.Attendance.Update(attendance);
            return new Result().DBcommit(context, "Check-out successful", null, attendance);
        }
        public Result JoinListByRole()
        {
            var list = context.User_Attendance.Where(x=>x.Role==2).ToList();
            if (list.Count() == 0)
            {
                return new Result(false, "No attendance found");
            }
            return new Result(true, "List of all attendances", list);
        }
    }
}
