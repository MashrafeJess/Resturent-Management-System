using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using Database.ViewModel;
namespace Database.Context
{
    public class Context
    {
        public class ResturantContext : DbContext //Connecting to SQL Server Database
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=ASUS;Database=Resturant;Trusted_Connection=True;TrustServerCertificate=True;ConnectRetryCount=0",
                    sqlOptions => sqlOptions.EnableRetryOnFailure());
            }

            public DbSet<UserInfo> UserInfo { get; set; }
            public DbSet<Role> Role { get; set; }
            public DbSet<Food> Food { get; set; }
            public DbSet<Cart> Cart { get; set; }
            public DbSet<Order> Order { get; set; } 
            public DbSet<Attendance> Attendance { get; set; }

            //=============== View Models ===============
            public DbSet<User_Attendance> User_Attendance { get; set; }
            public DbSet<User_Role> User_Role { get; set; }
            public DbSet<JoinCart> JoinCart { get; set; }
        }
    }
}
