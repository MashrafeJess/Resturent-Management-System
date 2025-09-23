using Database;
using static Database.Context.Context;
namespace Business.Services
{
    public class RoleService
    {
        ResturantContext context = new ResturantContext();
        public Result Add(Role role)
        {
            if (context.Role.Any(x => x.RoleName == role.RoleName))
                return new Result(false, "Role already exist!");
            context.Role.Add(role);
            return new Result().DBcommit(context, "Save Successfully!", null, role);
        }
        public Result Update(Role role)
        {
            if (!context.Role.Any(x => x.RoleId == role.RoleId))
                return new Result(false, "Role not exist!");
            context.Role.Update(role);
            return new Result().DBcommit(context, "Updated Successfully!", null, role);
        }
        public Result List()
        {
            try
            {
                var roles = context.Role.ToList();
                return new Result(true, "Success", roles);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(int Id)
        {
            try
            {
                var role = context.Role.Where(x => x.RoleId == Id).FirstOrDefault();
                return new Result(true, "Success", role);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}