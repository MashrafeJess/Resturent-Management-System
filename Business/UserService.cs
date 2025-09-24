using Database;
using Database.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static Database.Context.Context;

namespace Business
{
    public class UserService
    {
        ResturantContext context = new ResturantContext();
        public Result Registration(UserInfo userData)
        {
            bool x = context.UserInfo.Any(u => u.Email == userData.Email);
            if (x)
            {
                return new Result(false, "Email already exists", null);
            }
            UserInfo user = new UserInfo()
            {
                Name = userData.Name,
                Email = userData.Email,
                PasswordHash = new PasswordHasher<object>().HashPassword(userData, userData.PasswordHash),
                Contact = userData.Contact,
                Role = userData.Role == 0 ? 3 : userData.Role,
                CreatedBy = userData.CreatedBy,
                UpdatedDate = null,
                UpdatedBy = null,
            };
            context.UserInfo.Add(user);
            return new Result().DBcommit(context, "Registration successful", null, user);
        }
        public Result Login(UserInfo userData)
        {
            UserInfo userInfo = context.UserInfo.FirstOrDefault(u => u.Email == userData.Email);
            if (userInfo == null)
            {
                return new Result(true, "Email not found.Register First!", null);
            }
            PasswordVerificationResult HashResult = new PasswordHasher<UserInfo>().VerifyHashedPassword(userInfo, userInfo.PasswordHash, userData.PasswordHash);
            if (HashResult != PasswordVerificationResult.Failed)
            {
                return new Result(true, $"Logged in successfully", userInfo);
            }
            else
            {
                return new Result(false, "Incorrect Password");
            }
        }
        public Result Update(UserInfo userData)
        {
            UserInfo user = context.UserInfo.FirstOrDefault(u => u.Email == userData.Email);
            if (user == null)
            {
                return new Result(false, "User not found", null);
            }

            // Update only if provided (not null or empty)
            if (!string.IsNullOrEmpty(userData.Name))
                user.Name = userData.Name;

            if (!string.IsNullOrEmpty(userData.Email))
                user.Email = userData.Email;

            if (!string.IsNullOrEmpty(userData.PasswordHash))
                user.PasswordHash = new PasswordHasher<object>().HashPassword(userData, userData.PasswordHash);

            if (userData.Contact != 0)
                user.Contact = userData.Contact;

            user.Role = userData.Role == 0 ? 3 : userData.Role;

            user.UpdatedDate = DateTime.Now;
            user.UpdatedBy = userData.UpdatedBy;

            return new Result().DBcommit(context, "User info updated successfully", null, user);
        }
   
        public Result List()
        {
            try
            {
                var Users = context.User_Role.ToList();
                return new Result(true, "Success", Users);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(string id)
        {
            UserInfo user = context.UserInfo.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return new Result(false, "User not found", null);
            }

            return new Result(true, "User found", user);
        }
        public Result JoinSingle(string Id)
        {
            var user = context.User_Role.FirstOrDefault(u => u.UserId == Id);
            if (user == null)
            {
                return new Result(false, "User not found", null);
            }

            return new Result(true, "User found", user);
        }
    }


}